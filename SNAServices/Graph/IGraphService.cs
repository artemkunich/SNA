using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAServices.Graph
{
    public interface IGraphService
    {
        public Task<List<Node>> GetDatasetNodes(int datasetId);
        public Task<List<Link>> GetDatasetLinks(int datasetId);
    }
}
