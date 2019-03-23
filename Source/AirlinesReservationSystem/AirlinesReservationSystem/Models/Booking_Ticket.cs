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

        public int UserId { get; set; }

        public int FlightId { get; set; }

        public int PriceId { get; set; }

        public int? DiscountId { get; set; }

        public int? DiscountBySeatClassId { get; set; }

        public int? DiscountBySkymiles { get; set; }

        public int? DiscountByEarly { get; set; }

        public int? SeatDetailByFlightId { get; set; }

        public int ReservationModId { get; set; }
    }
}
