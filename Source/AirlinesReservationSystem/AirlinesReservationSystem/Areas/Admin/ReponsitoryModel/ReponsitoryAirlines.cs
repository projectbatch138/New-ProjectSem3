using AirlinesReservationSystem.IReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlinesReservationSystem.ReponsitoryModel
{
    public class ReponsitoryAirlines: GenericReponsitory<Airline>
    {
        public List<Airline> Search(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            var airlines = this.SelectAll().ToList();
            var search = airlines.Where(s => s.AirlineName.ToLower().Contains(name.ToLower())).ToList();
            return search;
        }
    }
}