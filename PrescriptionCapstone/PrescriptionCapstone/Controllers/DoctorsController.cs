using Newtonsoft.Json.Linq;
﻿using Microsoft.AspNet.Identity;
using PrescriptionCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PrescriptionCapstone.Controllers
{
    public class DoctorsController : Controller
    {
        ApplicationDbContext context;
        private string patientDiagnosis;

        public DoctorsController()
        {
            context = new ApplicationDbContext();
        }

        // GET: Doctors
        public ActionResult Index()
        {
            try
            {
                var doctorId = User.Identity.GetUserId();
                Doctor doctor = context.Doctors.Where(d => d.ApplicationId == doctorId).SingleOrDefault();
                var listofPatients = context.Patients.Where(p => p.DoctorId == doctor.Id).ToList();
                return View(listofPatients);
            }
            catch 
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Doctors/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Doctors/Create
        public ActionResult Create()
        {
            Doctor doctor = new Doctor();
            return View(doctor);
        }

        // POST: Doctors/Create
        [HttpPost]
        public ActionResult Create(Doctor doctor)
        {
            try
            {
                doctor.ApplicationId = User.Identity.GetUserId();
                context.Doctors.Add(doctor);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Doctors/Edit/5
        public ActionResult Edit(int id)
        {
            Doctor doctor = context.Doctors.Where(d => d.Id == id).SingleOrDefault();
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Doctor doctor)
        {
            try
            {
                Doctor editDoctor = context.Doctors.Find(id);
                editDoctor.FirstName = doctor.FirstName;
                editDoctor.LastName = doctor.LastName;

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Doctors/Delete/5
        public ActionResult Delete(int id)
        {
            Doctor RemoveDoc = context.Doctors.Where(d => d.Id == id).SingleOrDefault();
            return View(RemoveDoc);
        }

        // POST: Doctors/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Doctor doctor)
        {
            try
            {
                context.Doctors.Remove(context.Doctors.Find(id));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Presciption REST API
        public ActionResult DisplayAllMedication()
        {
            try
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
                return View(ListOfMedication);
            }
            catch
            {
                return View();
            }
        }

        // POST: Prescription REST API by Diagnosis
        [HttpPost]
        public ActionResult DisplayAllMedication(string searchString)
        {
            if (searchString == "")
            {
                return RedirectToAction("DisplayAllMedication");
            }

            try
            {
                patientDiagnosis = searchString;
                var requestUrl = $"https://localhost:44315/api/Medications";
                var result = new WebClient().DownloadString(requestUrl);
                var jo = JArray.Parse(result);
                List<MedicationViewModel> ListOfMedication = new List<MedicationViewModel>();

                for (int i = 0; i < jo.Count; i++)
                {
                    if (jo[i]["Treatment"].ToString() == searchString)
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
                }
                return View(ListOfMedication);
            }
            catch
            {
                return View();
            }
        }

        // GET: Sorted Patients by Diagnosis
        public ActionResult GetPatientsByDiagnosis()
        {
            try
            {
                Doctor doctor = GetDoctor();
                List<Patient> filteredPatients = GetFilteredPatientsByDoctorId(doctor.Id);
                return View("PatientsByDiagnosis", filteredPatients);
            }
            catch
            {
                return RedirectToAction("DisplayAllMedication");
            }
        }

        // GET: doctor's patients by diagnosis
        public List<Patient> GetFilteredPatientsByDoctorId(int id)
        {
            var patientsFromDb = context.Patients.Where(p => p.DoctorId == id);
            var patientsFilteredByDiagnosis = patientsFromDb.Where(p => p.Diagnosis == patientDiagnosis).ToList();
            return patientsFilteredByDiagnosis;
        }

        // GET: Doctor
        public Doctor GetDoctor()
        {
            var userId = User.Identity.GetUserId();
            Doctor doctor = context.Doctors.Where(d => d.ApplicationId == userId).SingleOrDefault();
            return doctor;
        }

        // Register New Patient Login
        public ActionResult CreateLogin()
        {
            return RedirectToAction("Register", "Account", "get");
        }

        [HttpGet]
        public ActionResult DoctorScheduledAppointments(int id)
        {
            var doctor = context.Patients.Where(d => d.Id == id).Select(p => p.ScheduledAppointment);
            return View();
        }
    }
}

