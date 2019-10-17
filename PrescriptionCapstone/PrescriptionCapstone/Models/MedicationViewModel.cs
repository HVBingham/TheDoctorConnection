using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrescriptionCapstone.Models
{
    public class MedicationViewModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Generic Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Side Effect")]
        public string SideEffect { get; set; }

        [Display(Name = "Time of Day")]
        public string TimeOfDay { get; set; }

        [Display(Name = "Treatment")]
        public string Treatment { get; set; }
    }
}