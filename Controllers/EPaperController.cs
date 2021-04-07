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
    public class EPaperController : ControllerBase
    {
        private readonly ILogger<EPaperController> _logger;
        private readonly IConfiguration _config;

        public EPaperController(ILogger<EPaperController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        [HttpGet]
        public List<EPaper> Get()
        {
            var logMessage = $"JSON-file downloaded at {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation(logMessage);          
    
            var database = new EPaperDatabase(_config["ConnectionStrings:MariaDbConnectionString"]);            
            
            return database.GetAllEPaper(new CultureInfo(_config["CultureInfo"])).OrderByDescending(f => f.PublicationDate).ToList();            
        }
    }
}
