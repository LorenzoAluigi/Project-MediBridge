using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API_MediBridge.Models.Class
{
    public class ReportDto
    {
        public string IdReport { get; set; }

        [Required]
        [StringLength(255)]
        public string description { get; set; }
        [Required]
        public int userId { get; set; }

        [Column(TypeName = "date")]
        public DateTime reportDate { get; set; }


        public int reportTypeId { get; set; }

        [Required]
        public string fileBase64 { get; set; } 

        [Required]
        public string fileName { get; set; }

        public string reportsType { get; set; }
    }
}