namespace API_MediBridge.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PatientDoctorKeyNameFix : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.PatientDoctors");
            DropColumn("dbo.PatientDoctors", "PatienDoctorId");
            AddColumn("dbo.PatientDoctors", "PatientDoctorId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.PatientDoctors", "PatientDoctorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PatientDoctors", "PatienDoctorId", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.PatientDoctors");
            DropColumn("dbo.PatientDoctors", "PatientDoctorId");
            AddPrimaryKey("dbo.PatientDoctors", "PatienDoctorId");
        }
    }
}
