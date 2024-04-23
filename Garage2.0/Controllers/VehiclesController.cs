using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage2._0.Data;
using Garage2._0.Extensions;
using Garage2._0.Models;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

namespace Garage2._0.Controllers {
    public class VehiclesController : Controller {
        private readonly Garage2_0Context _context;
        private  ParkingSpotRepository _parkingSpotRepository;
        public VehiclesController(Garage2_0Context context, ParkingSpotRepository parkingSpotRepository) {
            _context = context;
            _parkingSpotRepository = parkingSpotRepository;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index(string? licencePlateSearch = null) {
            var vehicles = _context.Vehicles.Include(v => v.ParkingSpot);
            List<Vehicle> output;

            if (licencePlateSearch != null) {
                output = await vehicles
                    .WhereActive()
                    .Where(v => v.LicensePlate == licencePlateSearch)
                    .ToListAsync();
            }
            else {
                output = await vehicles
                    .WhereActive()
                    .ToListAsync();
                // output = _parkingSpotRepository.AllParkedVehicles();
            }

            SummaryViewModel m = new SummaryViewModel(output,_parkingSpotRepository.AllParkedVehiclesIndex());

            return View(m);
        }


        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int id) {
            var vehicle = await _context.Vehicles
                .WhereActive()
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (vehicle == null) {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("LicensePlate,VehicleType,Color,Brand,Model,NumberOfWheels")]
            Vehicle vehicle) {

            await ValidateLicensePlateUniqueness(vehicle.LicensePlate!); 
            
            vehicle.ArrivalTime = DateTime.Now;

            // vehicle.LicensePlate
            if (ModelState.IsValid) {
                vehicle = _parkingSpotRepository.onParkVehicle(vehicle)!;
                _context.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(vehicle);
        }
        
        
        private async Task ValidateLicensePlateUniqueness(string licensePlate) {
            var licensePlateExists = await _context.Vehicles
                .WhereActive()
                .AnyAsync(v => v.LicensePlate == licensePlate);

            if (licensePlateExists) {
                ModelState.AddModelError("LicensePlate", "A vehicle with this license plate already exists.");
            }
        }

        
        


        // GET: Vehicles/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // POST: Vehicles/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase) {
            var searchResult = await _context.Vehicles
                .WhereActive()
                .Where(p => p.LicensePlate.Contains(SearchPhrase))
                .ToListAsync();

            var summaryViewModel = new SummaryViewModel(searchResult,_parkingSpotRepository.AllParkedVehiclesIndex());
            
            return View("Index", summaryViewModel);
        }

        // GET: Vehicles/ShowSearchByPropertyForm
        public async Task<IActionResult> ShowSearchByPropertyForm()
        {
            return View();
        }

