using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AirlinesReservationSystem.ViewModels
{
    public class PaymentViewModel
    {
        [Key]
        public int Booking_TicketId { get; set; }
        public int FlightID { get; set; }
        // public int LocationId { get; set; }

        [Display(Name = "Depart")]
        public string Location_Depart { get; set; }
        [Required]
        [Display(Name = "Arrival")]
        public string Location_Arrival { get; set; }
        [Required]
        [Display(Name = "Start")]
        public DateTime Dept_Time { get; set; }
        [Display(Name = "End")]
        public DateTime Arr_Time { get; set; }

        public int Price { get; set; }

        public int? Discount { get; set; }

        public int Total { get; set; }
        [Display(Name = "FirstName")]
        public string PassengerFirstName { get; set; }
        [Display(Name = "LastName")]
        public string PassengerLastName { get; set; }
        [Display(Name = "CMND")]
        public string PassengerNumberId { get; set; }
        [Display(Name = "SeatId")]
        public int? SeatDetailByFlightId { get; set; }
        [Display(Name = "SeatNumber")]
        public int SeatNumber { get; set; }
        [Display(Name = "Email")]
        public string PassengerEmail { get; set; }
        [Display(Name = "Phone")]
        public string PassengerPhoneNumber { get; set; }
    }
}