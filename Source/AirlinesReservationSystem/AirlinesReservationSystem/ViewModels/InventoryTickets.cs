using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlinesReservationSystem.ViewModels
{
    public class InventoryTickets
    {
        public int FlightId { get; set; }
        public int SeatClassId { get; set; }
        public int Inventory { get; set; }
    }
}