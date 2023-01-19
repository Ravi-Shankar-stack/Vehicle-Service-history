using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSAS.Models;

namespace VSAS.Controllers
{
    public class VehicleServiceHistoryController : Controller
    {
        private VSASDbContext _context;

        public VehicleServiceHistoryController(VSASDbContext context)
        {
            
            _context = context;
        }

        [HttpGet]
        [Route("/VehicleServiceHistory/Index")]
        public IActionResult Index()
        {
            var serviceHistory = _context.VehicleServiceHistory.ToList();
            return View(serviceHistory);
        }

        [HttpGet]
        [Route("/VehicleServiceHistory/Create")]
        public IActionResult Create()
        {
            //var vehicleList = _context.VehicleDetail.Select(u => u.VehicleRegNumber).ToList();
            //ViewBag.vehicleList = vehicleList;
            //return View();

            var vehicleList = _context.VehicleDetail.Select(u => new SelectListItem { Value = u.VehicleId.ToString(), Text = u.VehicleRegNumber }).ToList();
            ViewBag.vehicleList = vehicleList;
            return View();
        }

        [HttpPost]
        [Route("/VehicleServiceHistory/Create")]
        public IActionResult Create(VehicleServiceHistory vehicleServiceHistory)
        {
            
            if (vehicleServiceHistory.OdometerReading <= 0)
            {
                ViewBag.ErrorMessage = "Service Done Date Should be Greater than Purchase Date..";
                return View();
            }

            
            if (vehicleServiceHistory.ServiceDoneDate == null)
            {
                ViewBag.ErrorMessage = "Service Done Date Should be Greater than Purchase Date..";
                return View();
            }

            
            if (string.IsNullOrEmpty(vehicleServiceHistory.ServiceDetails))
            {
                ViewBag.ErrorMessage = "Service Done Date Should be Greater than Purchase Date..";
                return View();
            }
            else if (vehicleServiceHistory.ServiceDetails.Length > 100)
            {
                ViewBag.ErrorMessage = "Service Done Date Should be Greater than Purchase Date..";
                return View();
            }

            
            if (string.IsNullOrEmpty(vehicleServiceHistory.ServiceDealerDetails))
            {
                ViewBag.ErrorMessage = "Service Done Date Should be Greater than Purchase Date..";
                return View();
            }
            else if (vehicleServiceHistory.ServiceDealerDetails.Length > 100)
            {
                ViewBag.ErrorMessage = "Service Done Date Should be Greater than Purchase Date..";
                return View();
            }

            
            if (vehicleServiceHistory.NextServiceDueDate == null)
            {
                ViewBag.ErrorMessage = "Service Done Date Should be Greater than Purchase Date..";
                return View();
            }

            
            if (vehicleServiceHistory.CreatedDate == null)
            {
                ViewBag.ErrorMessage = "Service Done Date Should be Greater than Purchase Date..";
                return View();
            }

            


            if (ModelState.IsValid)
            {
                var vehicle = _context.VehicleDetail.FirstOrDefault(v => v.VehicleId == vehicleServiceHistory.VehicleId);
                if (vehicleServiceHistory.ServiceDoneDate > vehicle.PurchaseDate && vehicleServiceHistory.OdometerReading > vehicle.CurrentOdometerReading)
                {
                    vehicleServiceHistory.CreatedDate = DateTime.Now;
                    vehicle.CurrentOdometerReading = vehicleServiceHistory.OdometerReading;
                    try
                    {
                        _context.Update(vehicle);
                        _context.VehicleServiceHistory.Add(vehicleServiceHistory);
                        _context.SaveChanges();
                        return RedirectToAction("Index", "VehicleServiceHistory");
                    }
                    catch
                    {
                        ViewBag.ErrorMessage = "Service Done Date Should be Greater than Purchase Date..";
                        return View();
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Service Done Date Should be Greater than Purchase Date..";
                    return View();
                }
            }
            //else
            //{
            //    ViewBag.ErrorMessage = "Service Done Date should be greater than Purchase Date and Odometer Reading should be greater than current odometer reading.";
            //    return View();
            //}
            ViewBag.VehicleList = new SelectList(_context.VehicleDetail, "VehicleId", "VehicleRegNumber", vehicleServiceHistory.VehicleId);
            return View(vehicleServiceHistory);
        }



    }
}
