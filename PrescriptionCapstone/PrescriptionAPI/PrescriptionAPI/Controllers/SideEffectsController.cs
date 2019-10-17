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
    public class SideEffectsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/SideEffects
        public IQueryable<SideEffect> GetSideEffects()
        {
            return db.SideEffects;
        }

        // GET: api/SideEffects/5
        [ResponseType(typeof(SideEffect))]
        public IHttpActionResult GetSideEffect(int id)
        {
            SideEffect sideEffect = db.SideEffects.Find(id);
            if (sideEffect == null)
            {
                return NotFound();
            }

            return Ok(sideEffect);
        }

        // PUT: api/SideEffects/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSideEffect(int id, SideEffect sideEffect)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sideEffect.Id)
            {
                return BadRequest();
            }

            db.Entry(sideEffect).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SideEffectExists(id))
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

        // POST: api/SideEffects
        [ResponseType(typeof(SideEffect))]
        public IHttpActionResult PostSideEffect(SideEffect sideEffect)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SideEffects.Add(sideEffect);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = sideEffect.Id }, sideEffect);
        }

        // DELETE: api/SideEffects/5
        [ResponseType(typeof(SideEffect))]
        public IHttpActionResult DeleteSideEffect(int id)
        {
            SideEffect sideEffect = db.SideEffects.Find(id);
            if (sideEffect == null)
            {
                return NotFound();
            }

            db.SideEffects.Remove(sideEffect);
            db.SaveChanges();

            return Ok(sideEffect);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SideEffectExists(int id)
        {
            return db.SideEffects.Count(e => e.Id == id) > 0;
        }
    }
}