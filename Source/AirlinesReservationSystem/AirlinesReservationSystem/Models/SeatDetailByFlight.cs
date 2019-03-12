namespace AirlinesReservationSystem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SeatDetailByFlight")]
    public partial class SeatDetailByFlight
    {
        public int SeatDetailByFlightId { get; set; }

        public int FlightId { get; set; }

        public int SeatNumberId { get; set; }

        public bool SeatStatus { get; set; }
    }
}
