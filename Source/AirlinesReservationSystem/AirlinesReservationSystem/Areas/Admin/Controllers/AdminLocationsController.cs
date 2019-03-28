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
    public class AdminLocationsController : Controller
    {
        private ReponsitoryModel.ReponsitoryLocations _LocationRepo = new ReponsitoryModel.ReponsitoryLocations();
        // GET: Locations
        public ActionResult Index()
        {
            var locations = _LocationRepo.SelectAll();
            return View(locations);
        }

        // GET: Locations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location locations = _LocationRepo.SelectById(id);
            if (locations == null)
            {
                return HttpNotFound();
            }
            return View(locations);
        }

        // GET: Locations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LocationId,Country,City,State,CountryIATACode,CityIATACode")] Location locations)
        {
            if (!ModelState.IsValid)
            {
                return View(locations);
            }
            try
            {
                _LocationRepo.Insert(locations);
                _LocationRepo.Save();
            }
            catch (Exception)
            {
                // todo
            }
            return RedirectToAction("Index");
        }

        // GET: Locations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location locations = _LocationRepo.SelectById(id);
            if (locations == null)
            {
                return HttpNotFound();
            }
            return View(locations);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LocationId,Country,City,State,CountryIATACode,CityIATACode")] Location locations)
        {
            if (!ModelState.IsValid)
            {
                return View(locations);
            }
            try
            {
                _LocationRepo.Update(locations);
                _LocationRepo.Save();
            }
            catch (Exception)
            {
                // todo
            }
            return RedirectToAction("Index");
        }

        // GET: Locations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Airlines = _LocationRepo.SelectById(id);
            if (Airlines == null)
            {
                return HttpNotFound();
            }
            return View(Airlines);
        }

        // POST: Locations/Delete/5
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
                _LocationRepo.Delete(id);
                _LocationRepo.Save();
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}