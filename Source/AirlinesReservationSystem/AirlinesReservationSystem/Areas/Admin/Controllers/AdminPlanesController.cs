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
    [Authorize(Roles = "Admin")]
    public class AdminPlanesController : Controller
    {
        // GET: Planes
        private ReponsitoryModel.ReponsitoryPlane _PlanesRepo = new ReponsitoryModel.ReponsitoryPlane();
        private ReponsitoryAirlines airlines = new ReponsitoryAirlines();
        //private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var Planes = _PlanesRepo.SelectAll();
            return View(Planes);
        }
        // GET: Planes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plane planes = _PlanesRepo.SelectById(id);
            if (planes == null)
            {
                return HttpNotFound();
            }
            return View(planes);
        }

        // GET: planes/Create
        public ActionResult Create()
        {
            ViewBag.AirlineId = new SelectList(airlines.SelectAll(), "AirlineId", "AirlineName");
            return View();
        }

        // POST: Planes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlaneId,PlaneName,AirlineId,Capacity,CodeIATAPlane,Status")] Plane plane)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.AirlineId = new SelectList(airlines.SelectAll(), "AirlineId", "AirlineName");
                return View(plane);
            }
            try
            {
                _PlanesRepo.Insert(plane);
                _PlanesRepo.Save();
            }
            catch (Exception)
            {
                // todo
            }
            
            return RedirectToAction("Index");
        }

        // GET: Planes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plane plane = _PlanesRepo.SelectById(id);
            if (plane == null)
            {
                return HttpNotFound();
            }
            ViewBag.AirlineId = new SelectList(airlines.SelectAll(), "AirlineId", "AirlineName");
            return View(plane);
        }

        // POST: Planes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlaneId,PlaneName,AirlineId,Capacity,CodeIATAPlane,Status")] Plane plane)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.AirlineId = new SelectList(airlines.SelectAll(), "AirlineId", "AirlineName");
                return View(plane);
            }
            try
            {
                _PlanesRepo.Update(plane);
                _PlanesRepo.Save();
            }
            catch (Exception)
            {
                // todo
            }
            return RedirectToAction("Index");
        }

        // GET: Planes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Planes = _PlanesRepo.SelectById(id);
            if (Planes == null)
            {
                return HttpNotFound();
            }
            return View(Planes);
        }

        // POST: Planes/Delete/5
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
                _PlanesRepo.Delete(id);
                _PlanesRepo.Save();
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}