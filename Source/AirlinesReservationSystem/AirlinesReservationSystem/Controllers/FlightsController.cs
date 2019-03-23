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
        
        //Get : Flights
        public ActionResult Index()
        {
            TempData["DataDepart"] = new SelectList(reponsitoryLocation.SelectAll(), "LocationId", "City");
            TempData["DataArrival"] = new SelectList(reponsitoryLocation.SelectAll(), "LocationId", "City");
            return Redirect("Home/index");
            //return PartialView("_SearchFlight");
        }

        // Post: Flights ([Bind(Include = "Flightid,RouterId,Dept_Time,Arr_Time,Status,PlaneId")] Flight flight)
        public ActionResult SearchFlight([Bind(Include = "FlightID,Location_Depart,Location_Arrival,Time_Depart,Time_Arrival,Trip")] FlightViewModel flightVm)
        {
            if (ModelState.IsValid)
            {
                return Redirect("Home/index");
            }
            var flight = reponsitoryFlights.SelectAll();
            var flightdepart = flight
                .Where(x => x.Dept_Time.CompareTo(flightVm.Time_Depart) > 0)
                .Where(x => x.Router.AirportDepart.Location.LocationId == flightVm.Location_Depart)
                .Where(x => x.Router.AirportArrival.Location.LocationId == flightVm.Location_Arrival)
                .ToList();
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
