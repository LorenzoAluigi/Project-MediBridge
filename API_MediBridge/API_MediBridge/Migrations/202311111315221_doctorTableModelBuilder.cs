namespace API_MediBridge.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class doctorTableModelBuilder : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PatientDoctors", "DoctorsId", "dbo.Doctors");
            AddForeignKey("dbo.PatientDoctors", "DoctorsId", "dbo.Doctors", "DoctorsID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PatientDoctors", "DoctorsId", "dbo.Doctors");
            AddForeignKey("dbo.PatientDoctors", "DoctorsId", "dbo.Doctors", "DoctorsID", cascadeDelete: true);
        }
    }
}
