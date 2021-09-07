using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WriterFile.Api.Model;

namespace WriterFile.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WriterController
    {
        private readonly ILogger<WriterController> _logger;
        private readonly StatusCodeResult _statusCodeResult;

        public WriterController(ILogger<WriterController> logger)
        {
            _logger = logger;
            _statusCodeResult = new StatusCodeResult(200);
        }


        [HttpPost]
        public Task Post([FromBody] Job job)
        {
            var task = Task.Factory.StartNew(() =>
            {
                _logger.LogInformation("ChargeId: {chargeid}, result: {status}", job.ChargeId, job.Status);
            });

            return Task.WhenAll(task);
        }
    }
}
