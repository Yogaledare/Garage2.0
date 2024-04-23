using Garage2._0.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Garage2._0.Data;
using Garage2._0.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Garage2._0.Controllers {
    public class HomeController : Controller {
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
                .Select(v => new RawTimeEntry {Time = v.ArrivalTime, Change = 1});
            // {Time = v.ArrivalTime, Change = 1});

            var departureTimes = _context.Vehicles
                .Where(v => v.DepartureTime != null)
                .Select(v => new RawTimeEntry {Time = v.DepartureTime!.Value, Change = -1});

            var rawTimeEntries = await arrivalTimes.Concat(departureTimes)
                .OrderBy(e => e.Time)
                .ToListAsync();

            var timeInterval = TimeSpan.FromDays(1);
            var aggregatedTimeEntries = AggregateTimeEntries(rawTimeEntries, timeInterval);

            var viewModel = new HomeViewModel {
                VehicleTypeSummaries = typeCounts,
                TimeEntries = aggregatedTimeEntries,
            };

            return View(viewModel);
        }

        private List<AggregatedTimeEntry> AggregateTimeEntries(List<RawTimeEntry> entries, TimeSpan interval) {
            var aggregated = new List<AggregatedTimeEntry>();
            var startDate = entries.Min(e => e.Time).Date;

            var groupedEntries = entries
                .GroupBy(e => new DateTime((e.Time.Ticks / interval.Ticks) * interval.Ticks))
                .OrderBy(g => g.Key);

            int cumulativeCount = 0;
            foreach (var group in groupedEntries) {
                var netChange = group.Sum(g => g.Change);
                cumulativeCount += netChange;
                // Console.WriteLine($"Time: {group.Key}, Change: {netChange}, Cumulative: {cumulativeCount}");
                aggregated.Add(new AggregatedTimeEntry {
                    TimeStamp = group.Key,
                    VehicleCount = cumulativeCount
                });
            }


            return aggregated;
        }


        private class RawTimeEntry {
            public DateTime Time { get; set; }
            public int Change { get; set; }
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}


// var timeEntries = await _context.Vehicles
// .SelectMany(v => new {
// new {Time = v.ArrivalTime, VehicleCount = }
// })
// .SelectMany(v => new[] {
// new TimeEntry { TimeStamp = v.ArrivalTime, VehicleCountChange = 1 },
// new TimeEntry { TimeStamp = v.DepartureTime, VehicleCountChange = -1 }
// })
// .Where(te => te.TimeStamp != null)
// .OrderBy(te => te.TimeStamp)
// .ToListAsync();


// return View(viewModel); 


//
// var arrivalTimes = _context.Vehicles
//     .Select(v => new TimeEntry {
//         TimeStamp = v.ArrivalTime,
//         VehicleCountChange = VehicleCountChange.Enter
//     });
//
// var departureTimes = _context.Vehicles
//     .Where(v => v.DepartureTime != null)
//     .Select(v => new TimeEntry {
//         TimeStamp = v.DepartureTime!.Value,
//         VehicleCountChange = VehicleCountChange.Leave
//     });
//
// var timeEntries = await arrivalTimes.Concat(departureTimes)
//     .OrderBy(te => te.TimeStamp)
//     .ToListAsync();
//
//
// var viewModel = new HomeViewModel {
//     VehicleTypeSummaries = typeCounts,
//     TimeEntries = timeEntries,
// };