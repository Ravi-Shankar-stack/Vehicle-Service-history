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

            var makeYearList = Enumerable.Range(1970, DateTime.Now.Year - 1970 + 1).Select(y => new SelectListItem { Text = y.ToString(), Value = y.ToString() });
            ViewBag.MakeYearList = makeYearList;

            return View();
        }

        [HttpPost]
        public IActionResult Create(VehicleDetail vehicleDetail)
        {
            //DateTime makeDate;
            //if (DateTime.TryParseExact(vehicleDetail.MakeMonthYear.ToString(), "yyyyMM", CultureInfo.InvariantCulture, DateTimeStyles.None, out makeDate))
            //{
            //    if (vehicleDetail.PurchaseDate > makeDate)
            //    {
            //        _context.VehicleDetail.Add(vehicleDetail);
            //        _context.SaveChanges();
            //        return RedirectToAction("Index", "VehicleDetail");
            //    }
            //    else
            //    {
            //        ViewBag.ErrorMessage = "Purchase date should be greater than make month and year";
            //    }

            //}
            //return View(vehicleDetail);

            if (ModelState.IsValid)
            {
                _context.VehicleDetail.Add(vehicleDetail);
                _context.SaveChanges();
                return RedirectToAction("Index", "VehicleDetail");
            }
            else
            {
                ViewBag.ErrorMessage = "Purchase date should be greater than make month and year";
                return View();
            }


        }


        [HttpGet]
        public IActionResult Edit(long id)
        {
            var vehicleDetail = _context.VehicleDetail.Find(id);
            var contactNumberList = TempData.Peek("ContactNumber").ToString();
            var monthList = Enumerable.Range(1, 12).Select(m => new SelectListItem { Text = new DateTime(2000, m, 1).ToString("MMMM"), Value = m.ToString() });
            var makeYearList = Enumerable.Range(1970, DateTime.Now.Year - 1970 + 1).Select(y => new SelectListItem { Text = y.ToString(), Value = y.ToString() });

            
            ViewBag.ContactNumberList = contactNumberList;
            ViewBag.MonthList = monthList;
            ViewBag.MakeYearList = makeYearList;

            return View(vehicleDetail);
        }

        [HttpPost]
        public IActionResult Edit(VehicleDetail vehicleDetail)
        {
            if (ModelState.IsValid)
            {
                var dbVehicleDetail = _context.VehicleDetail.Find(vehicleDetail.VehicleId);
                dbVehicleDetail.UserId = vehicleDetail.UserId;
                dbVehicleDetail.VehicleRegNumber = vehicleDetail.VehicleRegNumber;
                dbVehicleDetail.ChassisNumber = vehicleDetail.ChassisNumber;
                dbVehicleDetail.EngineNumber = vehicleDetail.EngineNumber;
                dbVehicleDetail.Make = vehicleDetail.Make;
                dbVehicleDetail.MakeMonthYear = vehicleDetail.MakeMonthYear;
                dbVehicleDetail.PurchaseDate = vehicleDetail.PurchaseDate;
                dbVehicleDetail.CurrentOdometerReading = vehicleDetail.CurrentOdometerReading;
                dbVehicleDetail.UpdatedDate = DateTime.Now;

                _context.SaveChanges();
                return RedirectToAction("Index", "VehicleDetail");
            }
            return View(vehicleDetail);
        }
    }
}
