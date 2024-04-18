using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage2._0.Data;
using Garage2._0.Models;

namespace Garage2._0.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly Garage2_0Context _context;

        public VehiclesController(Garage2_0Context context)
        {
            _context = context;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vehicles.ToListAsync());
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .FirstOrDefaultAsync(m => m.LicensePlate == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LicensePlate,VehicleType,Color,Brand,Model,NumberOfWheels,ArrivalTime")] Vehicle vehicle) {

            var searchResult = _context.Vehicles.Any(v => v.LicensePlate == vehicle.LicensePlate);

            if (searchResult) {
                ModelState.AddModelError("LicensePlate", "A vehicle with this license plate already exists.");
            }
            
            // vehicle.LicensePlate
            if (ModelState.IsValid)
            {
                _context.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            // if (id == null)
            // {
            //     return NotFound();
            // }

            var vehicle = await _context.Vehicles.FindAsync(id);

            Console.WriteLine("hello WORLD");
            Console.WriteLine(vehicle);
            
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LicensePlate,VehicleType,Color,Brand,Model,NumberOfWheels,ArrivalTime")] Vehicle vehicle)
        {
            if (id != vehicle.VehicleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.VehicleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            // if (id == null)
            // {
            //     return NotFound();
            // }

            var vehicle = await _context.Vehicles
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //ArgumentException: The key value at position 0 of the call to 'DbSet<Vehicle>.Find' was of type 'string', which does not match the property type of 'int'.
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle != null)
            {
                // _context.Vehicles.Remove(vehicle);
                return RedirectToAction("Invoice", new { licensePlate = vehicle.LicensePlate, date = vehicle.ArrivalTime });
            }

            //await _context.SaveChangesAsync();
            return RedirectToAction("Invoice");
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicles.Any(e => e.VehicleId == id);
        }

        public IActionResult Invoice(string licensePlate, DateTime date)
        {
            InvoiceViewModel m = new InvoiceViewModel(licensePlate, date);
            return View(m);
        } 
    }
}
