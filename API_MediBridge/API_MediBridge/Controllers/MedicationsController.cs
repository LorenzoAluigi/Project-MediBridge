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
using API_MediBridge.Models;
using API_MediBridge.Models.Class;

namespace API_MediBridge.Controllers
{
    public class MedicationsController : ApiController
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: api/Medications
        public IQueryable<Medications> GetMedications()
        {
            return db.Medications;
        }

        // GET: api/Medications/5
        [HttpGet]
        [Route("api/Medications/{id}")]
        public IHttpActionResult GetMedications(string id)
        {
            int ID = Convert.ToInt32(id);

            List<Medications> medications = db.Medications.Where(e=>e.MedicalConditions.UserId == ID).ToList();
            if (medications == null)
            {
                return NotFound();
            }
            List<MedicationDTO> medicationsDTO = new List<MedicationDTO>();

            
            foreach (var item in medications)
            {
                var medicationDTO = new MedicationDTO();
                medicationDTO.MedicationId = item.MedicationId.ToString();
                medicationDTO.MedicationName = item.MedicationName;
                medicationDTO.MedicalConditionId = item.MedicalConditionId.ToString();
                medicationDTO.DailyDosage = item.DailyDosage;
                medicationDTO.PlanId=item.PlanId.ToString();
                medicationDTO.MedicalCondition = item.MedicalConditions.Description;
                if (item.TherapeuticPlans != null)
                {

                medicationDTO.TPlanDescription = item.TherapeuticPlans.PlanDescription;
                }

                medicationsDTO.Add(medicationDTO);
            }


            return Ok(medicationsDTO);
        }

        // PUT: api/Medications/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMedications(int id, Medications medications)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != medications.MedicationId)
            {
                return BadRequest();
            }

            db.Entry(medications).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicationsExists(id))
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
        [HttpPost]
        [Route("api/Medications")]
        public IHttpActionResult PostMedications(MedicationDTO medications)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Medications newMedication = new Medications();
            newMedication.MedicationName = medications.MedicationName;
            newMedication.DailyDosage = medications.DailyDosage;
            newMedication.MedicalConditionId = Convert.ToInt32(medications.MedicalConditionId);
            if (medications.PlanId != null)
            {
                newMedication.PlanId = Convert.ToInt32(medications.PlanId);
            }


            try
            {
            db.Medications.Add(newMedication);
            db.SaveChanges();

            }
            catch { return BadRequest(); }


            return Ok();
        }

        // DELETE: api/Medications/5
        [ResponseType(typeof(Medications))]
        public IHttpActionResult DeleteMedications(int id)
        {
            Medications medications = db.Medications.Find(id);
            if (medications == null)
            {
                return NotFound();
            }

            db.Medications.Remove(medications);
            db.SaveChanges();

            return Ok(medications);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MedicationsExists(int id)
        {
            return db.Medications.Count(e => e.MedicationId == id) > 0;
        }
    }
}