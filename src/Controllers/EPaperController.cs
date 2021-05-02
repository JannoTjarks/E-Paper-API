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
    [Route("")]
    public class EPaperController : ControllerBase
    {
        private readonly ILogger<EPaperController> _logger;
        private readonly IConfiguration _config;

        public EPaperController(ILogger<EPaperController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
        
        [Route("newest")]
        [HttpGet]
        public List<EPaper> GetNewest()
        {
            var logMessage = $"JSON-file for newest epapers downloaded at {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation(logMessage);
    
            var database = new EPaperDatabase(_config["ConnectionStrings:MariaDbConnectionString"]);
            
            return database.GetNewestEpaper();
        }

        [Route("all")]
        [HttpGet]
        public List<EPaper> GetAll()
        {
            var logMessage = $"JSON-file for all epapers downloaded at {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation(logMessage);
    
            var database = new EPaperDatabase(_config["ConnectionStrings:MariaDbConnectionString"]);
            
            return database.GetAllEPaper();
        }

        [Route("{name}")]
        [HttpGet]
        public List<EPaper> GetByName(String name, [FromQuery]string all)
        {
            var logMessage = "JSON-file for " + name + $" downloaded at {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation(logMessage);  

            var database = new EPaperDatabase(_config["ConnectionStrings:MariaDbConnectionString"]);

            var epaperNames = database.GetEpaperNames();
            foreach(var epaperName in epaperNames)
            {
                if(epaperName == name)
                {
                    return database.GetEPaperByName(epaperName, all);
                }
            }

            return new List<EPaper>();
        }
    }
}
