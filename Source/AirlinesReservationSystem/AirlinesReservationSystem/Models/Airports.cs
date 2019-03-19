namespace AirlinesReservationSystem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Airports
    {
        
        public Airports()
        {
            Routers = new HashSet<Router>();
            Routers1 = new HashSet<Router>();
        }
        
        [Key]
        public int AirportId { get; set; }

        [Required]
        [StringLength(50)]
        public string AirportName { get; set; }

        [StringLength(6)]
        public string AirportIATACode { get; set; }

        public int LocationId { get; set; }

        public virtual Location Location { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Router> Routers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Router> Routers1 { get; set; }

        //public virtual ICollection<Router> Routers { get; set; }
    }
}
