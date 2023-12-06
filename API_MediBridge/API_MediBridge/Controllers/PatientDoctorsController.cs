using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Deployment.Internal;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using API_MediBridge.Models;
using API_MediBridge.Models.Class;

namespace API_MediBridge.Controllers
{
    public class PatientDoctorsController : ApiController
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: api/PatientDoctors
        public IQueryable<PatientDoctor> GetPatientDoctors()
        {
            return db.PatientDoctors;
        }





        
        [HttpGet]
        [Route("api/getPatientDoctorByUserId/{id}")]
        public IHttpActionResult GetbyUserId(string id)
        {
            int Id =Convert.ToInt32(id);
            List<PatientDoctor> patientDoctors = db.PatientDoctors
            .Include(pd => pd.Doctors)
            .Include(pd => pd.User)
            .Where(e => e.UsersId == Id)
            .ToList();

            if (patientDoctors == null)
            {
                return NotFound();
            }
            List<PatientDoctorDTO> response = new List<PatientDoctorDTO>();
            


            foreach (var i in patientDoctors)
            {
                PatientDoctorDTO newPatient = new PatientDoctorDTO();
                newPatient.PatientDoctorID = i.PatientDoctorId;
                newPatient.UserId = i.UsersId;
                newPatient.Doctor = new DoctorDTO();
                newPatient.Doctor.DoctorId = i.DoctorsId;
                newPatient.Doctor.DoctorName = i.Doctors.User.FirstName;
                newPatient.Doctor.DoctorSurname=i.Doctors.User.LastName;
                newPatient.Doctor.DoctorSpecialization = i.Doctors.Specialization;
                newPatient.Doctor.UsersId = i.Doctors.UsersID;
                
                response.Add(newPatient);
            }

            return Ok(response);
        }


        [HttpGet]
        [Route("api/getPatientDoctorByDoctorId/{id}")]
        public IHttpActionResult GetbyDoctorId(string id)
        {
            int Id = Convert.ToInt32(id);

           List< PatientDoctor> patientsDoctor = db.PatientDoctors.Where(e=> e.DoctorsId ==Id).ToList();
            if (patientsDoctor == null)
            {
                return NotFound();
            }

            List<PatientByDoctorDTO> patients = new List<PatientByDoctorDTO>();

            foreach (var i in patientsDoctor)
            {
                PatientByDoctorDTO newPatient=new PatientByDoctorDTO();
                newPatient.PatientDoctorID = i.PatientDoctorId;
                newPatient.DoctorId = i.DoctorsId;
               
                PatientDTO Patient = new PatientDTO();
                Patient.UserId = i.UsersId;
                Patient.FirstName = i.User.FirstName;
                Patient.LastName = i.User.LastName;
                Patient.Email = i.User.Email;
                Patient.Address = i.User.Address;
                Patient.City = i.User.City;
                Patient.CF=i.User.CF;
                Patient.Country = i.User.Country;
                Patient.Gender = i.User.Gender;
                Patient.DateOfBirth = i.User.DateOfBirth;
                Patient.Province = i.User.Province;

                newPatient.Patient= Patient;

                patients.Add(newPatient);
            }

            return Ok(patients);
        }




        // GET: api/getPatient/5
        [HttpGet]
        [Route("api/getPatient/{id}")]
        public IHttpActionResult GetPatientDoctor(string id)
        {
            int Id=Convert.ToInt32(id);
            Users patient = db.Users.Find(Id);
           
            if (patient == null)
            {
                return NotFound();
            }

            PatientDTO response= new PatientDTO();

            response.FirstName = patient.FirstName;
            response.LastName = patient.LastName;
            response.Email = patient.Email;
            response.Address = patient.Address;
            response.City = patient.City;
            response.Country = patient.Country;
            response.Gender = patient.Gender;
            response.CF = patient.CF;
            response.DateOfBirth= patient.DateOfBirth;
            response.UserId = patient.UserId;
            response.Province = patient.Province;


            return Ok(response);
        }

        // PUT: api/PatientDoctors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPatientDoctor(int id, PatientDoctor patientDoctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != patientDoctor.PatientDoctorId)
            {
                return BadRequest();
            }

            db.Entry(patientDoctor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientDoctorExists(id))
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

        // POST: api/PatientDoctors
       [HttpPost]
        [Route("api/PatientDoctors")]
        public IHttpActionResult PostPatientDoctor(PatientDoctor patientDoctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PatientDoctors.Add(patientDoctor);
            db.SaveChanges();

            return Ok("saved successfully");
        }

        // DELETE: api/PatientDoctors/5
        [ResponseType(typeof(PatientDoctor))]
        public IHttpActionResult DeletePatientDoctor(int id)
        {
            PatientDoctor patientDoctor = db.PatientDoctors.Find(id);
            if (patientDoctor == null)
            {
                return NotFound();
            }

            db.PatientDoctors.Remove(patientDoctor);
            db.SaveChanges();

            return Ok(patientDoctor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PatientDoctorExists(int id)
        {
            return db.PatientDoctors.Count(e => e.PatientDoctorId == id) > 0;
        }
    }
}