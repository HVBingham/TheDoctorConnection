﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PrescriptionCapstone.Models
{
    
    public class Log
    {
        [Key]
        public int Id { get; set; }
       
        [DataType(DataType.Date)]
        [Display(Name = "Time of log")]
        public DateTime? Time { get; set; }
       
        [Display(Name = "Text")]
        public string Text { get; set; }
        [ForeignKey("Patient")]
        public int PatientsId { get; set; }
        public Patient Patient { get; set; }

    
    }
}