using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNADomain
{
    public class DatasetsService : IDatasetsService
    {
        readonly IUnitOfWork unitOfWork;

        public DatasetsService(IUnitOfWork unitOfWork) {
            this.unitOfWork = unitOfWork;
        }

        public async Task<int> CreateNewDataset(string name, string description, List<Link> links)
        {            
            links = OrderLinksAndRemoveDublicates(links);

            var newDataset = new Dataset()
            {
                Name = name,
                Description = description
            };

            await unitOfWork.DatasetRepository.Create(newDataset);
            await SetStatistics(newDataset, links);
            await unitOfWork.Save();


            foreach (var l in links)
            {
                l.DatasetId = newDataset.Id;
                await unitOfWork.LinkRepository.Create(l);
            }

            await unitOfWork.Save();

            return newDataset.Id;
        }

        public async Task RemoveDataset(int id)
        {
            await unitOfWork.DatasetRepository.Delete(id);
            await unitOfWork.Save();
        }

        public async Task<List<Dataset>> GetAllDatasets() {
            var result = await unitOfWork.DatasetRepository.GetAll();
            return result.ToList().OrderByDescending(d => d.Id).ToList();
        }

        public async Task<List<Link>> GetDatasetLinks(int datasetId) {
            var links = await unitOfWork.LinkRepository.Get(datasetId);
            return links.ToList();
        }

        public async Task<List<int>> GetOrderedUsers(List<Link> links)
        {
            var ids1 = links.GroupBy(l => l.User1Id).Select(grp => grp.First().User1Id).ToList();
            var ids2 = links.GroupBy(l => l.User2Id).Select(grp => grp.First().User2Id).ToList();

            await Task.Run(() => ids1.AddRange(ids2));
            ids1 = ids1.GroupBy(u => u).Select(grp => grp.First()).ToList().OrderBy(u => u).ToList();

            return ids1;
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

        public List<Link> OrderLinksAndRemoveDublicates(List<Link> links)
        {

            var orderedLinks = links.Distinct(new UnorderedLinkComparer()).OrderBy(r => r.User1Id).ThenBy(r => r.User2Id).ToList();

            return orderedLinks;
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
