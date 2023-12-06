using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.UI.WebControls;
using API_MediBridge.Filters;
using API_MediBridge.Models;
using API_MediBridge.Models.Class;
namespace API_MediBridge.Controllers
{
    [JwtAuthorization]
    public class UsersController : ApiController
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: api/Users
        public IQueryable<Users> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Users/5
        [ResponseType(typeof(UserLoggedDTO))]
        public IHttpActionResult GetUsers(int id)
        {

            Users user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
           
            List<UserRoles> roles = db.UserRoles.Where(r => r.UserId == id).ToList();
            List<string> userRoles = new List<string>();
           
            foreach (var role in roles)
            {
                userRoles.Add(role.Roles.RoleName);
            }
            
            List<Medications> medications = db.Medications.Where(r => r.MedicalConditions.UserId == id).ToList();
            var dtoMedications = new List<object>();




            List<Reports> reports = db.Reports.Where(r => r.UserId == id).ToList();
            var dtoReports = new List<object>();
            foreach (var i in reports)
            {
                var report = new
                {
                    IdReports = i.ReportId,
                    Description=i.Description,
                    FileName = i.FileName,
                    FilePath=i.FilePath,
                    UserId = i.UserId,
                    ReportDate = i.ReportDate,
                    ReportType= i.ReportsType.Type
                };
                dtoReports.Add(report);
            }

            List<MedicalConditions> conditions = db.MedicalConditions.Where(r => r.UserId== id).ToList();
            var dtoConditions=new List<object>();
            foreach (var i in conditions)
            {
                var medCondition = new
                {
                    IdMedicalConditions=i.MedicalConditionId,
                    Description = i.Description,
                    UserId=i.UserId,
                    DiagnosisYear=i.DiagnosisYear

                };
                
                dtoConditions.Add(medCondition);
            }

            List<TherapeuticPlans> therapeuticPlans = db.TherapeuticPlans.Where(r => r.MedicalConditions.UserId == id).ToList();
            var dtoTherapeuticPlans = new List<object>();

            foreach (var i in therapeuticPlans)
            {
                var therapeuticPlan = new
                {
                    PlanId = i.PlanId,
                    MedicalConditionId = i.MedicalConditionId,
                    PlanDescription = i.PlanDescription,
                    StartDate = i.StartDate,
                    ExpiryDate = i.ExpiryDate,

                };
                dtoTherapeuticPlans.Add(therapeuticPlan);
            }




            var response = new
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                DateOfBirth = user.DateOfBirth,
                CF = user.CF,
                Address = user.Address,
                City = user.City,
                Province = user.Province,
                Country = user.Country,
                Email = user.Email,
                roles = userRoles,
                Reports=dtoReports,
                MedicalConditions=dtoConditions,
                TherapeuticPlans= dtoTherapeuticPlans

            };

            return Ok(response);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUsers(int id, Users user)
        {
                      
            if (id != user.UserId)
            {
                return BadRequest();
            }

            var password = db.Users.Where(u => u.UserId == id).Select(u => u.Password).FirstOrDefault();

            if (password == null)
            {
                return NotFound();
            }

            user.Password = password;


            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
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


        [HttpPatch]
        [Route("api/changePassword/{id}")]
        public IHttpActionResult PutPasswordUser(string id, PswChange PswChangeDto)
        {
            int ID = Convert.ToInt32(id);

            Users existingUser = db.Users.FirstOrDefault(u => u.UserId ==ID);

            if (existingUser == null)
            {
                return NotFound();
            }

            bool verified = BCrypt.Net.BCrypt.Verify(PswChangeDto.CurrentPsw, existingUser.Password);

            if (!verified)
            {
                return BadRequest();
            }
            string hashPassword = BCrypt.Net.BCrypt.HashPassword(PswChangeDto.NewPsw);

            existingUser.Password = hashPassword;

            db.Entry(existingUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();

            }
            return Ok();

        }




        // POST: api/Users
        [ResponseType(typeof(Users))]
        public IHttpActionResult PostUsers(Users users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(users);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = users.UserId }, users);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(Users))]
        public IHttpActionResult DeleteUsers(int id)
        {
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return NotFound();
            }

            db.Users.Remove(users);
            db.SaveChanges();

            return Ok(users);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsersExists(int id)
        {
            return db.Users.Count(e => e.UserId == id) > 0;
        }
    }
}