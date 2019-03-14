using AirlinesReservationSystem.IReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlinesReservationSystem.Areas.Admin.ReponsitoryModel
{
    public class ReponsitoryLocations : GenericReponsitory<Location>
    {
        public List<Location> Search(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            var locations = this.SelectAll().ToList();
            var search = locations.Where(s => s.Country.ToLower().Contains(name.ToLower())).ToList();
            return search;
        }
    }
}