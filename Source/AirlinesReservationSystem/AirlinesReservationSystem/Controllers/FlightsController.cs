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
        public ActionResult Index([Bind(Include = "FlightID,Location_Depart,Location_Arrival,Time_Depart,Time_Arrival,Trip")] FlightViewModel flightVm)
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
            if (flightVm.Trip == "Round")
            {
                var flightarival = flight
                .Where(x => x.Dept_Time.CompareTo(flightVm.Time_Arrival) > 0)
                .Where(x => x.Router.AirportDepart.Location.LocationId == flightVm.Location_Arrival)
                .Where(x => x.Router.AirportArrival.Location.LocationId == flightVm.Location_Depart)
                .ToList();
                ViewBag.FlightArrival = new List<Flight>();
                ViewBag.FlightArrival = flightarival;
            }  
            return View(flightdepart);

        }
    }
}
