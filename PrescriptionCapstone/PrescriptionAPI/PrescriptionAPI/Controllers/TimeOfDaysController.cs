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
    public class TimeOfDaysController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/TimeOfDays
        public IQueryable<TimeOfDay> GetTimeOfDays()
        {
            return db.TimeOfDays;
        }

        // GET: api/TimeOfDays/5
        [ResponseType(typeof(TimeOfDay))]
        public IHttpActionResult GetTimeOfDay(int id)
        {
            TimeOfDay timeOfDay = db.TimeOfDays.Find(id);
            if (timeOfDay == null)
            {
                return NotFound();
            }

            return Ok(timeOfDay);
        }

        // PUT: api/TimeOfDays/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTimeOfDay(int id, TimeOfDay timeOfDay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != timeOfDay.Id)
            {
                return BadRequest();
            }

            db.Entry(timeOfDay).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimeOfDayExists(id))
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

        // POST: api/TimeOfDays
        [ResponseType(typeof(TimeOfDay))]
        public IHttpActionResult PostTimeOfDay(TimeOfDay timeOfDay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TimeOfDays.Add(timeOfDay);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = timeOfDay.Id }, timeOfDay);
        }

        // DELETE: api/TimeOfDays/5
        [ResponseType(typeof(TimeOfDay))]
        public IHttpActionResult DeleteTimeOfDay(int id)
        {
            TimeOfDay timeOfDay = db.TimeOfDays.Find(id);
            if (timeOfDay == null)
            {
                return NotFound();
            }

            db.TimeOfDays.Remove(timeOfDay);
            db.SaveChanges();

            return Ok(timeOfDay);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TimeOfDayExists(int id)
        {
            return db.TimeOfDays.Count(e => e.Id == id) > 0;
        }
    }
}