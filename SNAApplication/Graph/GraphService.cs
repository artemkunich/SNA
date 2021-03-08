using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNADomain;

namespace SNAApplication.Graph
{
    public class GraphService : IGraphService
    {
        readonly IDatasetsService datasetsService;

        public GraphService(IDatasetsService datasetsService) {
            this.datasetsService = datasetsService;
        }

        public async Task<List<Node>> GetDatasetNodes(int datasetId) {
            var links = await datasetsService.GetDatasetLinks(datasetId);

            var users = await datasetsService.GetOrderedUsers(links);
            var nodes = users.Select(id => new Node() { Id = id }).ToList();

            return nodes;
        }

        public async Task<List<Link>> GetDatasetLinks(int datasetId)
        {
            var links = await datasetsService.GetDatasetLinks(datasetId);
            return links.Select(l => new Link() { Source = l.User1Id, Target = l.User2Id }).ToList();
        }
    }
}
