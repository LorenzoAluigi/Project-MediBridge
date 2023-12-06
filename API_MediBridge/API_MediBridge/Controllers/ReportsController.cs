using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Description;
using API_MediBridge.Filters;
using API_MediBridge.Models;
using API_MediBridge.Models.Class;

namespace API_MediBridge.Controllers
{
    [JwtAuthorization]
    public class ReportsController : ApiController
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: api/Reports
        public IQueryable<Reports> GetReports()
        {
            return db.Reports;
        }

        // GET: api/Reports/5
        [HttpGet]
        [Route("api/reports/{id}")]
        public IHttpActionResult GetReports(string id)
        {
            int ID = Convert.ToInt32(id);
            List<Reports> reports = db.Reports.Where(e=>e.UserId==ID).ToList();
            if (reports == null)
            {
                return NotFound();
            }
            var serializedReports = new List<object>();
            foreach (var report in reports)
            {
                
                var serializedReport = new
                {
                    IdReport = report.ReportId,
                    description = report.Description,
                    userId = report.UserId,
                    reportDate = report.ReportDate,
                    reportTypeId = report.ReportTypeId,
                    fileName = report.FileName,
                    filepath=report.FilePath,
                    reportsType = report.ReportsType.Type,

                };
                serializedReports.Add(serializedReport);
            }




            return Ok(serializedReports);
        }

        // PUT: api/Reports/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReports(int id, Reports reports)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reports.ReportId)
            {
                return BadRequest();
            }

            db.Entry(reports).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportsExists(id))
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

        // POST: api/Reports
        [HttpPost]
        [Route("api/report")]
        public IHttpActionResult PostReports([FromBody] ReportDto report)
        {
            if (report!=null)
            {
                string savePath;
                string uniqueFileName;

                    try
                    {

                        // Convert the Base64 field into a byte array
                        byte[] fileBytes = Convert.FromBase64String(report.fileBase64);

                        // Specify the virtual path where to save the file (adjust this path according to your needs)
                        string virtualPath = "~/Uploads/" + report.userId;

                        // Map the virtual path to a physical path
                        string physicalPath = HttpContext.Current.Server.MapPath(virtualPath);

                        // Check if the physical directory exists, create it if it doesn't
                        if (!Directory.Exists(physicalPath))
                        {
                            Directory.CreateDirectory(physicalPath);
                        }

                        // Generate a unique file name
                        uniqueFileName = GenerateUniqueFileName(report.fileName);

                        // Compose the full file path, including the unique file name
                        savePath = Path.Combine(physicalPath, uniqueFileName);

                        // Save the byte array as a file
                        File.WriteAllBytes(savePath, fileBytes);

                        //// Return a success response
                        //return Ok("File saved successfully.");
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions
                        return BadRequest("Error while saving the file: " + ex.Message);
                    }

                Reports newReports = new Reports
                {
                    Description = report.description,
                    FileName = uniqueFileName,
                    FilePath = savePath,
                    UserId = report.userId,
                    ReportDate = report.reportDate,
                    ReportTypeId = report.reportTypeId
                };

                try
                {
                    db.Reports.Add(newReports);
                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    return BadRequest("db save failed: " + ex.Message);
                }

                return Ok("report saved successfully.");

            }

            return BadRequest("Fill in all required fields");

        }

            // Generate a unique file name using the current timestamp
        private string GenerateUniqueFileName(string originalFileName)
        {
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string uniqueFileName = $"{timestamp}_{originalFileName}";

            return uniqueFileName;
        }


        [HttpGet]
        [Route("api/file/{id}/{fileName}")]
        public IHttpActionResult GetFileReports(string id, string fileName)
        {
            int ID = Convert.ToInt32(id);

            string rootPath = System.Web.Hosting.HostingEnvironment.MapPath("~");
            string filePath = Path.Combine(rootPath, $"Uploads/{id}/{fileName}.pdf");
           
            try
            {
                byte[] fileBytes = File.ReadAllBytes(filePath);
                string base64String = Convert.ToBase64String(fileBytes);

                var response = new {status= 200, file64=base64String};

                // Restituisci la stringa Base64 come parte della risposta
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Gestisci eventuali eccezioni
                var response = new {status= 500};
                return BadRequest(ex.Message+response);
            }
        }









        // DELETE: api/Reports/5
        [ResponseType(typeof(Reports))]
        public IHttpActionResult DeleteReports(int id)
        {
            Reports reports = db.Reports.Find(id);
            if (reports == null)
            {
                return NotFound();
            }

            db.Reports.Remove(reports);
            db.SaveChanges();

            return Ok(reports);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReportsExists(int id)
        {
            return db.Reports.Count(e => e.ReportId == id) > 0;
        }
    }
}