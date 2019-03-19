using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AirlinesReservationSystem;
using AirlinesReservationSystem.ReponsitoryModel;

namespace AirlinesReservationSystem.Areas.Admin.Controllers
{
    public class SeatDetailByFlightController : Controller
    {
        // GET: SeatDetailByFlight
        private ReponsitoryModel.ReponsitorySeatDetailByFlight _SeatDetailByFlightRepo = new ReponsitoryModel.ReponsitorySeatDetailByFlight();
        public ActionResult Index()
        {
            var SeatDetailByFlight = _SeatDetailByFlightRepo.SelectAll();
            return View(SeatDetailByFlight);
        }
        // GET: SeatDetailByFlight/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeatDetailByFlight SeatDetailByFlight = _SeatDetailByFlightRepo.SelectById(id);
            if (SeatDetailByFlight == null)
            {
                return HttpNotFound();
            }
            return View(SeatDetailByFlight);
        }

        // GET: SeatDetailByFlight/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SeatDetailByFlight/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SeatDetailByFlightId,FlightId,SeatNumberId,SeatStatus")] SeatDetailByFlight SeatDetailByFlight)
        {
            if (!ModelState.IsValid)
            {
                return View(SeatDetailByFlight);
            }
            try
            {
                _SeatDetailByFlightRepo.Insert(SeatDetailByFlight);
                _SeatDetailByFlightRepo.Save();
            }
            catch (Exception)
            {
                // todo
            }
            return RedirectToAction("Index");
        }

        // GET: SeatDetailByFlight/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeatDetailByFlight SeatDetailByFlight = _SeatDetailByFlightRepo.SelectById(id);
            if (SeatDetailByFlight == null)
            {
                return HttpNotFound();
            }
            return View(SeatDetailByFlight);
        }

        // POST: SeatDetailByFlight/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SeatDetailByFlightId,FlightId,SeatNumberId,SeatStatus")] SeatDetailByFlight SeatDetailByFlight)
        {
            if (!ModelState.IsValid)
            {
                return View(SeatDetailByFlight);
            }
            try
            {
                _SeatDetailByFlightRepo.Update(SeatDetailByFlight);
                _SeatDetailByFlightRepo.Save();
            }
            catch (Exception)
            {
                // todo
            }
            return RedirectToAction("Index");
        }

        // GET: SeatDetailByFlight/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var SeatDetailByFlight = _SeatDetailByFlightRepo.SelectById(id);
            if (SeatDetailByFlight == null)
            {
                return HttpNotFound();
            }
            return View(SeatDetailByFlight);
        }

        // POST: SeatDetailByFlight/Delete/5
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
                _SeatDetailByFlightRepo.Delete(id);
                _SeatDetailByFlightRepo.Save();
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}