        // GET: Vehicles/ShowSearchByPropertyFormResults
        public async Task<IActionResult> ShowSearchByPropertyFormResults(VehicleType? vehicleType, string color, string brand, string model, int? numberOfWheels)
        {
            IQueryable<Vehicle> query = _context.Vehicles.WhereActive();

            if (vehicleType.HasValue)
            {
                query = query.Where(v => v.VehicleType == vehicleType.Value);
            }

            if (!string.IsNullOrWhiteSpace(color))
            {
                query = query.Where(v => v.Color.ToLower().Contains(color.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(brand))
            {
                query = query.Where(v => v.Brand.ToLower().Contains(brand.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(model))
            {
                query = query.Where(v => v.Model.ToLower().Contains(model.ToLower()));
            }
            if (numberOfWheels.HasValue)
            {
                query = query.Where(v => v.NumberOfWheels == numberOfWheels.Value);
            }


            var vehicles = await query.ToListAsync();
            var summaryViewModel = new SummaryViewModel(vehicles, _parkingSpotRepository.AllParkedVehiclesIndex());

            return View("Index", summaryViewModel);
        }

     


        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int id) {
            // if (id == null)
            // {
            //     return NotFound();
            // }

            var vehicle = await _context.Vehicles
                .WhereActive()
                .FirstOrDefaultAsync(v => v.VehicleId == id);

            Console.WriteLine("hello WORLD");
            Console.WriteLine(vehicle);

            if (vehicle == null) {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("LicensePlate,VehicleType,Color,Brand,Model,NumberOfWheels")] Vehicle vehicleUpdateModel) {
            var vehicle = await _context.Vehicles
                .WhereActive()
                .FirstOrDefaultAsync(v => v.VehicleId == id);

            if (vehicle == null) {
                return NotFound();
            }

            if (vehicle.LicensePlate != vehicleUpdateModel.LicensePlate) {
                await ValidateLicensePlateUniqueness(vehicleUpdateModel.LicensePlate!); 
            }

            if (!ModelState.IsValid) {
                return View(vehicleUpdateModel);
            }
            
            vehicle.LicensePlate = vehicleUpdateModel.LicensePlate;
            vehicle.VehicleType = vehicleUpdateModel.VehicleType;
            vehicle.Color = vehicleUpdateModel.Color;
            vehicle.Brand = vehicleUpdateModel.Brand;
            vehicle.Model = vehicleUpdateModel.Model;
            vehicle.NumberOfWheels = vehicleUpdateModel.NumberOfWheels;
            
            try {
                _context.Update(vehicle);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!VehicleExists(vehicle.VehicleId)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Vehicles/Delete/5
        //Display a confirmation page to confirm whether the user really wants to delete this item
        public async Task<IActionResult> Delete(int id) {
            // if (id == null)
            // {
            //     return NotFound();
            // }

            var vehicle = await _context.Vehicles
                .WhereActive()
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (vehicle == null) {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        //Handle the actual delete operation
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
        
            var vehicle = await _context.Vehicles
                .WhereActive()
                .FirstOrDefaultAsync(v => v.VehicleId == id);
            if (vehicle == null) {
                return NotFound();
                // _context.Vehicles.Remove(vehicle);
            }

            vehicle.DepartureTime = DateTime.Now;
            var sId = _parkingSpotRepository.onLeaveVehicle(vehicle);
            _context.Update(vehicle);
           if(sId != null)
            {
                var parkingSpot = await _context.ParkingSpots.FindAsync(sId);
                if (parkingSpot != null) {
                    _context.ParkingSpots.Remove(parkingSpot);
             
                }

            }
           
            await _context.SaveChangesAsync();

            return RedirectToAction("Invoice", new {licensePlate = vehicle.LicensePlate, date = vehicle.ArrivalTime});
        }

        private bool VehicleExists(int id) {
            return _context.Vehicles
                .WhereActive()
                .Any(e => e.VehicleId == id);
        }

        public IActionResult Invoice(string licensePlate, DateTime date) {
            InvoiceViewModel m = new InvoiceViewModel(licensePlate, date);
            return View(m);
        }

        //public IActionResult Spots()
        //{
        //    //SpotViewModel m = new SpotViewModel(_parkingSpotRepository.AllParkedVehiclesIndex());
        //    SummaryViewModel m = new SummaryViewModel(_context.Vehicles, _parkingSpotRepository.AllParkedVehiclesIndex());
        //    return View(m);
        //}

        // public IActionResult Summary()
        // {
        //     SummaryViewModel m = new SummaryViewModel(_context.Vehicles);
        //     return View(m);
        // }
    }
}


// _context.Departures.Add( new Departure {
// Vehicle = vehicle, 
// DepartureDateTime = DateTime.Now,  
// }); 


// var vehicles = _context.Vehicles
//     .Where(v => string.IsNullOrEmpty(licencePlateSearch) || v.LicensePlate.Contains(licencePlateSearch))
//     .ToListAsync();
//
// List<Vehicle> output = await vehicles; 


// public async Task<IActionResult> Index() {
//     var output = await _context.Vehicles.ToListAsync();  
//     
//     
//     
//     return View(output);
// }