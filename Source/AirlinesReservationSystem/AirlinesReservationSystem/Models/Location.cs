namespace AirlinesReservationSystem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Location
    {
        public Location()
        {
            Airports = new HashSet<Airports>();
        }
        [Key]
        [Required]
        public int LocationId { get; set; }

        [Required]
        [StringLength(50)]
        public string Country { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        [StringLength(3)]
        public string CountryIATACode { get; set; }

        [StringLength(6)]
        public string CityIATACode { get; set; }

        public virtual ICollection<Airports> Airports { get; set; }
    }
}
