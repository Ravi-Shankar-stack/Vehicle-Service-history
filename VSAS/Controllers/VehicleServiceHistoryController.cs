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
        public IActionResult Index()
        {
            var serviceHistory = _context.VehicleServiceHistory.ToList();
            return View(serviceHistory);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.VehicleList = new SelectList(_context.VehicleDetail, "VehicleId", "VehicleRegNumber");
            return View();
        }

        [HttpPost]
        public IActionResult Create(VehicleServiceHistory vehicleServiceHistory)
        {
            if (ModelState.IsValid)
            {
                var vehicle = _context.VehicleDetail.FirstOrDefault(v => v.VehicleId == vehicleServiceHistory.VehicleId);
                if (vehicleServiceHistory.ServiceDoneDate > vehicle.PurchaseDate && vehicleServiceHistory.OdometerReading > vehicle.CurrentOdometerReading)
                {
                    vehicleServiceHistory.CreatedDate = DateTime.Now;
                    vehicle.CurrentOdometerReading = vehicleServiceHistory.OdometerReading;
                    _context.Update(vehicle);
                    _context.VehicleServiceHistory.Add(vehicleServiceHistory);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "VehicleServiceHistory");
                }
                else
                {
                    ViewBag.ErrorMessage = "Service Done Date should be greater than Purchase Date and Odometer Reading should be greater than current odometer reading.";
                }
            }
            ViewBag.VehicleList = new SelectList(_context.VehicleDetail, "VehicleId", "VehicleRegNumber", vehicleServiceHistory.VehicleId);
            return View(vehicleServiceHistory);
        }



    }
}
