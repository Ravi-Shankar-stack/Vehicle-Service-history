using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSAS.Models;

namespace VSAS.Controllers
{
    [Route("Registration")]
    public class RegistrationController : Controller
    {
        private VSASDbContext _context;

        public RegistrationController(VSASDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create(Registration registration)
        {
            if (ModelState.IsValid)
            {
                _context.Registrations.Add(registration);
                _context.SaveChanges();
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.errorMessage = "Email Id Already Exists.";
                return View();
            }
        }
    }
}
