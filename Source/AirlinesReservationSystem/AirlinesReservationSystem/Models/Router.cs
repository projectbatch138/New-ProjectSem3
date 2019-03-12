namespace AirlinesReservationSystem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Router
    {
        public int RouterId { get; set; }

        public int Depart { get; set; }

        public int Arrival { get; set; }

        public int? Distances { get; set; }
    }
}
