﻿using System;
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
            var patients = context.Patients.Include(p => p.Id);
            return View(patients.ToList());
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

        // POST: Patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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
        /*public ActionResult SelectAppointment()
        {
            Doctor appointment = new 
        }   
*/
        /*public ActionResult SelectMedication()
        {
            //view list from MD 
            //select medication
            //store selected option in a variable 
            //notify MD??
        }*/


        //public ActionResult confrimMedTaken(int Id, Patient patient)
        //{
 
        //    patient = context.Patients.Find(Id);

        //    if (patient.Id)
        //    {

        //    }
        //    return View(medications);
        //}
        //public ActionResult patientLog(int Id, string text)
        //{
        //    Patient patientFromDb = context.Patients.Find(Id);
        //    DateTime dt = DateTime.Now;
        //    patientFromDb.Log.Add(dt, text);

        //    return View(patientFromDb.Log);
        //}

    }
}
