using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
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

        [HttpGet]
        [Route("/VehicleDetail/Index")]
        public IActionResult Index()
        {
            

            var AllVehicleDetail = _context.VehicleDetail.ToList();
            return View(AllVehicleDetail);
        }

        [HttpGet]
        [Route("/VehicleDetail/Create")]
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
        [Route("/VehicleDetail/Create")]
        public IActionResult Create(VehicleDetail vehicleDetail)
        {

            vehicleDetail.UpdatedDate = DateTime.Now;

            
                

               
                if (string.IsNullOrEmpty(vehicleDetail.VehicleRegNumber) || !Regex.IsMatch(vehicleDetail.VehicleRegNumber, @"^[a-zA-Z0-9]+$"))
                {
                    
                    ViewBag.errorMessage = "Purchase Date Should be Greater than Make Month and Year";
                    return View();
                
                }

                
                if (string.IsNullOrEmpty(vehicleDetail.ChassisNumber) || !Regex.IsMatch(vehicleDetail.ChassisNumber, @"^[a-zA-Z0-9]+$"))
                {
                ViewBag.errorMessage = "Purchase Date Should be Greater than Make Month and Year";
                return View();

                }

            
            if (string.IsNullOrEmpty(vehicleDetail.EngineNumber) || !Regex.IsMatch(vehicleDetail.EngineNumber, @"^[a-zA-Z0-9]+$"))
                {
                ViewBag.errorMessage = "Purchase Date Should be Greater than Make Month and Year";
                return View();

            }

           
            if (string.IsNullOrEmpty(vehicleDetail.Make) || !Regex.IsMatch(vehicleDetail.Make, @"^[a-zA-Z0-9]+$"))
                {
                ViewBag.errorMessage = "Purchase Date Should be Greater than Make Month and Year";
                return View();

            }

            
            if (vehicleDetail.MakeMonth < 1 || vehicleDetail.MakeMonth > 12)
                {
                ViewBag.errorMessage = "Purchase Date Should be Greater than Make Month and Year";
                return View();

            }

            
            if (vehicleDetail.MakeYear < 1900 || vehicleDetail.MakeYear > DateTime.Now.Year)
                {
                ViewBag.errorMessage = "Purchase Date Should be Greater than Make Month and Year";
                return View();

            }

            
            if (vehicleDetail.PurchaseDate == null)
                {
                ViewBag.errorMessage = "Purchase Date Should be Greater than Make Month and Year";
                return View();

            }

            
            if (string.IsNullOrEmpty(vehicleDetail.CurrentOdometerReading.ToString()))
                {
                ViewBag.errorMessage = "Purchase Date Should be Greater than Make Month and Year";
                return View();

            }
            else if (!Regex.IsMatch(vehicleDetail.CurrentOdometerReading.ToString(), @"^[0-9]+$"))
                {
                ViewBag.errorMessage = "Purchase Date Should be Greater than Make Month and Year";
                return View();

            }

            if (vehicleDetail.CreatedDate == null)
                {
                ViewBag.errorMessage = "Purchase Date Should be Greater than Make Month and Year";
                return View();

            }





            if (ModelState.IsValid)
            {
                try
                {
                    _context.VehicleDetail.Add(vehicleDetail);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "VehicleDetail");
                }
                catch
                {
                    ViewBag.errorMessage = "Purchase Date Should be Greater than Make Month and Year";
                    return View();
                }
            }
            else
            {
                ViewBag.errorMessage = "Purchase Date Should be Greater than Make Month and Year";
                return View();
            }


        }


        [HttpGet]
        [Route("/VehicleDetail/Edit/{id?}")]
        public IActionResult Edit(long id =0)
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

            ViewBag.errorMessage = "";
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
                ViewBag.errorMessage = "Only Alphabets and Numbers Allowed (Make Field)";
                return View(vehicleDetail);
            }

            if (string.IsNullOrEmpty(vehicleDetail.VehicleRegNumber) || !Regex.IsMatch(vehicleDetail.VehicleRegNumber, @"^[a-zA-Z0-9]+$"))
            {

                ViewBag.errorMessage = "Only Alphabets and Numbers Allowed (Make Field)";
                return View();

            }


            if (string.IsNullOrEmpty(vehicleDetail.ChassisNumber) || !Regex.IsMatch(vehicleDetail.ChassisNumber, @"^[a-zA-Z0-9]+$"))
            {
                ViewBag.errorMessage = "Only Alphabets and Numbers Allowed (Make Field)";
                return View();

            }


            if (string.IsNullOrEmpty(vehicleDetail.EngineNumber) || !Regex.IsMatch(vehicleDetail.EngineNumber, @"^[a-zA-Z0-9]+$"))
            {
                ViewBag.errorMessage = "Only Alphabets and Numbers Allowed (Make Field)";
                return View();

            }


            if (string.IsNullOrEmpty(vehicleDetail.Make) || !Regex.IsMatch(vehicleDetail.Make, @"^[a-zA-Z0-9]+$"))
            {
                ViewBag.errorMessage = "Only Alphabets and Numbers Allowed (Make Field)";
                return View();

            }


            if (vehicleDetail.MakeMonth < 1 || vehicleDetail.MakeMonth > 12)
            {
                ViewBag.errorMessage = "Only Alphabets and Numbers Allowed (Make Field)";
                return View();

            }


            if (vehicleDetail.MakeYear < 1900 || vehicleDetail.MakeYear > DateTime.Now.Year)
            {
                ViewBag.errorMessage = "Only Alphabets and Numbers Allowed (Make Field)";
                return View();

            }


            if (vehicleDetail.PurchaseDate == null)
            {
                ViewBag.errorMessage = "Only Alphabets and Numbers Allowed (Make Field)";
                return View();

            }


            if (string.IsNullOrEmpty(vehicleDetail.CurrentOdometerReading.ToString()))
            {
                ViewBag.errorMessage = "Only Alphabets and Numbers Allowed (Make Field)";
                return View();

            }
            else if (!Regex.IsMatch(vehicleDetail.CurrentOdometerReading.ToString(), @"^[0-9]+$"))
            {
                ViewBag.errorMessage = "Only Alphabets and Numbers Allowed (Make Field)";
                return View();

            }

            if (vehicleDetail.CreatedDate == null)
            {
                ViewBag.errorMessage = "Only Alphabets and Numbers Allowed (Make Field)";
                return View();

            }


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicleDetail);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.errorMessage = "Only Alphabets and Numbers Allowed (Make Field)";
                    return View(vehicleDetail);
                }
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
