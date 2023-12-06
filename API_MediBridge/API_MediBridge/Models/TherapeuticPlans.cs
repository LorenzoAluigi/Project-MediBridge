namespace API_MediBridge.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TherapeuticPlans
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TherapeuticPlans()
        {
            Medications = new HashSet<Medications>();
        }

        [Key]
        public int PlanId { get; set; }

        public int MedicalConditionId { get; set; }

        [Required]
        [StringLength(255)]
        public string PlanDescription { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime ExpiryDate { get; set; }

        public virtual MedicalConditions MedicalConditions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Medications> Medications { get; set; }
    }
}
