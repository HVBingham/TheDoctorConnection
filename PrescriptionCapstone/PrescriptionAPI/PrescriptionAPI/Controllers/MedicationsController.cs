using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PrescriptionAPI.Models;

namespace PrescriptionAPI.Controllers
{
    public class MedicationsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Medications
        public IQueryable<Medication> GetMedications()
        {
            return db.Medications;
        }

        // GET: api/Medications/5
        [ResponseType(typeof(Medication))]
        public IHttpActionResult GetMedication(int id)
        {
            Medication medication = db.Medications.Find(id);
            if (medication == null)
            {
                return NotFound();
            }

            return Ok(medication);
        }

        // PUT: api/Medications/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMedication(int id, Medication medication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != medication.Id)
            {
                return BadRequest();
            }

            db.Entry(medication).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Medications
        [ResponseType(typeof(Medication))]
        public IHttpActionResult PostMedication(Medication medication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Medications.Add(medication);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = medication.Id }, medication);
        }

        // DELETE: api/Medications/5
        [ResponseType(typeof(Medication))]
        public IHttpActionResult DeleteMedication(int id)
        {
            Medication medication = db.Medications.Find(id);
            if (medication == null)
            {
                return NotFound();
            }

            db.Medications.Remove(medication);
            db.SaveChanges();

            return Ok(medication);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MedicationExists(int id)
        {
            return db.Medications.Count(e => e.Id == id) > 0;
        }
    }
}