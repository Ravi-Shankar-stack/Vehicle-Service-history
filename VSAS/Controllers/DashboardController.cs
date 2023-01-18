using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VSAS.Controllers
{
    public class DashboardController : Controller
    {
        [HttpGet]
        [Route("/Dashboard/Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
