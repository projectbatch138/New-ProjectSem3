using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AirlinesReservationSystem.Controllers
{
    public class HomeController : Controller
    {
        //private Areas.Admin.ReponsitoryModel.ReponsitoryFlights reponsitoryFlights = new Areas.Admin.ReponsitoryModel.ReponsitoryFlights();
        private Areas.Admin.ReponsitoryModel.ReponsitoryLocations reponsitoryLocation = new Areas.Admin.ReponsitoryModel.ReponsitoryLocations();

       
        public ActionResult Index()
        {
            TempData["DataDepart"] = new SelectList(reponsitoryLocation.SelectAll(), "LocationId", "City");
            TempData["DataArrival"] = new SelectList(reponsitoryLocation.SelectAll(), "LocationId", "City");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}