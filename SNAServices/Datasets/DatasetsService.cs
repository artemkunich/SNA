using System;
using SNAEntityFramework;
using SNAEntityFramework.Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace SNAServices.Datasets
{
    public class DatasetsService : IDatasetsService
    {
        readonly SNADbContext dbContext;
        readonly IDatasetParser parser;

        public DatasetsService(SNADbContext dbContext, IDatasetParser parser) {
            this.dbContext = dbContext;
            this.parser = parser;
        }

        public async Task<int> CreateNewDataset(DatasetInput datasetInput)
        {            
            List<Link> links = parser.Parse(datasetInput);

            links = OrderLinksAndRemoveDublicates(links);

            var newDataset = new Dataset()
            {
                Name = datasetInput.Name,
                Description = datasetInput.Description
            };

            var result = await dbContext.Datasets.AddAsync(newDataset);
            newDataset = result.Entity;
            await Task.Run(() => SetStatistics(newDataset, links));
            await dbContext.SaveChangesAsync();


            foreach (var l in links)
            {
                l.DatasetId = newDataset.Id;
                await dbContext.Links.AddAsync(l);
            }

            await dbContext.SaveChangesAsync();

            return newDataset.Id;
        }

        public async Task RemoveDataset(int id)
        {
            var dataset = dbContext.Datasets.Where(p => p.Id == id).ToList();
            dbContext.Datasets.RemoveRange(dataset);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Dataset>> GetAllDatasets() {


           return await dbContext.Datasets.OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<List<Link>> GetDatasetLinks(int datasetId) {
            var links = await dbContext.Links.Where(r => r.DatasetId == datasetId).ToListAsync();
            return links;
        }


        public async Task<List<int>> GetOrderedUsers(List<Link> links)
        {
            var ids1 = links.GroupBy(l => l.User1Id).Select(grp => grp.First().User1Id).ToList();
            var ids2 = links.GroupBy(l => l.User2Id).Select(grp => grp.First().User2Id).ToList();

            await Task.Run(() => ids1.AddRange(ids2));
            ids1 = ids1.GroupBy(u => u).Select(grp => grp.First()).ToList().OrderBy(u => u).ToList();

            return ids1;
        }

        public List<Link> OrderLinksAndRemoveDublicates(List<Link> links) {

            var orderedLinks = links.Distinct(new UnorderedLinkComparer()).OrderBy(r => r.User1Id).ThenBy(r => r.User2Id).ToList();

            //var orderedLinks = links.OrderBy(r => r.User1Id).ThenBy(r => r.User2Id).GroupBy(r => new { r.User1Id, r.User2Id }).Select(grp => grp.First()).ToList();

            //for (int i = 0; i < orderedLinks.Count; i++)
            //    orderedLinks.RemoveAll(l => l.User1Id == orderedLinks[i].User2Id && l.User2Id == orderedLinks[i].User1Id);

            return orderedLinks;
    }

        private async Task SetStatistics(Dataset dataset, List<Link> links)
        {

            dataset.LinksCount = links.Count;

            var usersCount = (await GetOrderedUsers(links)).Count;
            dataset.UsersCount = usersCount;
            dataset.AvgFriendsCount = 0;
            if (usersCount > 0)
                dataset.AvgFriendsCount = (double)links.Count * 2 / usersCount;
        }
    }

    public class UnorderedLinkComparer : IEqualityComparer<Link> {
        private IEqualityComparer<int> comparer;
        
        public UnorderedLinkComparer(IEqualityComparer<int> comparer = null) {
            this.comparer = comparer ?? EqualityComparer<int>.Default;
        }

        public bool Equals(Link l1, Link l2) {
            return comparer.Equals(l1.User1Id, l2.User1Id) && comparer.Equals(l1.User2Id, l2.User2Id) ||
                comparer.Equals(l1.User1Id, l2.User2Id) && comparer.Equals(l1.User2Id, l2.User1Id);
        }


        public int GetHashCode(Link link)
        {
            return comparer.GetHashCode(link.User1Id) ^ comparer.GetHashCode(link.User2Id);
        }
    }
}
