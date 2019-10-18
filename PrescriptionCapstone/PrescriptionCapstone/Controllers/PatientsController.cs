using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PrescriptionCapstone.Models;

namespace PrescriptionCapstone.Controllers
{
    public class PatientsController : Controller
    {
        ApplicationDbContext context;

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

        // GET: Patients/Create
        public ActionResult Create()
        {
            Patient patient=new Patient();
            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.GetUserId();
                patient.UserId = user;
                context.Patients.Add(patient);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

         
            return View(patient);
        }

        // GET: Patients/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.ApplicationId = new SelectList(context.Patients, "Id", "Email", patient.Id);
            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

        /*public ActionResult SelectMedication()
        {
            //view list from MD 
            //select medication
            //store selected option in a variable 
            //notify MD??
        }*/
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

    }
}
