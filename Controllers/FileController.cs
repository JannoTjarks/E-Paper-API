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
        public List<EPaper> Get()
        {
            var logMessage = $"JSON-file downloaded at {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation(logMessage);
            
            var directoryInfo = new DirectoryInfo(@"/e-paper/").GetFiles().OrderByDescending(f => f.LastWriteTime);            

            var listOfEPaper = new List<EPaper>();
            foreach(FileInfo fileInfo in directoryInfo)
            {
                var fileNameWithoutExtension = fileInfo.Name.Replace(fileInfo.Extension,"");
                var ePaperName = fileInfo.Name;
                var ePaperPublicationDate = fileNameWithoutExtension.Split("_")[1].Replace("-", ".");
                var ePaperCategory = fileNameWithoutExtension.Split("_")[3];
                //TODO: Strings in die Config-Datei ausgliedern!
                var ePaperPath = "/e-paper/" + fileInfo.Name;
                var ePaperWeekday = CultureInfo.DefaultThreadCurrentCulture.DateTimeFormat.GetDayName(Convert.ToDateTime(ePaperPublicationDate).DayOfWeek); 
                var ePaperImagePath = "/e-paper/frontpages/" + fileNameWithoutExtension + ".jpg";

                listOfEPaper.Add(new EPaper(ePaperName, ePaperPublicationDate, ePaperCategory, ePaperPath, ePaperWeekday, ePaperImagePath));
            }
            
            return listOfEPaper;            
        }
    }
}
