namespace API_MediBridge.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DoctorUserBugFix : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Doctors", "UsersID");
            RenameColumn(table: "dbo.Doctors", name: "User_UserId", newName: "UsersID");
            RenameIndex(table: "dbo.Doctors", name: "IX_User_UserId", newName: "IX_UsersID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Doctors", name: "IX_UsersID", newName: "IX_User_UserId");
            RenameColumn(table: "dbo.Doctors", name: "UsersID", newName: "User_UserId");
            AddColumn("dbo.Doctors", "UsersID", c => c.Int(nullable: false));
        }
    }
}
