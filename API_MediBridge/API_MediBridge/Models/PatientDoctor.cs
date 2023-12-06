using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace API_MediBridge.Models
{
    public class PatientDoctor
    {
        


        [Key]
        public int PatientDoctorId {  get; set; }

        

        public int DoctorsId {  get; set; }

        [ForeignKey("User")]
        public int UsersId {  get; set; }

        public virtual Doctors Doctors { get; set; }
        public virtual Users User { get; set; }

    }
}