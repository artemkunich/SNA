using System;
using System.Linq;
using Xunit;
using SNAApplication;
using SNADomain;
using SNAEntityFramework;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace SNATests
{
    public class TestDatasetsService
    {

        public TestDatasetsService()
        {
            services = new ServiceCollection();
            services.AddDbContext<SNADbContext>(options => options.UseInMemoryDatabase("InMemoryDbForTesting").UseInternalServiceProvider(serviceProvider));
            services.AddTransient<IDatasetsService, DatasetsService>();
            services.AddTransient<IDatasetParser, DatasetStringParser>();
            services.AddTransient<IUnitOfWork, EFUnitOfWork>();

            serviceProvider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

            dbContext = serviceProvider.GetRequiredService<SNADbContext>();
            datasetService = serviceProvider.GetRequiredService<IDatasetsService>();
            parser = serviceProvider.GetRequiredService<IDatasetParser>();

        }

        private SNADbContext dbContext;
        private IDatasetsService datasetService;
        private IServiceProvider serviceProvider;
        private IServiceCollection services;
        private IDatasetParser parser;


        [Fact]
        public async void TestGetOrderedUsers()
        {
            List<Link> input = new List<Link>() {
                new Link(){ User1Id=0, User2Id=1 }, new Link(){ User1Id=0, User2Id=2 },
                new Link(){ User1Id=0, User2Id=3 }, new Link(){ User1Id=0, User2Id=8 },
                new Link(){ User1Id=1, User2Id=2 }, new Link(){ User1Id=1, User2Id=11 },
                new Link(){ User1Id=1, User2Id=3 }, new Link(){ User1Id=1, User2Id=4 },
                new Link(){ User1Id=2, User2Id=3 }, new Link(){ User1Id=2, User2Id=4 },
                new Link(){ User1Id=3, User2Id=4 }, new Link(){ User1Id=3, User2Id=7 },
                new Link(){ User1Id=13, User2Id=4 }, new Link(){ User1Id=2, User2Id=99 }
            };

            List<int> expectedOutput = new List<int>(){ 0, 1, 2, 3, 4, 7, 8, 11, 13, 99 };
            List<int> actualOutput = await datasetService.GetOrderedUsers(input);

            Assert.True(expectedOutput.Count == actualOutput.Count);

            for (int i = 0; i < actualOutput.Count; i++) {
                Assert.Equal(expectedOutput[i], actualOutput[i]);
            }           
        }

        [Fact]
        public void TestOrderLinksAndRemoveDublicites()
        {
            List<Link> input = new List<Link>() {
                new Link(){ User1Id=1, User2Id=2 }, new Link(){ User1Id=1, User2Id=3 },
                new Link(){ User1Id=0, User2Id=1 }, new Link(){ User1Id=2, User2Id=3 },
                new Link(){ User1Id=0, User2Id=2 }, new Link(){ User1Id=1, User2Id=3 },
                new Link(){ User1Id=0, User2Id=3 }, new Link(){ User1Id=3, User2Id=1 },

            };

            List<Link> expectedOutput = new List<Link>() {
                new Link(){ User1Id=0, User2Id=1 },
                new Link(){ User1Id=0, User2Id=2 },
                new Link(){ User1Id=0, User2Id=3 },
                new Link(){ User1Id=1, User2Id=2 },
                new Link(){ User1Id=1, User2Id=3 },
                new Link(){ User1Id=2, User2Id=3 }

            };
            List<Link> actualOutput = datasetService.OrderLinksAndRemoveDublicates(input);

            Assert.True(expectedOutput.Count == actualOutput.Count);

            for (int i = 0; i < actualOutput.Count; i++)
            {
                Assert.Equal(expectedOutput[i].User1Id, actualOutput[i].User1Id);
                Assert.Equal(expectedOutput[i].User2Id, actualOutput[i].User2Id);
            }
        }

        [Theory]
        [InlineData("qwerty")]
        [InlineData("3.14 2.72")]
        [InlineData("3 2 4")]
        public void TestDatasetParserWithInvalidData(string value)
        {
            Assert.Throws<DatasetParserException>(() =>
            {
                parser.Parse(new DatasetInput() { Data = value });
            });
        }

        [Theory]
        [InlineData("0 1\n3 9", 0, 1, 3, 9)]
        [InlineData("16\t91\n 1750 4923", 16, 91, 1750, 4923)]
        public void TestDatasetParserWithValidData(string value, int input1, int input2, int input3, int input4)
        {
            List<Link> result;
            try
            {
                result = parser.Parse(new DatasetInput() { Data = value });
            }
            catch (Exception ex)
            {
                Assert.True(false, $"Parser throw exception {ex.Message}\n{ex.StackTrace}");
                return;
            }
            Assert.True(result.Count == 2);
            Assert.Equal(input1, result[0].User1Id);
            Assert.Equal(input2, result[0].User2Id);
            Assert.Equal(input3, result[1].User1Id);
            Assert.Equal(input4, result[1].User2Id);
        }

        [Fact]
        public async void TestCreateAndDeleteDataset()
        {
            string datasetName = "TestDatasetName";
            string datasetDescription = "TestDatasetDescription";
            string datasetData = "0 1\n0 2\n0 3\n1 2\n1 3\n2 3\n2\t3";

            var datasetInput = new DatasetInput()
            {
                Name = datasetName,
                Description = datasetDescription,
                Data = datasetData
            };


            CreateTestDataset(datasetInput);
            var datasets = dbContext.Datasets.Where(d => d.Name == datasetName).ToList();
            Assert.Single(datasets);

            Dataset dataset = datasets[0];

            //Description
            Assert.Equal(datasetDescription, dataset.Description);

            //Links
            var expectedLinks = datasetService.OrderLinksAndRemoveDublicates(parser.Parse(new DatasetInput() { Data = datasetData }));
            var actualLinks = dbContext.Links.Where(l => l.DatasetId == dataset.Id).ToList();

            Assert.Equal(expectedLinks.Count, dataset.LinksCount);
            Assert.Equal(expectedLinks.Count, actualLinks.Count);

            for (int i = 0; i < expectedLinks.Count; i++)
            {
                Assert.Equal(expectedLinks[i].User1Id, actualLinks[i].User1Id);
                Assert.Equal(expectedLinks[i].User2Id, actualLinks[i].User2Id);
            }

            //Users
            var users = await datasetService.GetOrderedUsers(expectedLinks);
            Assert.Equal(users.Count, dataset.UsersCount);


            //Delete dataset
            await datasetService.RemoveDataset(dataset.Id);
            datasets = dbContext.Datasets.Where(d => d.Id == dataset.Id).ToList();
            Assert.Empty(datasets);

            actualLinks = dbContext.Links.Where(l => l.DatasetId == dataset.Id).ToList();
            Assert.Empty(actualLinks);

        }



        private void CreateTestDataset(DatasetInput datasetInput) {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            datasetService.CreateNewDataset(datasetInput.Name, datasetInput.Description, parser.Parse(datasetInput));
        }
    }
}
