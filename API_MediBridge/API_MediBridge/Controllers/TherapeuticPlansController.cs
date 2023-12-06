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
using API_MediBridge.Filters;
using API_MediBridge.Models;
using API_MediBridge.Models.Class;

namespace API_MediBridge.Controllers
{
    [JwtAuthorization]
    public class TherapeuticPlansController : ApiController
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: api/TherapeuticPlans
        public IQueryable<TherapeuticPlans> GetTherapeuticPlans()
        {
            return db.TherapeuticPlans;
        }

        // GET: api/TherapeuticPlans/5
        [HttpGet]
        [Route("api/therapeuticPlans/{id}")]
        public IHttpActionResult GetTherapeuticPlans(string id)
        {
            int ID = Convert.ToInt32(id);
            List<TherapeuticPlans> TPlans =db.TherapeuticPlans.Where(e=>e.MedicalConditions.UserId == ID).ToList();
                     
            if (TPlans == null)
            {
                return NotFound();
            }

            var serializedTPlans = new List<Object>();

            foreach (var t in TPlans)
            {
                var serializedTPlan = new
                {
                    t.PlanId,
                    t.PlanDescription,
                    t.StartDate,
                    t.ExpiryDate,
                    t.MedicalConditionId,
                    MedicalCondition=t.MedicalConditions.Description
                };
                serializedTPlans.Add(serializedTPlan);

            }


            return Ok(serializedTPlans);
        }

        // PUT: api/TherapeuticPlans/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTherapeuticPlans(int id, TherapeuticPlans therapeuticPlans)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != therapeuticPlans.PlanId)
            {
                return BadRequest();
            }

            db.Entry(therapeuticPlans).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TherapeuticPlansExists(id))
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

        // POST: api/TherapeuticPlans
        [HttpPost]
        [Route("api/therapeuticPlan")]
        public IHttpActionResult PostTherapeuticPlans(TplanDTO tPlan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int id = Convert.ToInt32(tPlan.medicalConditionId);

          


            TherapeuticPlans newTherapeuticPlan = new TherapeuticPlans();
            newTherapeuticPlan.MedicalConditionId = id;
            newTherapeuticPlan.StartDate = tPlan.startDate;
            newTherapeuticPlan.ExpiryDate=tPlan.expiryDate;
            newTherapeuticPlan.PlanDescription = tPlan.planDescription;

            try
            {
            db.TherapeuticPlans.Add(newTherapeuticPlan);
            db.SaveChanges();

            }
            catch (DbUpdateException ex) 
            {
                return InternalServerError(ex);
            
            }

            return Ok(Ok("therapeuticPlan saved successfully"));




            
        }

        // DELETE: api/TherapeuticPlans/5
        [ResponseType(typeof(TherapeuticPlans))]
        public IHttpActionResult DeleteTherapeuticPlans(int id)
        {
            TherapeuticPlans therapeuticPlans = db.TherapeuticPlans.Find(id);
            if (therapeuticPlans == null)
            {
                return NotFound();
            }

            db.TherapeuticPlans.Remove(therapeuticPlans);
            db.SaveChanges();

            return Ok(therapeuticPlans);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TherapeuticPlansExists(int id)
        {
            return db.TherapeuticPlans.Count(e => e.PlanId == id) > 0;
        }
    }
}