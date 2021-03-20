using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EPaperSammlung.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private readonly ILogger<FileController> _logger;

        public FileController(ILogger<FileController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            string logMessage = $"JSON-file downloaded at {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation(logMessage);
            List<string> dirs = Directory.GetFiles(@"/e-paper/").ToList();
            return dirs;
        }
    }
}
