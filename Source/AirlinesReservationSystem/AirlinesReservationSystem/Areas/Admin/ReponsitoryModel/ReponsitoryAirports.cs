using AirlinesReservationSystem.IReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlinesReservationSystem.Areas.Admin.ReponsitoryModel
{
    public class ReponsitoryAirports : GenericReponsitory<Airports>
    {
        public List<Airports> Search(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            var airport = this.SelectAll().ToList();
            var search = airport.Where(s => s.AirportName.ToLower().Contains(name.ToLower())).ToList();
            return search;
        }
    }
}