using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeWork3.Interfaces;
using Microsoft.Net.Http.Headers;

namespace HomeWork3.Controllers
{
    public class TemperatureController : Controller
    {
        private ITemperatureValidator _validator;

        public TemperatureController(ITemperatureValidator validator)
        {
            _validator = validator;
        }

        //GET: Temperature
        public IActionResult Index()
        {
            return View();
        }

        // GET: Temperature/GetFTemperatureFromC?cTemp=32
        public IActionResult GetFTemperatureFromC(int cTemp, Enums.FileTypes fileType)
        {
            IActionResult result;
            var validationResult = _validator.Validate(cTemp);
            if (!validationResult.Valid)
            {
                result = new BadRequestObjectResult(validationResult.ErrorMessage);
            }
            else
            {
                var fTemp = (cTemp * 9) / 5 + 32;
                var resultText = $"{fTemp.ToString()}° on the Fahrenheit scale";
                result = fileType == Enums.FileTypes.None ? Content(resultText) : GenerateResultFile(resultText, fileType);

            }
            return result;
        }

        public IActionResult GenerateResultFile(string resultText, Enums.FileTypes fileType)
        {
            IActionResult result = null;

            switch (fileType)
            {
                case Enums.FileTypes.Txt:
                    result = new FileContentResult(Encoding.UTF8.GetBytes(resultText), "text/plain");
                    break;
                case Enums.FileTypes.Zip:
                    result = GenerateZipArchive(resultText);
                    break;
                case Enums.FileTypes.Bytes:
                    result = new FileStreamResult(new MemoryStream(Encoding.UTF8.GetBytes(resultText)), "application/octet-stream");
                    break;
            }

            return result;
        }

        public IActionResult GenerateZipArchive(string resultText)
        {
            IActionResult result = null;
            using (var archiveStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(archiveStream, ZipArchiveMode.Create, true))
                {
                    var zipArchiveEntry = archive.CreateEntry("FahrenheitTemperature", CompressionLevel.Fastest);
                    using (var zipStream = zipArchiveEntry.Open())
                        zipStream.Write(Encoding.UTF8.GetBytes(resultText), 0, Encoding.UTF8.GetBytes(resultText).Length);
                }

                result = new FileContentResult(archiveStream.ToArray(), "application/zip");
            }

            return result;
        }
    }
}
