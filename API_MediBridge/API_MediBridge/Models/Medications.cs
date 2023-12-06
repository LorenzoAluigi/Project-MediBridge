namespace API_MediBridge.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Medications
    {
        [Key]
        public int MedicationId { get; set; }

        [Required]
        [StringLength(100)]
        public string MedicationName { get; set; }

        [Required]
        [StringLength(50)]
        public string DailyDosage { get; set; }

        public int MedicalConditionId { get; set; }

        public int? PlanId { get; set; }

        public virtual MedicalConditions MedicalConditions { get; set; }

        public virtual TherapeuticPlans TherapeuticPlans { get; set; }
    }
}
