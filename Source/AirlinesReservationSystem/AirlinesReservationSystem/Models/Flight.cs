namespace AirlinesReservationSystem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Flight
    {
        public int Flightid { get; set; }

        public int RouterId { get; set; }

        public DateTime Dept_Time { get; set; }

        public DateTime Arr_Time { get; set; }

        public int? Status { get; set; }

        public int PlaneId { get; set; }
    }
}
