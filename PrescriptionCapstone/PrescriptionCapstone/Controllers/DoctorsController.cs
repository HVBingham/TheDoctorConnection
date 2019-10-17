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

        public DoctorsController()
        {
            context = new ApplicationDbContext();
        }

        // GET: Doctors
        public ActionResult Index()
        {
            var doctorId = User.Identity.GetUserId();
            Doctor doctor = context.Doctors.Where(d => d.UserId == doctorId).SingleOrDefault();
            var listofPatients = context.Patients.Where(p => p.DoctorId == doctor.Id).ToList();
            return View(listofPatients);
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
                var user = User.Identity.GetUserId();
                doctor.UserId = user;
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
                editDoctor.EmailAddress = doctor.EmailAddress;

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
    }
}
