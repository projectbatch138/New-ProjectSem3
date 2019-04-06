using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AirlinesReservationSystem.ViewModels;
using Microsoft.AspNet.Identity;

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
            booking.CodeTicket = booking.FlightId.ToString() + booking.Booking_TicketId.ToString();
            booking.UserId = HttpContext.User.Identity.GetUserId();
            _booking.Insert(booking);
            SeatDetailByFlight seat = new SeatDetailByFlight();
            seat= _SeatNumber.SelectById(booking.SeatDetailByFlightId);
            seat.SeatStatus = false;
            _SeatNumber.Update(seat);
            _SeatNumber.Save();
            _booking.Save();
            TempData["Code"] = booking.CodeTicket;
            TempData["Email"] = booking.PassengerEmail.ToString();
                return RedirectToAction("Contact");
           
        }


        public async Task<ActionResult> Contact()
        {
            if (ModelState.IsValid)
            {
                var codebooking = TempData["Code"];
                string EmailPas = TempData["Email"].ToString();
                var body = "<p>Email From: {0} ({1})</p><p>Thank you for booking ticket from our airline . :</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress(EmailPas));
                message.Subject = "Booking Airline";
                message.Body = string.Format(body, "ASPAirline: ", "aspairline@gmail.com", " Your code: " + codebooking);
                message.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                    await smtp.SendMailAsync(message);
                }
            }
            return RedirectToAction("Payment","Manage");
        }

       
    }
}