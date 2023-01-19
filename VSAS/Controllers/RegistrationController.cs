using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
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
            if (string.IsNullOrEmpty(registration.FirstName) || registration.FirstName.Length > 25)
            {
                //ModelState.AddModelError("FirstName", "First Name is required and should not be more than 25 characters");
                
            }
            if (string.IsNullOrEmpty(registration.LastName) || registration.LastName.Length > 25)
            {
                //ModelState.AddModelError("LastName", "Last Name is required and should not be more than 25 characters");
                
            }
            if (string.IsNullOrEmpty(registration.EmailId) || !new EmailAddressAttribute().IsValid(registration.EmailId))
            {
                ModelState.AddModelError("EmailId", "Email is required and should be a valid email address");
                
            }
            if (string.IsNullOrEmpty(registration.ContactNumber) || !Regex.IsMatch(registration.ContactNumber, @"^[0-9]{10}$"))
            {
                ModelState.AddModelError("ContactNumber", "Contact Number is required and should be a 10 digit number");
               
            }
            if (string.IsNullOrEmpty(registration.PassCode) || registration.PassCode.Length < 8)
            {
                ModelState.AddModelError("PassCode", "Passcode is required and should be at least 8 characters long");
                
            }

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
