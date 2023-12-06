namespace API_MediBridge.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPatientDoctorTable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Doctors", new[] { "User_UserId" });
            CreateTable(
                "dbo.PatientDoctors",
                c => new
                    {
                        PatienDoctorId = c.Int(nullable: false, identity: true),
                        DoctorsId = c.Int(nullable: false),
                        UsersId = c.Int(nullable: false),
                        User_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PatienDoctorId)
                .ForeignKey("dbo.Doctors", t => t.DoctorsId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.DoctorsId)
                .Index(t => t.User_UserId);
            
            AlterColumn("dbo.Doctors", "User_UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Doctors", "User_UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PatientDoctors", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.PatientDoctors", "DoctorsId", "dbo.Doctors");
            DropIndex("dbo.PatientDoctors", new[] { "User_UserId" });
            DropIndex("dbo.PatientDoctors", new[] { "DoctorsId" });
            DropIndex("dbo.Doctors", new[] { "User_UserId" });
            AlterColumn("dbo.Doctors", "User_UserId", c => c.Int());
            DropTable("dbo.PatientDoctors");
            CreateIndex("dbo.Doctors", "User_UserId");
        }
    }
}
