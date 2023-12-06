using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_MediBridge.Models.Class
{
    public class PatientByDoctorDTO
    {
        public int PatientDoctorID { get; set; }
        public int DoctorId { get; set; }

        public PatientDTO Patient { get; set; }

        
    }
}