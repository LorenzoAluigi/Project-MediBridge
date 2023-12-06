namespace API_MediBridge.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Appointments
    {
        [Key]
        public int AppointmentId { get; set; }

        public int UserId { get; set; }

        public int TimeSlotId { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        public virtual AvailableTimes AvailableTimes { get; set; }

        public virtual Users Users { get; set; }
    }
}
