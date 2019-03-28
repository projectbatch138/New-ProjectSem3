namespace AirlinesReservationSystem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SeatNumber
    {
        public SeatNumber()
        {
            /*
            this.Booking_Ticket = new HashSet<Booking_Ticket>();
            this.Booking_Ticket1 = new HashSet<Booking_Ticket>();
            this.DiscountDetails = new HashSet<DiscountDetail>();
            this.Prices = new HashSet<Price>();
            */
            this.SeatDetailByFlights = new HashSet<SeatDetailByFlight>();
        }
        [Key]
        [Required]
        public int SeatNumberId { get; set; }

        [Required]
        public int SeatNo { get; set; }
        [Required]
        public int SeatClassId { get; set; }
        [Required]
        public int PlaneId { get; set; }

        public virtual SeatClass SeatClass { get; set; }

        public virtual Plane Plane { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SeatDetailByFlight> SeatDetailByFlights { get; set; }
    }
}
