using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;
using AirlinesReservationSystem.ValidationAtributesCustom;

namespace AirlinesReservationSystem.ViewModels
{
    public class FlightViewModel
    {
        [Key]
        public int FlightID { get; set; }
       // public int LocationId { get; set; }
        [Required]
        public int Location_Depart { get; set; }
        [Required]
       // [Compare("Location_Depart" , ErrorMessage = "The Depart and Arrival do not match.")]
        public int Location_Arrival { get; set; }
        [Required]
        public DateTime Time_Depart { get; set; }

        [RequiredIf("Trip", "Round", "Requied If you choosed Round")]
        public DateTime? Time_Arrival { get; set; }
        [Required]
        public string Trip { get; set; }
        [Range(1,3)][Required]
        public int Adults { get; set; }
    }
}