using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AirlinesReservationSystem;
using AirlinesReservationSystem.Areas.Admin.ReponsitoryModel;

namespace AirlinesReservationSystem.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminFlightsController : Controller
    {
        private ReponsitoryFlights _FlightRepo = new ReponsitoryFlights();
        private ReponsitoryRouter _RouterRepo = new ReponsitoryRouter();
        private ReponsitoryPlane _PlaneRepo = new ReponsitoryPlane();
        private ReponsitorySeatNumber _SeatNumber = new ReponsitorySeatNumber();
        private ReponsitorySeatDetailByFlight _SeatDetailByFlight = new ReponsitorySeatDetailByFlight();

        // GET: Admin/AdminFlights
        public ActionResult Index()
        {
            var Flights = _FlightRepo.SelectAll();
            return View(Flights);
        }

        // GET: Admin/AdminFlights/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = _FlightRepo.SelectById(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        // GET: Admin/AdminFlights/Create
        public ActionResult Create()
        {
            ViewBag.DataRouter = new SelectList(_RouterRepo.SelectAll(), "RouterId", "RouterName");
            ViewBag.DataPlane = new SelectList(_PlaneRepo.SelectAll(), "PlaneId", "PlaneName");
            return View();
        }

        // POST: Admin/AdminFlights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Flightid,RouterId,Dept_Time,Arr_Time,Status,PlaneId")] Flight flight)
        {
            
            
                if (!ModelState.IsValid)
                {
                    ViewBag.DataRouter = new SelectList(_RouterRepo.SelectAll(), "RouterId", "RouterName");
                    ViewBag.DataPlane = new SelectList(_PlaneRepo.SelectAll(), "PlaneId", "PlaneName");
                    return View(flight);
                }
            try
            {
                _FlightRepo.Insert(flight);
                _FlightRepo.Save();
                var seatnumber = _SeatNumber.SelectAll().Where(x => x.PlaneId == flight.PlaneId);
                foreach (SeatNumber s in seatnumber)
                {
                    SeatDetailByFlight seat = new SeatDetailByFlight
                    {
                        FlightId = flight.Flightid,
                        SeatStatus = true
                    };
                    seat.SeatNumberId = s.SeatNumberId;
                    _SeatDetailByFlight.Insert(seat);
                }
               
                _SeatDetailByFlight.Save();
               
            }
            catch (Exception)
            {
                // todo
            }
            return RedirectToAction("Index");

        }

        // GET: Admin/AdminFlights/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = _FlightRepo.SelectById(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            ViewBag.DataRouter = new SelectList(_RouterRepo.SelectAll(), "RouterId", "RouterName");
            ViewBag.DataPlane = new SelectList(_PlaneRepo.SelectAll(), "PlaneId", "PlaneName");
            return View(flight);
        }

        // POST: Admin/AdminFlights/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Flightid,RouterId,Dept_Time,Arr_Time,Status,PlaneId")] Flight flight)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.DataRouter = new SelectList(_RouterRepo.SelectAll(), "RouterId", "RouterName");
                ViewBag.DataPlane = new SelectList(_PlaneRepo.SelectAll(), "PlaneId", "PlaneName");
                return View(flight);
            }

            try
            {
                _FlightRepo.Update(flight);
                _FlightRepo.Save();
            }
            catch
            {
                //Todo
            }
            return RedirectToAction("Index");
        }

        // GET: Admin/AdminFlights/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = _FlightRepo.SelectById(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        // POST: Admin/AdminFlights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            try
            {
                _FlightRepo.Delete(id);
                _FlightRepo.Save();
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
