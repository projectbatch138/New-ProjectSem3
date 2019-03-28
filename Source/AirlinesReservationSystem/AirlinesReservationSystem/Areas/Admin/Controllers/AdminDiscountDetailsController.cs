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
    [Authorize(Roles = "Admin")]
    public class AdminDiscountDetailsController : Controller
    {
        private ReponsitoryDiscountDetail _DiscountRepo = new ReponsitoryDiscountDetail();

        // GET: Admin/AdminDiscountDetails
        public ActionResult Index()
        {
            var discountDetail = _DiscountRepo.SelectAll();
            return View(discountDetail);
        }

        // GET: Admin/AdminDiscountDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiscountDetail discountDetail = _DiscountRepo.SelectById(id);
            if (discountDetail == null)
            {
                return HttpNotFound();
            }
            return View(discountDetail);
        }

        // GET: Admin/AdminDiscountDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminDiscountDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DiscountDetailId,FlightId,TypeDiscountId,CreatedDate,modifyDate")] DiscountDetail discountDetail)
        {
            if (ModelState.IsValid)
            {

                return View(discountDetail);
            }
            try
            {
                _DiscountRepo.Insert(discountDetail);
                _DiscountRepo.Save();
            }
            catch (Exception)
            {
                // todo
            }
            return RedirectToAction("Index");
        }

        // GET: Admin/AdminDiscountDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiscountDetail discountDetail = _DiscountRepo.SelectById(id);
            if (discountDetail == null)
            {
                return HttpNotFound();
            }
            return View(discountDetail);
        }

        // POST: Admin/AdminDiscountDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DiscountDetailId,FlightId,TypeDiscountId,CreatedDate,modifyDate")] DiscountDetail discountDetail)
        {
            if (ModelState.IsValid)
            {
                return View(discountDetail);
            }

            try
            {
                _DiscountRepo.Update(discountDetail);
                _DiscountRepo.Save();
            }
            catch
            {
                //Todo
            }
            return RedirectToAction("Index");
        }

        // GET: Admin/AdminDiscountDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiscountDetail discountDetail = _DiscountRepo.SelectById(id);
            if (discountDetail == null)
            {
                return HttpNotFound();
            }
            return View(discountDetail);
        }

        // POST: Admin/AdminDiscountDetails/Delete/5
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
                _DiscountRepo.Delete(id);
                _DiscountRepo.Save();
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

      
    }
}
