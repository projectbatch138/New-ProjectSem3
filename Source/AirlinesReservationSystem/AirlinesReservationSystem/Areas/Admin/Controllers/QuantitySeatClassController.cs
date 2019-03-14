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
    public class QuantitySeatClassController : Controller
    {
        private ReponsitoryQuantitySeatClass _QuantitySeatRepo = new ReponsitoryQuantitySeatClass();
        // GET: QuantitySeatClass
        public ActionResult Index()
        {
            var QuantitySeatClass = _QuantitySeatRepo.SelectAll();
            return View(QuantitySeatClass);
        }

        // GET: QuantitySeatClass/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuantitySeatClass quantityseatclass = _QuantitySeatRepo.SelectById(id);
            if (quantityseatclass == null)
            {
                return HttpNotFound();
            }
            return View(quantityseatclass);
        }

        // GET: QuantitySeatClass/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuantitySeatClass/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuantitySeatClassId,PlaneId,Quantity,SeatClassId")] QuantitySeatClass quantityseatclass)
        {
            if (!ModelState.IsValid)
            {
                return View(quantityseatclass);
            }
            try
            {
                _QuantitySeatRepo.Insert(quantityseatclass);
                _QuantitySeatRepo.Save();
            }
            catch (Exception)
            {
                // todo
            }
            return RedirectToAction("Index");
        }

        // GET: QuantitySeatClass/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuantitySeatClass quantityseatclass = _QuantitySeatRepo.SelectById(id);
            if (quantityseatclass == null)
            {
                return HttpNotFound();
            }
            return View(quantityseatclass);
        }

        // POST: QuantitySeatClass/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuantitySeatClassId,PlaneId,Quantity,SeatClassId")] QuantitySeatClass quantityseatclass)
        {
            if (!ModelState.IsValid)
            {
                return View(quantityseatclass);
            }
            try
            {
                _QuantitySeatRepo.Update(quantityseatclass);
                _QuantitySeatRepo.Save();
            }
            catch (Exception)
            {
                // todo
            }
            return RedirectToAction("Index");
        }

        // GET: QuantitySeatClass/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var quantityseatclass = _QuantitySeatRepo.SelectById(id);
            if (quantityseatclass == null)
            {
                return HttpNotFound();
            }
            return View(quantityseatclass);
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
                _QuantitySeatRepo.Delete(id);
                _QuantitySeatRepo.Save();
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
}