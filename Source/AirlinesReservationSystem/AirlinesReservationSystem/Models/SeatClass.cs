namespace AirlinesReservationSystem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SeatClass
    {
        public SeatClass()
        {
            QuantitySeatClass = new HashSet<QuantitySeatClass>();
            SeatNumbers = new HashSet<SeatNumber>();
        }
        [Key]
        public int SeatClassId { get; set; }

        [Required]
        [StringLength(30)]
        public string SeatClassName { get; set; }

        public virtual ICollection<QuantitySeatClass> QuantitySeatClass { get; set; }
        public virtual ICollection<SeatNumber> SeatNumbers { get; set; }
    }
}
