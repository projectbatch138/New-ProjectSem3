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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuantitySeatClassId { get; set; }

        public int PlaneId { get; set; }

        public int Quantity { get; set; }

        public int SeatClassId { get; set; }
    }
}
