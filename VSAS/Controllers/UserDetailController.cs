using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSAS.Models;
namespace VSAS.Controllers
{
    public class UserDetailController : Controller
    {
        private VSASDbContext _context;

        public UserDetailController(VSASDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            
            //List<UserDetails> User = new List<UserDetails>();
            string userEmailId;
            if(TempData.Peek("loggedInEmailId") == null)
            {
                return NotFound();
            }
            else
            {
                userEmailId = TempData.Peek("loggedInEmailId").ToString();
            }

            var registration = _context.Registrations.Where(r => r.EmailId == userEmailId).FirstOrDefault();
            if (registration != null)
            {
                var userDetail = _context.UserDetails.Where(u => u.EmailId == registration.EmailId).FirstOrDefault();
                if (userDetail != null)
                {

                    userDetail.FirstName = registration.FirstName;
                    userDetail.LastName = registration.LastName;
                    userDetail.ContactNumber = registration.ContactNumber;
                    userDetail.Passcode = registration.PassCode;
                    userDetail.UpdatedDate = DateTime.Now;
                    _context.SaveChanges();
                }
                else
                {
                    
                    var newUserDetail = new UserDetails
                    {
                        FirstName = registration.FirstName,
                        LastName = registration.LastName,
                        EmailId = registration.EmailId,
                        ContactNumber = registration.ContactNumber,
                        Passcode = registration.PassCode,
                        CreatedDate = DateTime.Now
                    };
                    
                    _context.UserDetails.Add(newUserDetail);
                    _context.SaveChanges();
                }
            }

            var allUsers = _context.UserDetails.ToList();
            //foreach (UserDetails UD in allUsers)
            //{
            //    if(UD.EmailId == userEmailId)
            //    {
            //        User.Add(UD);
            //    }
            //}
            return View(allUsers);
        }

        public IActionResult Details(int id)
        {
            var userDetail = _context.UserDetails.SingleOrDefault(x => x.UserId == id);
            if (userDetail == null)
            {
                return NotFound();
            }
            return View(userDetail);
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            var userDetail = _context.UserDetails.SingleOrDefault(x => x.UserId == id);
            if (userDetail == null)
            {
                return NotFound();
            }
            return View(userDetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, UserDetails userDetail)
        {
            if (id != userDetail.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(userDetail);
                _context.SaveChanges();
                RedirectToAction("Index");
            }
            return View(userDetail);
        }
    }
}
