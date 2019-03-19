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
    public class AdminAirportsController : Controller
    {
        private ReponsitoryAirports _AirportRepo = new ReponsitoryAirports();
        private ReponsitoryLocations _LocationRepo = new ReponsitoryLocations();

        // GET: Admin/AdminAirports
        public ActionResult Index()
        {
            var Airports = _AirportRepo.SelectAll();
            return View(Airports);
        }

        // GET: Admin/AdminAirports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Airports airports = _AirportRepo.SelectById(id);
            if (airports == null)
            {
                return HttpNotFound();
            }
            return View(airports);
        }
    

        // GET: Admin/AdminAirports/Create
        public ActionResult Create()
        {
            ViewBag.LocationId = new SelectList(_LocationRepo.SelectAll(), "LocationId", "City");
            return View();
        }

        // POST: Admin/AdminAirports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AirportId,AirportName,AirportIATACode,LocationId")] Airports airports)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.LocationId = new SelectList(_LocationRepo.SelectAll(), "LocationId", "City");
                return View(airports);
            }
            try
            {
                _AirportRepo.Insert(airports);
                _AirportRepo.Save();
            }
            catch (Exception)
            {
                // todo
            }
            return RedirectToAction("Index");
        }

        // GET: Admin/AdminAirports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Airports airports = _AirportRepo.SelectById(id);
            if (airports == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationId = new SelectList(_LocationRepo.SelectAll(), "LocationId", "City");
            return View(airports);
        }

        // POST: Admin/AdminAirports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AirportId,AirportName,AirportIATACode,LocationId")] Airports airports)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.LocationId = new SelectList(_LocationRepo.SelectAll(), "LocationId", "City");
                return View(airports);
            }

            try
            {
                _AirportRepo.Update(airports);
                _AirportRepo.Save();
            }
            catch
            {
                //Todo
            }
            return RedirectToAction("Index");
        }

        // GET: Admin/AdminAirports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Airports airports = _AirportRepo.SelectById(id);
            if (airports == null)
            {
                return HttpNotFound();
            }
            return View(airports);
        }

        // POST: Admin/AdminAirports/Delete/5
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
                _AirportRepo.Delete(id);
                _AirportRepo.Save();
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

       
    }
}
