using AirlinesReservationSystem.IReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlinesReservationSystem.Areas.Admin.ReponsitoryModel
{
    public class ReponsitoryPlane : GenericReponsitory<Plane>
    {
        public List<Plane> Search(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            var planes = this.SelectAll().ToList();
            var search = planes.Where(s => s.PlaneName.ToLower().Contains(name.ToLower())).ToList();
            return search;
        }

        internal void Insert(Router router)
        {
            throw new NotImplementedException();
        }

        internal void Update(Router router)
        {
            throw new NotImplementedException();
        }
    }
}