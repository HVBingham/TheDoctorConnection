using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrescriptionAPI.Models
{
    public class SideEffect
    {
        //public SideEffect()
        //{
        //    this.Medications = new HashSet<Medication>();
        //}
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
       // public ICollection<Medication> Medications { get; set; }
    }

}