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
        public int DiscountDetailId { get; set; }

        public int FlightId { get; set; }

        public int TypeDiscountId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? modifyDate { get; set; }
    }
}
