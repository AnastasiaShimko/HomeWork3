using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeWork3.Controllers
{
    public class RedirectController : Controller
    {
        public IActionResult Index()
        {
            return Ok();
        }

        // GET: Redirect/ToItAcademy
        public IActionResult ToItAcademy()
        {
            return Redirect("https://it-academy.by");
        }
    }
}
