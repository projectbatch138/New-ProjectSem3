using AirlinesReservationSystem.IReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlinesReservationSystem.Areas.Admin.ReponsitoryModel
{
    public class ReponsitoryReservationMod : GenericReponsitory<ReservationMod>
        {
            public List<ReservationMod> Search(string name)
            {
                if (string.IsNullOrEmpty(name)) return null;
                var reservationmod = this.SelectAll().ToList();
                var search = reservationmod.Where(s => s.ReservationModName.ToLower().Contains(name.ToLower())).ToList();
                return search;
            }
        }
}