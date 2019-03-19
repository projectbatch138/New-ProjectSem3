namespace AirlinesReservationSystem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Plane
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Plane()
        {
            this.Flights = new HashSet<Flight>();
            this.QuantitySeatClass = new HashSet<QuantitySeatClass>();
            this.SeatNumbers = new HashSet<SeatNumber>();
        }
        [Key]
        [Required]
        public int PlaneId { get; set; }

        [Required]
        [StringLength(50)]
        public string PlaneName { get; set; }

        public int AirlineId { get; set; }

        public int Capacity { get; set; }

        [StringLength(3)]
        public string CodeIATAPlane { get; set; }

        public int? Status { get; set; }
        public virtual Airline Airline { get; set; }
        public virtual ICollection<QuantitySeatClass> QuantitySeatClass { get; set; }
        public virtual ICollection<SeatNumber> SeatNumbers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Flight> Flights { get; set; }

    }
}
