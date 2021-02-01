using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SNAEntityFramework;
using SNAServices.Graph;

namespace SocialNetworkAnalyser.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GraphController : ControllerBase
    {
        private readonly ILogger<DatasetsController> _logger;
        private readonly IGraphService graphService;

        public GraphController(ILogger<DatasetsController> logger, IGraphService graphService)
        {
            _logger = logger;
            this.graphService = graphService;
        }

        [Route("Nodes/{id:int}")]
        [HttpGet]
        public async Task<IEnumerable<Node>> GetNodes(int id)
        {
            return await graphService.GetDatasetNodes(id);
        }

        [Route("Links/{id:int}")]
        [HttpGet]
        public async Task<IEnumerable<Link>> GetLinks(int id)
        {
            return await graphService.GetDatasetLinks(id);
        }
    }
}
