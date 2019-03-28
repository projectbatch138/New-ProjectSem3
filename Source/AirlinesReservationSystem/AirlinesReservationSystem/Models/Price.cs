namespace AirlinesReservationSystem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Price
    {
        public int PriceId { get; set; }

        public int FlightId { get; set; }

        public int SeatClassId { get; set; }

        [Column("Price")]
        public int? Price1 { get; set; }

        public virtual Flight Flight { get; set; }
        public virtual SeatClass SeatClass { get; set; }
    }
}
