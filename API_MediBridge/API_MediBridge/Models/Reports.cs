namespace API_MediBridge.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Reports
    {
        [Key]
        public int ReportId { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        [StringLength(100)]
        public string FileName { get; set; }

        [Required]
        [StringLength(255)]
        public string FilePath { get; set; }

        public int UserId { get; set; }

        [Column(TypeName = "date")]
        public DateTime ReportDate { get; set; }

        public int ReportTypeId { get; set; }

        public virtual ReportsType ReportsType { get; set; }

        public virtual Users Users { get; set; }



    }
}
