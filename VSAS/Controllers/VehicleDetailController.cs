using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using VSAS.Models;

namespace VSAS.Controllers
{
    public class VehicleDetailController : Controller
    {
        private VSASDbContext _context;

        public VehicleDetailController(VSASDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //var AllVehicleDetail = _context.VehicleDetail.ToList();
            ////var contactNumberList = _context.UserDetails.Select(u => u.ContactNumber).ToList();
            ////List<SelectListItem> selectList = new List<SelectListItem>();
            ////foreach (var item in contactNumberList)
            ////{
            ////    selectList.Add(new SelectListItem { Text = item, Value = item });
            ////}
            //ViewBag.ContactNumberList = _context.UserDetails.Select(u => u.ContactNumber);
            //return View(AllVehicleDetail);

            var AllVehicleDetail = _context.VehicleDetail.ToList();
            return View(AllVehicleDetail);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var contactNumberList = _context.UserDetails.Select(u => u.ContactNumber).ToList();
            List<SelectListItem> selectList = new List<SelectListItem>();
            foreach (var item in contactNumberList)
            {
                selectList.Add(new SelectListItem { Text = item, Value = item });
            }
            ViewBag.ContactNumberList = selectList;


            var monthList = Enumerable.Range(1, 12).Select(m => new SelectListItem { Text = new DateTime(2000, m, 1).ToString("MMMM"), Value = m.ToString() });
            ViewBag.MonthList = monthList;

            

            return View();
        }

        [HttpPost]
        public IActionResult Create(VehicleDetail vehicleDetail)
        {

            vehicleDetail.UpdatedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.VehicleDetail.Add(vehicleDetail);
                _context.SaveChanges();
                return RedirectToAction("Index", "VehicleDetail");
            }
            else
            {
                ViewBag.errorMessage = "Purchase Date Should be Greater than Make Month and Year";
                return View();
            }


        }


        [HttpGet]
        public IActionResult Edit(long id)
        {
            var vehicleDetail = _context.VehicleDetail.Find(id);
            //var contactNumberList = TempData.Peek("ContactNumber").ToString();
            var contactNumberList = _context.UserDetails.Select(u => u.ContactNumber).ToList();
            List<SelectListItem> selectList = new List<SelectListItem>();
            foreach (var item in contactNumberList)
            {
                selectList.Add(new SelectListItem { Text = item, Value = item });
            }
            ViewBag.ContactNumberList = selectList;
            var monthList = Enumerable.Range(1, 12).Select(m => new SelectListItem { Text = new DateTime(2000, m, 1).ToString("MMMM"), Value = m.ToString() });
            var makeYearList = Enumerable.Range(1970, DateTime.Now.Year - 1970 + 1).Select(y => new SelectListItem { Text = y.ToString(), Value = y.ToString() });

            
            //ViewBag.ContactNumberList = contactNumberList;
            ViewBag.MonthList = monthList;
            ViewBag.MakeYearList = makeYearList;

            return View(vehicleDetail);
        }

       

        [HttpPost]
        public IActionResult Edit(long id, VehicleDetail vehicleDetail)
        {
            if (id != vehicleDetail.VehicleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(vehicleDetail);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.errorMessage = "Only Alphabets and Numbers Allowed (Make Field)";
                return View(vehicleDetail);
            }
        }

        public IActionResult Details(int id)
        {
            var vehicleDetail = _context.VehicleDetail.SingleOrDefault(x => x.VehicleId == id);
            if (vehicleDetail == null)
            {
                return NotFound();
            }
            return View(vehicleDetail);
        }
    }
}
