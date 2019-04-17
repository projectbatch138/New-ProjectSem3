using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AirlinesReservationSystem;
using AirlinesReservationSystem.ViewModels;

namespace AirlinesReservationSystem.Controllers
{
    public class FlightsController : Controller
    {
        private Areas.Admin.ReponsitoryModel.ReponsitoryFlights reponsitoryFlights = new Areas.Admin.ReponsitoryModel.ReponsitoryFlights();
        private Areas.Admin.ReponsitoryModel.ReponsitoryLocations reponsitoryLocation = new Areas.Admin.ReponsitoryModel.ReponsitoryLocations();
        private Areas.Admin.ReponsitoryModel.ReponsitorySeatDetailByFlight reponsitorySeatDetailByFlight = new Areas.Admin.ReponsitoryModel.ReponsitorySeatDetailByFlight();
        private Areas.Admin.ReponsitoryModel.ReponsitorySeatClass _Seatclass = new Areas.Admin.ReponsitoryModel.ReponsitorySeatClass();
        private Areas.Admin.ReponsitoryModel.ReponsitoryPrices _price = new Areas.Admin.ReponsitoryModel.ReponsitoryPrices();
        private Areas.Admin.ReponsitoryModel.ReponsitoryQuantitySeatClass _quantitySeat = new Areas.Admin.ReponsitoryModel.ReponsitoryQuantitySeatClass();
        private Areas.Admin.ReponsitoryModel.ReponsitoryBookingTicket _booking = new Areas.Admin.ReponsitoryModel.ReponsitoryBookingTicket();
        private Areas.Admin.ReponsitoryModel.ReponsitoryDiscountDetail _Discount = new Areas.Admin.ReponsitoryModel.ReponsitoryDiscountDetail();
        //Get : Flights
        public ActionResult Index()
        {
            TempData["DataDepart"] = new SelectList(reponsitoryLocation.SelectAll(), "LocationId", "City");
            TempData["DataArrival"] = new SelectList(reponsitoryLocation.SelectAll(), "LocationId", "City");

            return Redirect("Home/index");
            //return PartialView("_SearchFlight");
        }

        // Post: Flights ([Bind(Include = "Flightid,RouterId,Dept_Time,Arr_Time,Status,PlaneId")] Flight flight)
        [HttpPost]
        public ActionResult Index([Bind(Include = "FlightID,Location_Depart,Location_Arrival,Time_Depart,Time_Arrival,Trip,Adults")] FlightViewModel flightVm)
        {
            if (!ModelState.IsValid)
            {
                TempData["DataDepart"] = new SelectList(reponsitoryLocation.SelectAll(), "LocationId", "City");
                TempData["DataArrival"] = new SelectList(reponsitoryLocation.SelectAll(), "LocationId", "City");
                return View("~/Views/Home/Index.cshtml");
            }

            var flight = reponsitoryFlights.SelectAll();
            var flightdepart = flight
                .Where(x => x.Dept_Time.CompareTo(flightVm.Time_Depart) > 0)
                .Where(x => x.Router.AirportDepart.Location.LocationId == flightVm.Location_Depart)
                .Where(x => x.Router.AirportArrival.Location.LocationId == flightVm.Location_Arrival)
                .ToList();
            var seatclasses = _Seatclass.SelectAll();
            ViewBag.SeatClass = new List<SeatClass>();
            ViewBag.SeatClass = seatclasses;

            var seatbyflight = reponsitorySeatDetailByFlight.SelectAll();
            ViewBag.SeatClassByFlight = new List<SeatDetailByFlight>();
            ViewBag.SeatClassByFlight = seatbyflight;
            List<InventoryTickets> inventories = new List<InventoryTickets>();
            foreach (var flightdep in flightdepart)
            {
                foreach (var seatclass in seatclasses)
                {
                    InventoryTickets inventory = new InventoryTickets
                    {
                        FlightId = flightdep.Flightid,
                        SeatClassId = seatclass.SeatClassId
                    };
                    QuantitySeatClass seat = new QuantitySeatClass();
                    seat = _quantitySeat.SelectAll().Where(x => x.PlaneId == flightdep.PlaneId).Where(x => x.SeatClassId == seatclass.SeatClassId).FirstOrDefault();
                    int countBooking = _booking.SelectAll().Where(x => x.FlightId == flightdep.Flightid).Where(x => x.SeatClassId == seatclass.SeatClassId).Where(x => x.ReservationModId == 1 || x.ReservationModId == 2).Count();
                    inventory.Inventory = (seat.Quantity - countBooking);
                    inventories.Add(inventory);
                }
                
            }

            //var seatnumber = _seatnumber.SelectAll();
            ViewBag.Inventories = inventories;


            var Price = _price.SelectAll();
            ViewBag.Price = new List<Price>();
            ViewBag.Price = Price;

            var Discount = _Discount.SelectAll();
            ViewBag.Discount = new List<DiscountDetail>();
            ViewBag.Discount = Discount;
            TempData["Adults"] = flightVm.Adults;
            if (flightVm.Trip == "Round")
            {
                var flightarival = flight
                .Where(x => x.Dept_Time.CompareTo(flightVm.Time_Arrival) > 0)
                .Where(x => x.Router.AirportDepart.Location.LocationId == flightVm.Location_Arrival)
                .Where(x => x.Router.AirportArrival.Location.LocationId == flightVm.Location_Depart)
                .ToList();
                ViewBag.FlightArrival = new List<Flight>();
                ViewBag.FlightArrival = flightarival;
                List<InventoryTickets> inventoriesArr = new List<InventoryTickets>();
                foreach (var flightdep in flightarival)
                {
                    foreach (var seatclass in seatclasses)
                    {
                        InventoryTickets inventoryArr = new InventoryTickets
                        {
                            FlightId = flightdep.Flightid,
                            SeatClassId = seatclass.SeatClassId
                        };
                        QuantitySeatClass seat = new QuantitySeatClass();
                        seat = _quantitySeat.SelectAll().Where(x => x.PlaneId == flightdep.PlaneId).Where(x => x.SeatClassId == seatclass.SeatClassId).FirstOrDefault();
                        int countBooking = _booking.SelectAll().Where(x => x.FlightId == flightdep.Flightid).Where(x => x.SeatClassId == seatclass.SeatClassId).Where(x => x.ReservationModId == 1 || x.ReservationModId == 2).Count();
                        inventoryArr.Inventory = (seat.Quantity - countBooking);
                        inventoriesArr.Add(inventoryArr);
                    }

                }

                //var seatnumber = _seatnumber.SelectAll();
                TempData["InventoriesArr"] = inventoriesArr;
            }
            return View(flightdepart);

        }

        public ActionResult ArrivalFlight()
        {
            var seatclass = _Seatclass.SelectAll();
            ViewBag.SeatClass = new List<SeatClass>();
            ViewBag.SeatClass = seatclass;

            var seatbyflight = reponsitorySeatDetailByFlight.SelectAll();
            ViewBag.SeatClassByFlight = new List<SeatDetailByFlight>();
            ViewBag.SeatClassByFlight = seatbyflight;

            var Price = _price.SelectAll();
            ViewBag.Price = new List<Price>();
            ViewBag.Price = Price;

            var Discount = _Discount.SelectAll();
            ViewBag.Discount = new List<DiscountDetail>();
            ViewBag.Discount = Discount;

            return View();
        }
    }
}
