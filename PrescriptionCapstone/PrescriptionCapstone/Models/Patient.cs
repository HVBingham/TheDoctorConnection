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

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name ="Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Diagnosis")]
        public string Diagnosis { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Appointment")]
        public DateTime? ScheduledAppointment { get; set; }

        [Display(Name = "Log/Notes")]
        public Log Log { get; set; }
        public ICollection<Log> Logs { get; set; }

        [Display(Name = "Medication Options")]
        public ICollection<MedicationViewModel> MedicationOptions { get; set; }

        [ForeignKey("Medication")]
        public int? MedicationId { get; set; }
        public Medication Medication { get; set; }
        public ICollection<Medication> CurrentMedication { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("Doctor")]
        [Display(Name = "Doctor")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public IEnumerable<Doctor> Doctors { get; set; }


    }
}