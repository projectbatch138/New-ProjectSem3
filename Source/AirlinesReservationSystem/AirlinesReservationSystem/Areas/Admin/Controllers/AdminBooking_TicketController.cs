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
    public class AdminBooking_TicketController : Controller
    {
        private ReponsitoryBookingTicket _BookingTicketRepo = new ReponsitoryBookingTicket();

        // GET: Admin/AdminBooking_Ticket
        public ActionResult Index()
        {
            var BookingTicket = _BookingTicketRepo.SelectAll();
            return View(BookingTicket);
        }

        // GET: Admin/AdminBooking_Ticket/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking_Ticket booking_Ticket = _BookingTicketRepo.SelectById(id);
            if (booking_Ticket == null)
            {
                return HttpNotFound();
            }
            return View(booking_Ticket);
        }

        // GET: Admin/AdminBooking_Ticket/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminBooking_Ticket/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Booking_TicketId,UserId,FlightId,PriceId,DiscountId,DiscountBySeatClassId,DiscountBySkymiles,DiscountByEarly,SeatDetailByFlightId,ReservationModId")] Booking_Ticket booking_Ticket)
        {
            if (ModelState.IsValid)
            {

                return View(booking_Ticket);
            }
            try
            {
                _BookingTicketRepo.Insert(booking_Ticket);
                _BookingTicketRepo.Save();
            }
            catch (Exception)
            {
                // todo
            }
            return RedirectToAction("Index");


        }

        // GET: Admin/AdminBooking_Ticket/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking_Ticket booking_Ticket = _BookingTicketRepo.SelectById(id);
            if (booking_Ticket == null)
            {
                return HttpNotFound();
            }
            return View(booking_Ticket);
        }

        // POST: Admin/AdminBooking_Ticket/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Booking_TicketId,UserId,FlightId,PriceId,DiscountId,DiscountBySeatClassId,DiscountBySkymiles,DiscountByEarly,SeatDetailByFlightId,ReservationModId")] Booking_Ticket booking_Ticket)
        {
            if (ModelState.IsValid)
            {
                return View(booking_Ticket); }

                try
                {
                    _BookingTicketRepo.Update(booking_Ticket);
                    _BookingTicketRepo.Save();
                }
            catch
            {
                //Todo
            }
                return RedirectToAction("Index");
            
            
        }

        // GET: Admin/AdminBooking_Ticket/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking_Ticket booking_Ticket = _BookingTicketRepo.SelectById(id);
            if (booking_Ticket == null)
            {
                return HttpNotFound();
            }
            return View(booking_Ticket);
        }

        // POST: Admin/AdminBooking_Ticket/Delete/5
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
                _BookingTicketRepo.Delete(id);
                _BookingTicketRepo.Save();
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
