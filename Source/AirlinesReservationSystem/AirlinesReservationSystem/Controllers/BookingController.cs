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
        private readonly Areas.Admin.ReponsitoryModel.ReponsitoryFlights _flight = new Areas.Admin.ReponsitoryModel.ReponsitoryFlights();
        private readonly Areas.Admin.ReponsitoryModel.ReponsitorySeatClass _seatclass = new Areas.Admin.ReponsitoryModel.ReponsitorySeatClass();
        private readonly Areas.Admin.ReponsitoryModel.ReponsitorySeatDetailByFlight _SeatNumber = new Areas.Admin.ReponsitoryModel.ReponsitorySeatDetailByFlight();
        private readonly Areas.Admin.ReponsitoryModel.ReponsitoryBookingTicket _booking = new Areas.Admin.ReponsitoryModel.ReponsitoryBookingTicket();

        //Get : Booking
        public ActionResult Booking()
        {
            TempData["Depart"] = TempData["Depart"];
            if (TempData["Arrivalflight"] != null)
            {
                TempData["Arrivalflight"] = TempData["Arrivalflight"];
            }

            return View();
        }


        [HttpPost]
        public ActionResult Booking([Bind(Include = "Booking_TicketId,PassengerFirstName,PassengerLastName,PassengerNumberId,PassengerEmail,PassengerPhoneNumber")] Booking_Ticket[] bookings)
        {
            int i = 0;
            BookingViewModel Departflight = new BookingViewModel();

            Departflight = (BookingViewModel)TempData["Depart"];

            foreach (var booking in bookings)
            {
                booking.FlightId = Departflight.Flightid;
                booking.DiscountId = Departflight.DiscountId;
                booking.PriceId = Departflight.PriceId;
                booking.ReservationModId = 1;
                booking.CodeTicket = booking.FlightId.ToString() + booking.Booking_TicketId.ToString();
                booking.UserId = HttpContext.User.Identity.GetUserId();
                _booking.Insert(booking);
                TempData["CodeDepart" + i] = booking.CodeTicket;
                if (TempData["Arrivalflight"] != null)
                {
                    BookingViewModel Arrivalflight = new BookingViewModel();
                    Arrivalflight = (BookingViewModel)TempData["Arrivalflight"];

                    booking.FlightId = Arrivalflight.Flightid;
                    booking.DiscountId = Arrivalflight.DiscountId;
                    booking.PriceId = Arrivalflight.PriceId;
                    booking.CodeTicket = booking.FlightId.ToString() + booking.Booking_TicketId.ToString();
                    _booking.Insert(booking);
                    TempData["CodeArrival" + i] = booking.CodeTicket;
                }

                //_SeatNumber.Update(seat);
                //_SeatNumber.Save();
                //_booking.Save();

                TempData["Email" + i] = booking.PassengerEmail.ToString();
                i++;
            }


            TempData["Sum"] = i;
            return RedirectToAction("Contact");

        }


        public async Task<ActionResult> Contact()
        {
            if (ModelState.IsValid)
            {

                for (int i = 0; i < (int)TempData["Sum"]; i++)
                {
                    var codebooking = TempData["CodeDepart" + i];
                    string EmailPas = TempData["Email" + i].ToString();
                    var body = "<p>Email From: {0} ({1})</p><p>Thank you for booking ticket from our airline . :</p><p>{2}</p>";
                    var message = new MailMessage();
                    message.To.Add(new MailAddress(EmailPas));
                    message.Subject = "Booking Airline";
                    message.Body = string.Format(body, "ASPAirline: ", "aspairline@gmail.com", " Your Depart Flight code: " + codebooking);
                    if (TempData["CodeArrival" + i] != null)
                    {
                        var codebookingArr = TempData["CodeArrival" + i];
                        message.Body = string.Format(body, "ASPAirline: ", "aspairline@gmail.com", " Your Depart Flight code: " + codebooking + " Your Arrival Flight code: " + codebookingArr);
                    }
                    message.IsBodyHtml = true;
                    using (var smtp = new SmtpClient())
                    {
                        await smtp.SendMailAsync(message);
                    }
                }

            }
            return RedirectToAction("Payment", "Manage");
        }


    }
}