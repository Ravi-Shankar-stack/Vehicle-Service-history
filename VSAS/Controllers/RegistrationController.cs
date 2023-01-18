using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSAS.Models;

namespace VSAS.Controllers
{
    
    public class RegistrationController : Controller
    {
        private VSASDbContext _context;

        public RegistrationController(VSASDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Registration/Create")]
        public IActionResult Create()
        {
            ViewBag.errorMessage = "";
            return View();
        }

        [HttpPost]
        [Route("Registration/Create")]
        public IActionResult Create(Registration registration)
        {
            
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Registrations.Add(registration);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Login");
                }

                catch
                {
                    ViewBag.errorMessage = "Email Id Already Exists.";
                    return View();
                }
            }
            else
            {
                ViewBag.errorMessage = "Email Id Already Exists.";
                return View();
            }


        }
    }
}
