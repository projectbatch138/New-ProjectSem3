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
    public class SeatNumbersController : Controller
    {
        // GET: SeatNumbers
        private ReponsitoryModel.ReponsitorySeatNumber _SeatNumbersRepo = new ReponsitoryModel.ReponsitorySeatNumber();
        public ActionResult Index()
        {
            var SeatNumbers = _SeatNumbersRepo.SelectAll();
            return View(SeatNumbers);
        }
        // GET: SeatNumbers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeatNumber SeatNumbers = _SeatNumbersRepo.SelectById(id);
            if (SeatNumbers == null)
            {
                return HttpNotFound();
            }
            return View(SeatNumbers);
        }

        // GET: SeatNumbers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SeatNumbers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SeatNumberId,SeatNumber1,SeatClassId,PlaneId")] SeatNumber SeatNumber)
        {
            if (!ModelState.IsValid)
            {
                return View(SeatNumber);
            }
            try
            {
                _SeatNumbersRepo.Insert(SeatNumber);
                _SeatNumbersRepo.Save();
            }
            catch (Exception)
            {
                // todo
            }
            return RedirectToAction("Index");
        }

        // GET: SeatNumbers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeatNumber SeatNumber = _SeatNumbersRepo.SelectById(id);
            if (SeatNumber == null)
            {
                return HttpNotFound();
            }
            return View(SeatNumber);
        }

        // POST: SeatNumbers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SeatNumberId,SeatNumber1,SeatClassId,PlaneId")] SeatNumber SeatNumber)
        {
            if (!ModelState.IsValid)
            {
                return View(SeatNumber);
            }
            try
            {
                _SeatNumbersRepo.Update(SeatNumber);
                _SeatNumbersRepo.Save();
            }
            catch (Exception)
            {
                // todo
            }
            return RedirectToAction("Index");
        }

        // GET: SeatNumbers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var SeatNumbers = _SeatNumbersRepo.SelectById(id);
            if (SeatNumbers == null)
            {
                return HttpNotFound();
            }
            return View(SeatNumbers);
        }

        // POST: SeatNumbers/Delete/5
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
                _SeatNumbersRepo.Delete(id);
                _SeatNumbersRepo.Save();
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}