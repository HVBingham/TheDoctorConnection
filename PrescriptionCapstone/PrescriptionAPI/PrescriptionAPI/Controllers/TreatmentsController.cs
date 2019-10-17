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
    public class TreatmentsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Treatments
        public IQueryable<Treatment> GetTreatments()
        {
            return db.Treatments;
        }

        // GET: api/Treatments/5
        [ResponseType(typeof(Treatment))]
        public IHttpActionResult GetTreatment(int id)
        {
            Treatment treatment = db.Treatments.Find(id);
            if (treatment == null)
            {
                return NotFound();
            }

            return Ok(treatment);
        }

        // PUT: api/Treatments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTreatment(int id, Treatment treatment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != treatment.Id)
            {
                return BadRequest();
            }

            db.Entry(treatment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TreatmentExists(id))
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

        // POST: api/Treatments
        [ResponseType(typeof(Treatment))]
        public IHttpActionResult PostTreatment(Treatment treatment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Treatments.Add(treatment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = treatment.Id }, treatment);
        }

        // DELETE: api/Treatments/5
        [ResponseType(typeof(Treatment))]
        public IHttpActionResult DeleteTreatment(int id)
        {
            Treatment treatment = db.Treatments.Find(id);
            if (treatment == null)
            {
                return NotFound();
            }

            db.Treatments.Remove(treatment);
            db.SaveChanges();

            return Ok(treatment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TreatmentExists(int id)
        {
            return db.Treatments.Count(e => e.Id == id) > 0;
        }
    }
}