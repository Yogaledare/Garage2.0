using Garage2._0.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Garage2._0.Data;
using Garage2._0.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Garage2._0.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Garage2_0Context _context;

        public HomeController(ILogger<HomeController> logger, Garage2_0Context context) {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index() {
            var typeCounts = await _context.Vehicles
                .WhereActive()
                .GroupBy(v => v.VehicleType)
                .Select(group => new VehicleTypeSummary {
                    VehicleType = group.Key,
                    Count = group.Count(),
                    TotalWheels = group.Sum(v => v.NumberOfWheels)
                })
                .ToListAsync();

            var arrivalTimes = _context.Vehicles
                .Select(v => new TimeEntry {
                    TimeStamp = v.ArrivalTime,
                    VehicleCountChange = VehicleCountChange.Enter
                });

            var departureTimes = _context.Vehicles
                .Where(v => v.DepartureTime != null)
                .Select(v => new TimeEntry {
                    TimeStamp = v.DepartureTime!.Value,
                    VehicleCountChange = VehicleCountChange.Leave
                });

            var timeEntries = await arrivalTimes.Concat(departureTimes)
                .OrderBy(te => te.TimeStamp)
                .ToListAsync();


            var viewModel = new HomeViewModel {
                VehicleTypeSummaries = typeCounts,
                TimeEntries = timeEntries,
            };

            return View(viewModel); 

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}




// .GroupBy(v => v.VehicleType)
// .Select(group => new HomeViewModel {
// VehicleType = group.Key, 
// Count = group.Count(),
// TotalWheels = group.Sum(v => v.NumberOfWheels)
// })
// .ToListAsync();


// return View(typeCounts);


