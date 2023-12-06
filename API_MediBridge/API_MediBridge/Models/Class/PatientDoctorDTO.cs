using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_MediBridge.Models.Class
{
    public class PatientDoctorDTO
    {
        public int PatientDoctorID {  get; set; }
        public int UserId {  get; set; }

       public  DoctorDTO Doctor { get; set; }

    }
}