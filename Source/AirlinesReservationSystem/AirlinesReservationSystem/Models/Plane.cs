namespace AirlinesReservationSystem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Plane
    {
        public int PlaneId { get; set; }

        [Required]
        [StringLength(50)]
        public string PlaneName { get; set; }

        public int AirlineId { get; set; }

        public int Capacity { get; set; }

        [StringLength(3)]
        public string CodeIATAPlane { get; set; }

        public int? Status { get; set; }
    }
}
