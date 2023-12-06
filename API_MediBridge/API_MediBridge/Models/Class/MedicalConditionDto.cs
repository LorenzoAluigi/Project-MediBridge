using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API_MediBridge.Models.Class
{
    public class MedicalConditionDto
    {
               
        public string Description { get; set; }

        public int UserId { get; set; }

        public int? DiagnosisYear { get; set; }

    }
}