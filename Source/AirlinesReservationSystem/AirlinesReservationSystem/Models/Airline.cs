namespace AirlinesReservationSystem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Airline
    {
        public int AirlineId { get; set; }

        [Required]
        [StringLength(50)]
        public string AirlineName { get; set; }

        [StringLength(150)]
        public string LogoImage { get; set; }

        [StringLength(250)]
        public string Slogan { get; set; }
    }
}
