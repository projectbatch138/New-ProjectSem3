namespace AirlinesReservationSystem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ReservationMod")]
    public partial class ReservationMod
    {
        public int ReservationModId { get; set; }

        [Required]
        [StringLength(20)]
        public string ReservationModName { get; set; }
    }
}
