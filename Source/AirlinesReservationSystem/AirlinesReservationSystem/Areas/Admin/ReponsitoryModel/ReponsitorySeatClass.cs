using AirlinesReservationSystem.IReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlinesReservationSystem.Areas.Admin.ReponsitoryModel
{
    public class ReponsitorySeatClass : GenericReponsitory<SeatClass>
    {
        public List<SeatClass> Search(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            var seatClasses = this.SelectAll().ToList();
            var search = seatClasses.Where(s => s.SeatClassName.ToLower().Contains(name.ToLower())).ToList();
            return search;
        }
    }
}