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
    public class RoutersController : Controller
    {
        // GET: Routers
        private ReponsitoryModel.ReponsitoryRouter _RoutersRepo = new ReponsitoryModel.ReponsitoryRouter();
        public ActionResult Index()
        {
            var Routers = _RoutersRepo.SelectAll();
            return View(Routers);
        }
        // GET: Routers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Router Routers = _RoutersRepo.SelectById(id);
            if (Routers == null)
            {
                return HttpNotFound();
            }
            return View(Routers);
        }

        // GET: Routers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Routers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RouterId,Depart,Arrival,Distances")] Router Router)
        {
            if (!ModelState.IsValid)
            {
                return View(Router);
            }
            try
            {
                _RoutersRepo.Insert(Router);
                _RoutersRepo.Save();
            }
            catch (Exception)
            {
                // todo
            }
            return RedirectToAction("Index");
        }

        // GET: Routers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Router Router = _RoutersRepo.SelectById(id);
            if (Router == null)
            {
                return HttpNotFound();
            }
            return View(Router);
        }

        // POST: Routers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RouterId,Depart,Arrival,Distances")] Router Router)
        {
            if (!ModelState.IsValid)
            {
                return View(Router);
            }
            try
            {
                _RoutersRepo.Update(Router);
                _RoutersRepo.Save();
            }
            catch (Exception)
            {
                // todo
            }
            return RedirectToAction("Index");
        }

        // GET: Routers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Routers = _RoutersRepo.SelectById(id);
            if (Routers == null)
            {
                return HttpNotFound();
            }
            return View(Routers);
        }

        // POST: Routers/Delete/5
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
                _RoutersRepo.Delete(id);
                _RoutersRepo.Save();
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}