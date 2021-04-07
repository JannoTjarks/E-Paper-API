using EPaperApi.Model;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Threading;
using Microsoft.Extensions.Configuration;

namespace EPaperSammlung.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private readonly ILogger<FileController> _logger;
        private readonly IConfiguration _config;

        public FileController(ILogger<FileController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        [HttpGet]
        public List<EPaper> Get()
        {
            var logMessage = $"JSON-file downloaded at {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation(logMessage);          

            var listOfEPaper = new List<EPaper>();
            var database = new EPaperDatabase(_config["ConnectionStrings:MariaDbConnectionString"]);
            listOfEPaper = database.GetAllEPaper(new CultureInfo(_config["CultureInfo"])).OrderByDescending(f => f.PublicationDate).ToList();
            
            return listOfEPaper;            
        }
    }
}
