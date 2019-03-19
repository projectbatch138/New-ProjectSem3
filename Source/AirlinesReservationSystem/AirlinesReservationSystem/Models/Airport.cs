namespace AirlinesReservationSystem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Airport
    {
        public int AirportId { get; set; }

        [Required]
        [StringLength(50)]
        public string AirportName { get; set; }

        [StringLength(6)]
        public string AirportIATACode { get; set; }

        public int LocationId { get; set; }
    }
}
