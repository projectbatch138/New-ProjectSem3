namespace AirlinesReservationSystem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DiscountDetail")]
    public partial class DiscountDetail
    {
        [Key][Required]
        public int DiscountId { get; set; }
        [Required]
        public int FlightId { get; set; }

        public int Discount { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedDate { get; set; }

        public int Status { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ModifyDate { get; set; }
        public virtual Flight Flight { get; set; }
    }
}
