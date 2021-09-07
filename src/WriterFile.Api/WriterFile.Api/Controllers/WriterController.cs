using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using WriterFile.Api.Model;

namespace WriterFile.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WriterController : IDisposable
    {
        private readonly ILogger<WriterController> _logger;
        private readonly StatusCodeResult _statusCodeResult;

        // To detect redundant calls
        private bool _disposed = false;

        // Instantiate a SafeHandle instance.
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose() => Dispose(true);

        public WriterController(ILogger<WriterController> logger)
        {
            _logger = logger;
            _statusCodeResult = new StatusCodeResult(200);
        }


        [HttpPost]
        public Task<int> Post([FromBody] Job job)
        {
            try
            {
                _logger.LogInformation("ChargeId: {chargeid}, result: {status}", job.ChargeId, job.Status);
            }
            catch (Exception ex)
            {
                _logger.LogError("ChargeId: {chargeid}, result: {status}", job.ChargeId, ex.Message);
            }
            finally
            {
                Dispose(true);
            }

            return  _statusCodeResult.StatusCode;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose managed state (managed objects).
                _safeHandle?.Dispose();
            }

            _disposed = true;
        }
    }
}
