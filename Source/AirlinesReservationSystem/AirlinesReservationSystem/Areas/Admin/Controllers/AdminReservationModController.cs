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
    public class AdminReservationModController : Controller
    {
        private ReponsitoryReservationMod _ReservationMod = new ReponsitoryReservationMod();
        // GET: ReservationMod
        public ActionResult Index()
        {
            var reservationmod = _ReservationMod.SelectAll();
            return View(reservationmod);
        }

        // GET: ReservationMod/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservationMod reservationmod = _ReservationMod.SelectById(id);
            if (reservationmod == null)
            {
                return HttpNotFound();
            }
            return View(reservationmod);
        }

        // GET: ReservationMod/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuantitySeatClass/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReservationModId,ReservationModName")] ReservationMod reservationmod)
        {
            if (!ModelState.IsValid)
            {
                return View(reservationmod);
            }
            try
            {
                _ReservationMod.Insert(reservationmod);
                _ReservationMod.Save();
            }
            catch (Exception)
            {
                // todo
            }
            return RedirectToAction("Index");
        }

        // GET: ReservationMod/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservationMod reservationmod = _ReservationMod.SelectById(id);
            if (reservationmod == null)
            {
                return HttpNotFound();
            }
            return View(reservationmod);
        }

        // POST: QuantitySeatClass/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservationModId,ReservationModName")] ReservationMod reservationmod)
        {
            if (!ModelState.IsValid)
            {
                return View(reservationmod);
            }
            try
            {
                _ReservationMod.Update(reservationmod);
                _ReservationMod.Save();
            }
            catch (Exception)
            {
                // todo
            }
            return RedirectToAction("Index");
        }

        // GET: ReservationMod/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var reservationmod = _ReservationMod.SelectById(id);
            if (reservationmod == null)
            {
                return HttpNotFound();
            }
            return View(reservationmod);
        }

        // POST: QuantitySeatClass/Delete/5
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
                _ReservationMod.Delete(id);
                _ReservationMod.Save();
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
}