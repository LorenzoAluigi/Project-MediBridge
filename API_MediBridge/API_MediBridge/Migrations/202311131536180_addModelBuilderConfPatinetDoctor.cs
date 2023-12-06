namespace API_MediBridge.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addModelBuilderConfPatinetDoctor : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PatientDoctors", "User_UserId", "dbo.Users");
            DropIndex("dbo.PatientDoctors", new[] { "User_UserId" });
            DropColumn("dbo.PatientDoctors", "UsersId");
            RenameColumn(table: "dbo.PatientDoctors", name: "User_UserId", newName: "UsersId");
            AlterColumn("dbo.PatientDoctors", "UsersId", c => c.Int(nullable: true));
            CreateIndex("dbo.PatientDoctors", "UsersId");
            AddForeignKey("dbo.PatientDoctors", "UsersId", "dbo.Users", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PatientDoctors", "UsersId", "dbo.Users");
            DropIndex("dbo.PatientDoctors", new[] { "UsersId" });
            AlterColumn("dbo.PatientDoctors", "UsersId", c => c.Int());
            RenameColumn(table: "dbo.PatientDoctors", name: "UsersId", newName: "User_UserId");
            AddColumn("dbo.PatientDoctors", "UsersId", c => c.Int(nullable: true));
            CreateIndex("dbo.PatientDoctors", "User_UserId");
            AddForeignKey("dbo.PatientDoctors", "User_UserId", "dbo.Users", "UserId");
        }
    }
}
