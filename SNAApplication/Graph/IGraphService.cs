using System.Collections.Generic;
using System.Threading.Tasks;

namespace SNAApplication.Graph
{
    public interface IGraphService
    {
        public Task<List<Node>> GetDatasetNodes(int datasetId);
        public Task<List<Link>> GetDatasetLinks(int datasetId);
    }
}
