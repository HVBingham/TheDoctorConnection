using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using PrescriptionCapstone.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace PrescriptionCapstone.Controllers
{
    public class PatientsController : Controller
    {
        public ApplicationDbContext context;

        //public object MailHelper { get; private set; }

        public PatientsController()
        {
            context = new ApplicationDbContext();
        }

        // GET: Patients
        public ActionResult Index()
        {
            var patients = context.Patients.ToList();
            return View(patients);
        }

        // GET: Patients/Details/5
        public ActionResult Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            var applicationId = User.Identity.GetUserId();
            Patient patient = context.Patients.FirstOrDefault(p => p.ApplicationId == applicationId);

            //Patient patient = context.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // GET: Patients/Create
        public ActionResult Create()
        {

            var doctors = context.Doctors.ToList();
            Patient patient = new Patient()
            {
                Doctors = doctors
            };

            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
                patient.ApplicationId = User.Identity.GetUserId();
                context.Patients.Add(patient);
                context.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(patient);
        }

        // GET: Patients/Edit/5
        [Authorize(Roles = RoleName.Doctor)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var medicationOptions = GetMedicationFromAPI();
            Patient patient = context.Patients.Find(id);
            patient.MedicationOptions = medicationOptions;
            if (patient == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationId = new SelectList(context.Patients, "Id", "Email", patient.Id);
            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Doctor)]
        public ActionResult Edit(int id, Patient patient)
        {
            Patient patient1 = context.Patients.Find(id);
            patient1.FirstName = patient.FirstName;
            patient1.LastName = patient.LastName;
            patient.Doctor = patient.Doctor;
            return View(patient);
        }

        // GET: Patients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = context.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = context.Patients.Find(id);
            context.Patients.Remove(patient);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }

        [Authorize(Roles = RoleName.Doctor)]
        public List<MedicationViewModel> GetMedicationFromAPI()
        {
            var requestUrl = $"https://localhost:44315/api/Medications";
            var result = new WebClient().DownloadString(requestUrl);
            var jo = JArray.Parse(result);
            List<MedicationViewModel> ListOfMedication = new List<MedicationViewModel>();


            for (int i = 0; i < jo.Count; i++)
            {
                MedicationViewModel medication = new MedicationViewModel();
                medication.Id = Convert.ToInt32(jo[i]["Id"]);
                medication.Name = jo[i]["Name"].ToString();
                medication.Description = jo[i]["Description"].ToString();
                medication.SideEffect = jo[i]["SideEffect"].ToString();
                medication.TimeOfDay = jo[i]["TimeOfDay"].ToString();
                medication.Treatment = jo[i]["Treatment"].ToString();
                ListOfMedication.Add(medication);
            }
            return ListOfMedication;
        }

        public ActionResult SelectAppointment(Doctor value, Doctor appointment)
        {
            Doctor appointmentsFromDb = context.Doctors.Where(d => d.Appointment == appointment.Appointment).FirstOrDefault();
            
            if (appointmentsFromDb != null)
            {
                return View("Appointment not available" + RedirectToAction("SelectAppointment"));
            }
            else
            {
                context.Doctors.Add(appointment);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }


        public ActionResult AddSelectedMedication(int Id, Patient medicaiton)
        {
            Patient patientFromDb = context.Patients.Find(Id);
            Patient medicationFromDb = context.Patients.Where(p => p.Medication == medicaiton.Medication).FirstOrDefault();
            context.Patients.Add(medicationFromDb);
            context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        public ActionResult confrimMedTaken(Medication medication)
        {
            Medication medicatioFromDb = context.Medications.Where(m => m.PatientId == medication.PatientId).FirstOrDefault();
            medication.MedicationConfirmed = true;
            context.SaveChanges();
            return View(medication);
            //what if patient does not confirm?
        }
        public ActionResult patientLog(int Id, Log text)
        {
            Patient patientFromDb = context.Patients.Find(Id);
            DateTime dt = DateTime.Now;
            Log log = new Log();
            context.Log.Add(log);
            context.SaveChanges();
            return View(patientFromDb.Log);
        }
        public ActionResult Dashboard()
        {
            return View();
        }


        //private static void Main()
        //{
        //    SendEmail().Wait();
        //}
        public async Task<ActionResult> PrescriptionEmail(int id)
        {
            Patient patient = context.Patients.Include(p => p.User).FirstOrDefault(p => p.Id == id);



            return View(patient);

        }
        [HttpPost]
        public async Task<ActionResult> PrescriptionEmail(Patient patient)
        {

            string email = patient.User.Email;
            //var apiKey = Environment.GetEnvironmentVariable("Work");
            var client = new SendGridClient(ApiKey.sendgridapi);
            var from = new EmailAddress("michaelrackowski@gmail.com", "Michael Rack");
            var subject = "Prescription Status!";
            var to = new EmailAddress(email, "Myself");
            var plainTextContent = "Your Prescription is ready for pick up";
            var htmlContent = "<strong>Your Prescription is ready for pick up</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            var startdate = DateTime.Today;

            return RedirectToAction("Index");
        }
        public async Task<ActionResult> RefillEmail(int id)
        {
            Patient patient = context.Patients.Include(p => p.User).FirstOrDefault(p => p.Id == id);


           // return View("Index");
            return View(patient);
        }
        [HttpPost]
        public async Task<ActionResult> RefillEmail(Patient patient)
            {
            string email = patient.User.Email;
            var client = new SendGridClient(ApiKey.sendgridapi);
            var from = new EmailAddress("michaelrackowski@gmail.com", "Michael Rack");
            var subject = "Prescription Status!";
            var to = new EmailAddress(email, "Myself");
            var plainTextContent = "Your prescription is almost up please notify your doctor for a refill";
            var htmlContent = "<strong>Your prescription is almost up please notify your doctor for a refill</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            var startdate = DateTime.Today;

            return RedirectToAction("Index");
        }
    }
}
   



