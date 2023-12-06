namespace API_MediBridge.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbInit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        AppointmentId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        TimeSlotId = c.Int(nullable: false),
                        Notes = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.AppointmentId)
                .ForeignKey("dbo.AvailableTimes", t => t.TimeSlotId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.TimeSlotId);
            
            CreateTable(
                "dbo.AvailableTimes",
                c => new
                    {
                        TimeSlotId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        DayOfWeek = c.String(nullable: false, maxLength: 20),
                        StartTime = c.Time(nullable: false, precision: 7),
                        EndTime = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.TimeSlotId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Gender = c.String(nullable: false, maxLength: 10),
                        DateOfBirth = c.DateTime(nullable: false, storeType: "date"),
                        CF = c.String(nullable: false, maxLength: 16),
                        Address = c.String(nullable: false, maxLength: 255),
                        City = c.String(nullable: false, maxLength: 100),
                        Province = c.String(nullable: false, maxLength: 50),
                        Country = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.MedicalConditions",
                c => new
                    {
                        MedicalConditionId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 255),
                        UserId = c.Int(nullable: false),
                        DiagnosisYear = c.Int(),
                    })
                .PrimaryKey(t => t.MedicalConditionId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Medications",
                c => new
                    {
                        MedicationId = c.Int(nullable: false, identity: true),
                        MedicationName = c.String(nullable: false, maxLength: 100),
                        DailyDosage = c.String(nullable: false, maxLength: 50),
                        MedicalConditionId = c.Int(nullable: false),
                        PlanId = c.Int(),
                    })
                .PrimaryKey(t => t.MedicationId)
                .ForeignKey("dbo.TherapeuticPlans", t => t.PlanId)
                .ForeignKey("dbo.MedicalConditions", t => t.MedicalConditionId)
                .Index(t => t.MedicalConditionId)
                .Index(t => t.PlanId);
            
            CreateTable(
                "dbo.TherapeuticPlans",
                c => new
                    {
                        PlanId = c.Int(nullable: false, identity: true),
                        MedicalConditionId = c.Int(nullable: false),
                        PlanDescription = c.String(nullable: false, maxLength: 255),
                        StartDate = c.DateTime(nullable: false, storeType: "date"),
                        ExpiryDate = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.PlanId)
                .ForeignKey("dbo.MedicalConditions", t => t.MedicalConditionId)
                .Index(t => t.MedicalConditionId);
            
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        ReportId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 255),
                        FileName = c.String(nullable: false, maxLength: 100),
                        FilePath = c.String(nullable: false, maxLength: 255),
                        UserId = c.Int(nullable: false),
                        ReportDate = c.DateTime(nullable: false, storeType: "date"),
                        ReportTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReportId)
                .ForeignKey("dbo.ReportsType", t => t.ReportTypeId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ReportTypeId);
            
            CreateTable(
                "dbo.ReportsType",
                c => new
                    {
                        ReportTypeId = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ReportTypeId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserRoleId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserRoleId)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Reports", "UserId", "dbo.Users");
            DropForeignKey("dbo.Reports", "ReportTypeId", "dbo.ReportsType");
            DropForeignKey("dbo.MedicalConditions", "UserId", "dbo.Users");
            DropForeignKey("dbo.TherapeuticPlans", "MedicalConditionId", "dbo.MedicalConditions");
            DropForeignKey("dbo.Medications", "MedicalConditionId", "dbo.MedicalConditions");
            DropForeignKey("dbo.Medications", "PlanId", "dbo.TherapeuticPlans");
            DropForeignKey("dbo.AvailableTimes", "UserId", "dbo.Users");
            DropForeignKey("dbo.Appointments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Appointments", "TimeSlotId", "dbo.AvailableTimes");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.Reports", new[] { "ReportTypeId" });
            DropIndex("dbo.Reports", new[] { "UserId" });
            DropIndex("dbo.TherapeuticPlans", new[] { "MedicalConditionId" });
            DropIndex("dbo.Medications", new[] { "PlanId" });
            DropIndex("dbo.Medications", new[] { "MedicalConditionId" });
            DropIndex("dbo.MedicalConditions", new[] { "UserId" });
            DropIndex("dbo.AvailableTimes", new[] { "UserId" });
            DropIndex("dbo.Appointments", new[] { "TimeSlotId" });
            DropIndex("dbo.Appointments", new[] { "UserId" });
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.Roles");
            DropTable("dbo.UserRoles");
            DropTable("dbo.ReportsType");
            DropTable("dbo.Reports");
            DropTable("dbo.TherapeuticPlans");
            DropTable("dbo.Medications");
            DropTable("dbo.MedicalConditions");
            DropTable("dbo.Users");
            DropTable("dbo.AvailableTimes");
            DropTable("dbo.Appointments");
        }
    }
}
