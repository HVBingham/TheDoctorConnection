using System;
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
        public DateTime Time { get; set; }
        public string Text { get; set; }

    
    }
}