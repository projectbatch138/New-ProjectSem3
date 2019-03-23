using AirlinesReservationSystem.IReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlinesReservationSystem.Areas.Admin.ReponsitoryModel
{
    public class ReponsitoryFlights : GenericReponsitory<Flight>
    {
        public List<Flight> Search(int departLocationId, int arrivalLocationId,DateTime departtime, DateTime arrivaltime)
        {
            var flight = this.SelectAll().ToList();
            if (arrivaltime == null)
            {
                departtime = DateTime.Now;
                flight = flight.Where(x => x.Dept_Time.CompareTo(departtime) >0 ).ToList();
                return flight;
            }
            return flight;
        }
    }
}