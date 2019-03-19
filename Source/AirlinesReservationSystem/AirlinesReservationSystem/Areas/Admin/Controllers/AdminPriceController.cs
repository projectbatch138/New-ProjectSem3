using AirlinesReservationSystem.Areas.Admin.ReponsitoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AirlinesReservationSystem.Areas.Admin.Controllers
{
    public class AdminPriceController : Controller
    {
        private ReponsitoryPrices _PricesRepo = new ReponsitoryPrices();
        // GET: Admin/AdminPrice
        public ActionResult Index()
        {
            var Prices = _PricesRepo.SelectAll();
            return View(Prices);
        }
        // GET: Prices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Price price = _PricesRepo.SelectById(id);
            if (price == null)
            {
                return HttpNotFound();
            }
            return View(price);
        }

        // GET: Prices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Prices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PriceId,FlightId,SeatClassId,Price1")] Price price)
        {
            if (!ModelState.IsValid)
            {
                return View(price);
            }
            try
            {
                _PricesRepo.Insert(price);
                _PricesRepo.Save();
            }
            catch (Exception)
            {
                // todo
            }
            return RedirectToAction("Index");
        }

        // GET: Prices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Price price = _PricesRepo.SelectById(id);
            if (price == null)
            {
                return HttpNotFound();
            }
            return View(price);
        }

        // POST: Prices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PriceId,FlightId,SeatClassId,Price1")] Price price)
        {
            if (!ModelState.IsValid)
            {
                return View(price);
            }
            try
            {
                _PricesRepo.Update(price);
                _PricesRepo.Save();
            }
            catch (Exception)
            {
                // todo
            }
            return RedirectToAction("Index");
        }

        // GET: Prices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Prices = _PricesRepo.SelectById(id);
            if (Prices == null)
            {
                return HttpNotFound();
            }
            return View(Prices);
        }

        // POST: Prices/Delete/5
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
                _PricesRepo.Delete(id);
                _PricesRepo.Save();
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
}