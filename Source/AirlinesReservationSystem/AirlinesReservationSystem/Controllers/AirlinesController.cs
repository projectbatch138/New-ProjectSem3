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

namespace AirlinesReservationSystem.Controllers
{
    public class AirlinesController : Controller
    {
        private ReponsitoryAirlines _AirlinesRepo = new ReponsitoryAirlines();
        // GET: Airlines
        public ActionResult Index()
        {
            var Airlines = _AirlinesRepo.SelectAll();
            return View(Airlines);
        }

        // GET: Airlines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Airline airline = _AirlinesRepo.SelectById(id);
            if (airline == null)
            {
                return HttpNotFound();
            }
            return View(airline);
        }

        // GET: Airlines/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Airlines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AirlineId,AirlineName,LogoImage,Slogan")] Airline airline)
        {
            if (!ModelState.IsValid)
            {
                return View(airline);
            }
            try
            {
                _AirlinesRepo.Insert(airline);
                _AirlinesRepo.Save();
            }
            catch (Exception)
            {
                // todo
            }
            return RedirectToAction("Index");
        }

        // GET: Airlines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Airline airline = _AirlinesRepo.SelectById(id);
            if (airline == null)
            {
                return HttpNotFound();
            }
            return View(airline);
        }

        // POST: Airlines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AirlineId,AirlineName,LogoImage,Slogan")] Airline airline)
        {
            if (!ModelState.IsValid)
            {
                return View(airline);
            }
            try
            {
                _AirlinesRepo.Update(airline);
                _AirlinesRepo.Save();
            }
            catch (Exception)
            {
                // todo
            }
            return RedirectToAction("Index");
        }

        // GET: Airlines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Airlines = _AirlinesRepo.SelectById(id);
            if (Airlines == null)
            {
                return HttpNotFound();
            }
            return View(Airlines);
        }

        // POST: Airlines/Delete/5
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
                _AirlinesRepo.Delete(id);
                _AirlinesRepo.Save();
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
}
