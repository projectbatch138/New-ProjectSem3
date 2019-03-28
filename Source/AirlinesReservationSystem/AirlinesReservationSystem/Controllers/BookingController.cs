using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AirlinesReservationSystem.Controllers
{
    public class BookingController : Controller
    {
        private Areas.Admin.ReponsitoryModel.ReponsitoryFlights _flight = new Areas.Admin.ReponsitoryModel.ReponsitoryFlights();
        private Areas.Admin.ReponsitoryModel.ReponsitorySeatClass _seatclass = new Areas.Admin.ReponsitoryModel.ReponsitorySeatClass();
        private Areas.Admin.ReponsitoryModel.ReponsitorySeatDetailByFlight _SeatNumber = new Areas.Admin.ReponsitoryModel.ReponsitorySeatDetailByFlight();
        private Areas.Admin.ReponsitoryModel.ReponsitoryBookingTicket _booking = new Areas.Admin.ReponsitoryModel.ReponsitoryBookingTicket();

        //Get : Booking
        public ActionResult Booking(int? id, int seatclassid)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = new Flight();
            flight = _flight.SelectById(id);
            ViewBag.flight = flight;
            if (flight == null)
            {
                return HttpNotFound();
            }
            SeatClass seatclass = new SeatClass();
            seatclass = _seatclass.SelectById(seatclassid);
            ViewBag.SeatClass = seatclass;
            var seatbyflight = _SeatNumber.SelectAll().Where(x => x.FlightId == id)
                .Where(x => x.SeatStatus == true).Where(x => x.SeatNumber.SeatClassId == seatclassid);
            TempData["DataSeat"] = new SelectList(seatbyflight, "SeatDetailByFlightId", "SeatNumber.SeatNo");
            return View();
        }
        [HttpPost]
        public ActionResult Booking(int id, int seatclassid,int DiscountId, int PriceId, [Bind(Include = "Booking_TicketId,PassengerFirstName,PassengerLastName,PassengerNumberId,SeatDetailByFlightId,PassengerEmail,PassengerPhoneNumber")] Booking_Ticket booking)
        {
            booking.FlightId = id;
            booking.DiscountId = DiscountId;
            booking.PriceId = PriceId;
            booking.ReservationModId = 1;
            booking.UserId = 1;
            _booking.Insert(booking);
            SeatDetailByFlight seat = new SeatDetailByFlight();
            seat= _SeatNumber.SelectById(booking.SeatDetailByFlightId);
            seat.SeatStatus = false;
            _SeatNumber.Update(seat);
            _SeatNumber.Save();
            _booking.Save();
           return RedirectToAction("index","Home");
        }
    }
}