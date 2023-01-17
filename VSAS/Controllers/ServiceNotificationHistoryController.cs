using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSAS.Models;

namespace VSAS.Controllers
{
    public class ServiceNotificationHistoryController : Controller
    {
        private VSASDbContext _context;

        public ServiceNotificationHistoryController(VSASDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var notification = _context.ServiceNotificationHistory.ToList();
            return View(notification);
        }
    }
}
