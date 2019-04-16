namespace AirlinesReservationSystem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Booking_Ticket
    {

       public int Booking_TicketId { get; set; }

        public string UserId { get; set; }

        public int FlightId { get; set; }

        public int PriceId { get; set; }

        public int? DiscountId { get; set; }

        public string PassengerFirstName { get; set; }

        public string PassengerLastName { get; set; }

        public string PassengerNumberId { get; set; }

        [Display(Name = "Choose Seat Number")]
        [Required]
        public int? SeatDetailByFlightId { get; set; }

        public string PassengerEmail { get; set; }

        public string PassengerPhoneNumber { get; set; }

        public int ReservationModId { get; set; }

        public string CodeTicket { get; set; }

        public virtual Flight Flight { get; set; }
        public virtual ReservationMod ReservationMod { get; set; }
    }
}
