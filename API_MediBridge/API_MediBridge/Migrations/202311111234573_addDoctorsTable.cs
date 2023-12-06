namespace API_MediBridge.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDoctorsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        DoctorsID = c.Int(nullable: false, identity: true),
                        Specialization = c.String(),
                        UsersID = c.Int(nullable: false),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.DoctorsID)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.User_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Doctors", "User_UserId", "dbo.Users");
            DropIndex("dbo.Doctors", new[] { "User_UserId" });
            DropTable("dbo.Doctors");
        }
    }
}
