using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Description;
using API_MediBridge.Models;
using API_MediBridge.Models.Class;

namespace API_MediBridge.Controllers
{
    public class DoctorsController : ApiController
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: api/Doctors
        public IQueryable<Doctors> GetDoctors()
        {
            return db.Doctors;
        }

        // GET: api/Doctors/idUser
        [HttpGet]
        [Route("api/Doctor/{id}")]
        public IHttpActionResult GetDoctors(string id)
        {
            int Id = Convert.ToInt32(id);
            Doctors doctors = db.Doctors.FirstOrDefault(e => e.UsersID == Id);
            if (doctors == null)
            {
                return NotFound();
            }

            DoctorDTO doctorDTO = new DoctorDTO();
            doctorDTO.DoctorId = doctors.DoctorsID;
            doctorDTO.UsersId = doctors.UsersID;
            doctorDTO.DoctorName= doctors.User.FirstName;
            doctorDTO.DoctorSurname= doctors.User.LastName;
            doctorDTO.DoctorSpecialization = doctors.Specialization;



            return Ok(doctorDTO);
        }


        // GET api/doctors/search?query=loremIpsum
        [HttpGet]
        [Route("api/doctors/search")]
        public IHttpActionResult SearchDoctors(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return BadRequest("La query non può essere vuota.");
            }

            // Effettua la ricerca nel database
            var searchResults = db.Doctors
                .Where(d => d.User.LastName.Contains(query) || d.Specialization.Contains(query))
                .ToList();

            List<DoctorDTO> doctors = new List<DoctorDTO>();

            foreach (var i in searchResults)
            {
                DoctorDTO doctor = new DoctorDTO();
                doctor.DoctorId = i.DoctorsID;
                doctor.DoctorName = i.User.FirstName;
                doctor.DoctorSurname = i.User.LastName;
                doctor.DoctorSpecialization = i.Specialization;
                doctor.UsersId = i.UsersID;

                doctors.Add(doctor);

            }




            return Ok(doctors);
        }
    



    // PUT: api/Doctors/5
    [ResponseType(typeof(void))]
        public IHttpActionResult PutDoctors(int id, Doctors doctors)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != doctors.DoctorsID)
            {
                return BadRequest();
            }

            db.Entry(doctors).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorsExists(id))
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

        // POST: api/Doctors
        [ResponseType(typeof(Doctors))]
        public IHttpActionResult PostDoctors(Doctors doctors)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Doctors.Add(doctors);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = doctors.DoctorsID }, doctors);
        }

        // DELETE: api/Doctors/5
        [ResponseType(typeof(Doctors))]
        public IHttpActionResult DeleteDoctors(int id)
        {
            Doctors doctors = db.Doctors.Find(id);
            if (doctors == null)
            {
                return NotFound();
            }

            db.Doctors.Remove(doctors);
            db.SaveChanges();

            return Ok(doctors);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DoctorsExists(int id)
        {
            return db.Doctors.Count(e => e.DoctorsID == id) > 0;
        }
    }
}