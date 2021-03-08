using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using SNAApplication;
using SNADomain;


namespace SocialNetworkAnalyser.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DatasetsController : ControllerBase
    {
        private readonly ILogger<DatasetsController> _logger;
        private readonly IDatasetsService datasetService;
        private readonly IDatasetParser parser;

        public DatasetsController(ILogger<DatasetsController> logger, IDatasetsService datasetService, IDatasetParser parser)
        {
            _logger = logger;
            this.datasetService = datasetService;
            this.parser = parser;
        }

        [HttpGet]
        public async Task<IEnumerable<Dataset>> Get()
        {
            return await datasetService.GetAllDatasets();
        }

        [HttpPost]
        public async Task<ActionResult<Dataset>> Post([FromBody] DatasetInput datasetInput)
        {
            if (datasetInput.Data == null || datasetInput.Data.Length == 0)
            {
                return BadRequest(new { errorMessage = "Input does not contain any data" });
            }
            else
            {
                try
                {
                    int result = await datasetService.CreateNewDataset(datasetInput.Name, datasetInput.Description, parser.Parse(datasetInput));
                } catch (DatasetParserException ex) {
                    _logger.LogError(ex, $"{ex.Message}\n{ex.StackTrace}");
                    return BadRequest(new { errorMessage = "Invalid data format. Input file should contain rows with two space separated numbers" });
                }
            }
            return StatusCode(201);
        }

        [Route("File")]
        [HttpPost]
        public async Task<ActionResult<Dataset>> UploadDataset([FromForm] DatasetInput datasetInput)
        {
            try
            {
                using (var stream = datasetInput.File.OpenReadStream())
                {
                    datasetInput.Data = new StreamReader(stream).ReadToEnd();
                }
                int result = await datasetService.CreateNewDataset(datasetInput.Name, datasetInput.Description, parser.Parse(datasetInput));

            }
            catch (DatasetParserException ex)
            {
                _logger.LogError(ex, $"{ex.Message}\n{ex.StackTrace}");
                return BadRequest(new { errorMessage = "Invalid data format. Input file should contain rows with two space separated numbers" });
            }
            return StatusCode(201);
    }




        [Route("{id:int}")]
        [HttpDelete]
        public async Task<ActionResult<Dataset>> Delete([FromRoute] int id)
        {
            await datasetService.RemoveDataset(id);
            return StatusCode(204);
        }
    }
}
