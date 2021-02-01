using System;
using SNAEntityFramework;
using SNAEntityFramework.Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace SNAServices.Datasets
{
    public interface IDatasetsService
    {
        public Task<int> CreateNewDataset(DatasetInput datasetInput);
        public Task RemoveDataset(int id);
        public Task<List<Dataset>> GetAllDatasets();
        public Task<List<Link>> GetDatasetLinks(int datasetId);
        public Task<List<int>> GetOrderedUsers(List<Link> links);
        public List<Link> OrderLinksAndRemoveDublicates(List<Link> links);
    }
}
