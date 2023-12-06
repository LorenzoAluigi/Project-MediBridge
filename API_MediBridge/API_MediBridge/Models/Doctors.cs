using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace API_MediBridge.Models
{
    
    public class Doctors
    {
        public Doctors() {
        
            PatientDoctors=new HashSet<PatientDoctor>();
        }
        
        [Key]
        public int DoctorsID {  get; set; }

        public string Specialization { get; set; }

        [ForeignKey("User")]
        public int UsersID { get; set; }

        public virtual Users User { get; set; }

        public virtual ICollection<PatientDoctor> PatientDoctors { get; set; }

    }
}