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
    public class SeatClassController : Controller
    {
        // GET: SeatClass
        private ReponsitoryModel.ReponsitorySeatClass _SeatClassRepo = new ReponsitoryModel.ReponsitorySeatClass();
        public ActionResult Index()
        {
            var SeatClass = _SeatClassRepo.SelectAll();
            return View(SeatClass);
        }
        // GET: SeatClass/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeatClass SeatClass = _SeatClassRepo.SelectById(id);
            if (SeatClass == null)
            {
                return HttpNotFound();
            }
            return View(SeatClass);
        }

        // GET: SeatClass/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SeatClass/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SeatClassId,SeatClassName")] SeatClass SeatClass)
        {
            if (!ModelState.IsValid)
            {
                return View(SeatClass);
            }
            try
            {
                _SeatClassRepo.Insert(SeatClass);
                _SeatClassRepo.Save();
            }
            catch (Exception)
            {
                // todo
            }
            return RedirectToAction("Index");
        }

        // GET: SeatClass/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeatClass SeatClass = _SeatClassRepo.SelectById(id);
            if (SeatClass == null)
            {
                return HttpNotFound();
            }
            return View(SeatClass);
        }

        // POST: SeatClass/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SeatClassId,SeatClassName")] SeatClass SeatClass)
        {
            if (!ModelState.IsValid)
            {
                return View(SeatClass);
            }
            try
            {
                _SeatClassRepo.Update(SeatClass);
                _SeatClassRepo.Save();
            }
            catch (Exception)
            {
                // todo
            }
            return RedirectToAction("Index");
        }

        // GET: SeatClass/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var SeatClass = _SeatClassRepo.SelectById(id);
            if (SeatClass == null)
            {
                return HttpNotFound();
            }
            return View(SeatClass);
        }

        // POST: SeatClass/Delete/5
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
                _SeatClassRepo.Delete(id);
                _SeatClassRepo.Save();
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}