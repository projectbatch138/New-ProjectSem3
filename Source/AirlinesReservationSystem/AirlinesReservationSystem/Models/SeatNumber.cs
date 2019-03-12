namespace AirlinesReservationSystem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SeatNumber
    {
        public int SeatNumberId { get; set; }

        [Column("SeatNumber")]
        public int SeatNumber1 { get; set; }

        public int SeatClassId { get; set; }

        public int PlaneId { get; set; }
    }
}
