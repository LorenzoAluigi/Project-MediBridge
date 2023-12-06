using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_MediBridge.Models.Class
{
    public class DoctorDTO
    {
        public int DoctorId {  get; set; }
        public string DoctorName { get; set; }
        public string DoctorSurname { get; set; }

        public string DoctorSpecialization { get; set;}

        public int UsersId { get; set; }

    }
}