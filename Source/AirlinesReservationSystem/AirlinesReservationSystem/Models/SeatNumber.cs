namespace AirlinesReservationSystem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SeatNumber
    {
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
    }
}
