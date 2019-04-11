using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlinesReservationSystem.ViewModels
{
    public class BookingViewModel
    {
        public int Flightid { get; set; }

        public int RouterId { get; set; }

        public DateTime Dept_Time { get; set; }

        public DateTime Arr_Time { get; set; }

        public int? Status { get; set; }

        public int PlaneId { get; set; }

        public int Booking_TicketId { get; set; }

        public string UserId { get; set; }

        public int PriceId { get; set; }

        public int? DiscountId { get; set; }

        public int SeatClassId { get; set; }

        public string PassengerFirstName { get; set; }

        public string PassengerLastName { get; set; }

        public string PassengerNumberId { get; set; }

        public int? SeatDetailByFlightId { get; set; }

        public string PassengerEmail { get; set; }

        public string PassengerPhoneNumber { get; set; }

        public int ReservationModId { get; set; }
    }
}