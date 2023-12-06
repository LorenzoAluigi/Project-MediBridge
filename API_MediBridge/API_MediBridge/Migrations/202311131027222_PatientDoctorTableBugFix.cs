namespace API_MediBridge.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PatientDoctorTableBugFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PatientDoctors", "DoctorsId", "dbo.Doctors");
            DropIndex("dbo.PatientDoctors", new[] { "User_UserId" });
            AlterColumn("dbo.PatientDoctors", "User_UserId", c => c.Int());
            CreateIndex("dbo.PatientDoctors", "User_UserId");
            AddForeignKey("dbo.PatientDoctors", "DoctorsId", "dbo.Doctors", "DoctorsID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PatientDoctors", "DoctorsId", "dbo.Doctors");
            DropIndex("dbo.PatientDoctors", new[] { "User_UserId" });
            AlterColumn("dbo.PatientDoctors", "User_UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.PatientDoctors", "User_UserId");
            AddForeignKey("dbo.PatientDoctors", "DoctorsId", "dbo.Doctors", "DoctorsID");
        }
    }
}
