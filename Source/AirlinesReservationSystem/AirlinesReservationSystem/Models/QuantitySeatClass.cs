namespace AirlinesReservationSystem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QuantitySeatClass")]
    public partial class QuantitySeatClass
    {
        [Key]
        [Required]
        public int QuantitySeatClassId { get; set; }
        [Required]
        public int PlaneId { get; set; }

        public int Quantity { get; set; }
        [Required]
        public int SeatClassId { get; set; }

        public virtual SeatClass SeatClass { get; set; }

        public virtual Plane Plane { get; set; }
    }
}
