using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNADomain
{
    public interface IDatasetsService
    {
        public Task<int> CreateNewDataset(string name, string description, List<Link> links);
        public Task RemoveDataset(int id);
        public Task<List<Dataset>> GetAllDatasets();
        public Task<List<Link>> GetDatasetLinks(int datasetId);
        public Task<List<int>> GetOrderedUsers(List<Link> links);
        public List<Link> OrderLinksAndRemoveDublicates(List<Link> links);
    }
}
