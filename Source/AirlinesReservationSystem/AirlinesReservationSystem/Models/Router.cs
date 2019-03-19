namespace AirlinesReservationSystem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Router
    {

        public Router()
        {
            this.Flights = new HashSet<Flight>();
        }
        [Key]
        [Required]
        public int RouterId { get; set; }
        [Required]
        [Display(Name = "Depart Airport")]
        public int Depart { get; set; }
        [Required]
        [Display(Name = "Arrival Airport")]
        public int Arrival { get; set; }

        public string RouterName { get; set; }

        public int? Distances { get; set; }

        public virtual Airports AirportDepart { get; set; }
        public virtual Airports AirportArrival { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Flight> Flights { get; set; }
    }
}
