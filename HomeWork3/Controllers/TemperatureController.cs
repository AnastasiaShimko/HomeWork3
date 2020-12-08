using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeWork3.Controllers
{
    public class TemperatureController : Controller
    {
        public IActionResult Index()
        {
            return Ok();
        }

        // GET: Temperature/GetFTemperatureFromC?cTemp=32
        public IActionResult GetFTemperatureFromC(int cTemp)
        {
            var fTemp = (cTemp * 9) / 5 + 32;
            return Content(fTemp.ToString());
        }
    }
}
