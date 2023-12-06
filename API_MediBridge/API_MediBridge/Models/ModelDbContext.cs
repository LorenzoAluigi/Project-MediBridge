using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace API_MediBridge.Models
{
    public partial class ModelDbContext : DbContext
    {
        public ModelDbContext()
            : base("name=ModelDbContext")
        {
           
        }

        public virtual DbSet<Appointments> Appointments { get; set; }
        public virtual DbSet<AvailableTimes> AvailableTimes { get; set; }
        public virtual DbSet<MedicalConditions> MedicalConditions { get; set; }
        public virtual DbSet<Medications> Medications { get; set; }
        public virtual DbSet<Reports> Reports { get; set; }
        public virtual DbSet<ReportsType> ReportsType { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TherapeuticPlans> TherapeuticPlans { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        public virtual DbSet<Doctors> Doctors { get; set; }

        public virtual DbSet<PatientDoctor> PatientDoctors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AvailableTimes>()
                .HasMany(e => e.Appointments)
                .WithRequired(e => e.AvailableTimes)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MedicalConditions>()
                .HasMany(e => e.Medications)
                .WithRequired(e => e.MedicalConditions)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MedicalConditions>()
                .HasMany(e => e.TherapeuticPlans)
                .WithRequired(e => e.MedicalConditions)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ReportsType>()
                .HasMany(e => e.Reports)
                .WithRequired(e => e.ReportsType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Roles>()
                .HasMany(e => e.UserRoles)
                .WithRequired(e => e.Roles)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Appointments)
                .WithRequired(e => e.Users)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.AvailableTimes)
                .WithRequired(e => e.Users)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.MedicalConditions)
                .WithRequired(e => e.Users)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Reports)
                .WithRequired(e => e.Users)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.UserRoles)
                .WithRequired(e => e.Users)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e=>e.Doctors).
                WithRequired(e=>e.User).
                WillCascadeOnDelete(false);

            modelBuilder.Entity<PatientDoctor>()
                .HasRequired(pd => pd.User)
                .WithMany(u => u.PatientDoctors)
                .HasForeignKey(pd => pd.UsersId);

            modelBuilder.Entity<PatientDoctor>()
                .HasRequired(pd => pd.Doctors)
                .WithMany(d => d.PatientDoctors)
                .HasForeignKey(pd => pd.DoctorsId);

            modelBuilder.Entity<Doctors>()
               .HasMany(d => d.PatientDoctors)
               .WithRequired(pd => pd.Doctors)
               .HasForeignKey(pd => pd.DoctorsId);




        }
    }
}
