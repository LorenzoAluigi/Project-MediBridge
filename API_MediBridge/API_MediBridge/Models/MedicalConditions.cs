namespace API_MediBridge.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MedicalConditions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MedicalConditions()
        {
            Medications = new HashSet<Medications>();
            TherapeuticPlans = new HashSet<TherapeuticPlans>();
        }

        [Key]
        public int MedicalConditionId { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        public int UserId { get; set; }

        public int? DiagnosisYear { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Medications> Medications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TherapeuticPlans> TherapeuticPlans { get; set; }

        public virtual Users Users { get; set; }
    }
}
