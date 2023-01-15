using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSAS.Models;

namespace VSAS.Controllers
{
    public class LoginController : Controller
    {
        private VSASDbContext _context;

        public LoginController(VSASDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("~/")]
        [Route("/login")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("/login")]
        public IActionResult Index(Registration registration)
        {

            Registration User = _context.Registrations.FirstOrDefault(u =>
               u.EmailId == registration.EmailId && u.PassCode == registration.PassCode);

            
                if(User == null)
                {
                    ViewBag.errorMessage = "Invalid Attempt, Please try again.";
                    return View();
                }
                else
                {
                    TempData["loggedInEmailId"] = User.EmailId;
                    return RedirectToAction("Index", "Dashboard");
                }
            

            
        }
    }
}
