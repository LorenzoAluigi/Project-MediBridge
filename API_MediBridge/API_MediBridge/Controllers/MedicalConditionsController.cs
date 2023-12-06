using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using API_MediBridge.Models;
using API_MediBridge.Models.Class;

namespace API_MediBridge.Controllers
{
    public class MedicalConditionsController : ApiController
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: api/MedicalConditions
        public IQueryable<MedicalConditions> GetMedicalConditions()
        {
            return db.MedicalConditions;
        }

        // GET: api/MedicalConditions/5
        [HttpGet]
        [Route("api/MedicalConditions/{id}")]
        public async Task<IHttpActionResult> GetMedicalConditions(string id)
        {
            int ID = Convert.ToInt32(id);

            List<MedicalConditions> MedicalConditions = db.MedicalConditions.Where(e => e.UserId == ID).ToList();
                
                
            if (MedicalConditions == null)
            {
                return NotFound();
            }

            var serializedMedicalConditions = new List<object>();

            foreach (var item in MedicalConditions)
            {
                var serializeMedicalCondition = new
                {
                    MedicalConditionId = item.MedicalConditionId,
                    Description = item.Description,
                    UserId = item.UserId,
                    DiagnosisYear = item.DiagnosisYear

                };
                serializedMedicalConditions.Add(serializeMedicalCondition);
            }
            return Ok(serializedMedicalConditions);
        }

        // PUT: api/MedicalConditions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMedicalConditions(int id, MedicalConditions medicalConditions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != medicalConditions.MedicalConditionId)
            {
                return BadRequest();
            }

            db.Entry(medicalConditions).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicalConditionsExists(id))
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

        // POST: api/MedicalConditions
        [HttpPost]
        [Route("api/MedicalCondition")]
        public async Task<IHttpActionResult> PostMedicalConditions(MedicalConditionDto medicalConditions)
        {
            
            MedicalConditions newMedCond= new MedicalConditions();
            newMedCond.Description= medicalConditions.Description;
            newMedCond.DiagnosisYear= medicalConditions.DiagnosisYear;
            newMedCond.UserId= medicalConditions.UserId;

            
            try
            {

            db.MedicalConditions.Add(newMedCond);
            await db.SaveChangesAsync();
            }
            catch(Exception ex) 
            {
                return BadRequest("error to add db" + ex);
            
            }

            return Ok("medical condition saved successfully.");
        }

        // DELETE: api/MedicalConditions/5
        [ResponseType(typeof(MedicalConditions))]
        public async Task<IHttpActionResult> DeleteMedicalConditions(int id)
        {
            MedicalConditions medicalConditions = await db.MedicalConditions.FindAsync(id);
            if (medicalConditions == null)
            {
                return NotFound();
            }

            db.MedicalConditions.Remove(medicalConditions);
            await db.SaveChangesAsync();

            return Ok(medicalConditions);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MedicalConditionsExists(int id)
        {
            return db.MedicalConditions.Count(e => e.MedicalConditionId == id) > 0;
        }
    }
}