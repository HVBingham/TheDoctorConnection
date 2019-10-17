using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrescriptionAPI.Models
{
    public class Treatment
    {
        //public Treatment()
        //{
        //    this.Medications = new HashSet<Medication>();
        //} 
        [Key]
        public int Id { get; set; }
        public string TreatmentType { get; set; }
       // public ICollection<Medication> Medications {get;set;} 
    }

}