using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("VehicleId,UserId,VehicleRegNumber,ChassisNumber,EngineNumber,Make,MakeMonthYear,PurchaseDate,CurrentOdometerReading,CreatedDate,UpdatedDate")] VehicleDetail vehicleDetail)
        {
            if (ModelState.IsValid)
            {
                vehicleDetail.CreatedDate = DateTime.Now;
                _context.Add(vehicleDetail);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehicleDetail);
        }
    }
}
