using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PrescriptionCapstone.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Doctor")]
        public int? DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }    

        [Display(Name = "First Name")]
        //[Required]
        //[StringLength(3)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        //[Required]
        //[StringLength(5)]
        public string LastName { get; set; }

        [Display(Name ="Date of Birth")]
        public string DateOfBirth { get; set; }

        [Display(Name = "Email Address")]
        //[Required] Validation but would need to be implemented in the views  @Html.ValidationMessageFor(m => m.FirstName) <--- example.
        //[EmailAddress]
        public string EmailAddress { get; set; }

        [Display(Name = "Diagnosis")]
        public string Diagnosis { get; set; }

        [ForeignKey("Medication")]
        public int? MedicationId { get; set; }
        public Medication Medication { get; set; }
        public ICollection<Medication> Medications { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Appointment")]
        public DateTime? ScheduledAppointment { get; set; }
        public Log Log { get; set; }
        public ICollection<Log> Logs { get; set; }
    }
}