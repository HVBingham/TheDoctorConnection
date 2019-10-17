using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PrescriptionAPI.Models
{
    public class Medication
    {
        //public Medication()
        //{
        //    this.SideEffects = new HashSet<SideEffect>();
        //    this.TimeOfDays = new HashSet<TimeOfDay>();
        //    this.Treatments = new HashSet<Treatment>();


        //}
       
        [Key]
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        //[ForeignKey("SideEffect")]
        //[Display(Name = "Side Effects")]
        //public int? SideEffectID { get; set; }
        //public SideEffect SideEffect { get; set; }
        ////public ICollection<SideEffect> SideEffects { get; set; }

        //[ForeignKey("TimeOfDay")]
        //[Display(Name = "Time of Day")]
        //public int? TimeOfDayId { get; set; }
        //public TimeOfDay TimeOfDay { get; set; }
        ////public ICollection<TimeOfDay> TimeOfDays {get;set;}

        //[ForeignKey("Treatment")]
        //[Display(Name="Treatments")]
        //public int?  TreatmentId { get; set; }
        //public Treatment Treatment { get; set; }
        ////public ICollection<Treatment> Treatments { get; set; }

        //[ForeignKey("SideEffect")]
        [Display(Name = "Side Effects")]
        public string SideEffect { get; set; }
        //public SideEffect SideEffect { get; set; }
        //public IEnumerable <SideEffect> SideEffects { get; set; }

        //[ForeignKey("TimeOfDay")]
        [Display(Name="Time of Day")]
        public string TimeOfDay { get; set; }
        //public string? TimeOfDay { get; set; }
        //public TimeOfDay TimeOfDay { get; set; }

        //[ForeignKey("Treatment")]
        [Display(Name="Treatments")]
        public string Treatment { get; set; }
        //public Treatment Treatment { get; set; }

        
    }

}