namespace AirlinesReservationSystem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Flight
    {
<<<<<<< HEAD
        public int Flightid { get; set; }
=======
       public int Flightid { get; set; }
>>>>>>> 6d44df0bf2b51c60db3d30cf0e05bf5a9de978a3

        public int RouterId { get; set; }

        public DateTime Dept_Time { get; set; }

        public DateTime Arr_Time { get; set; }

        public int? Status { get; set; }

        public int PlaneId { get; set; }
    }
}
