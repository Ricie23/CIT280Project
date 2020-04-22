namespace CIT280App.DataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Intial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Role = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Description = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        ProfilePic = c.String(),
                        Reviews = c.String(),
                        BuisnessName = c.String(),
                        BuisnessType = c.String(),
                        Major = c.String(),
                        School = c.String(),
                        YearInSchool = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.JobsModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        City = c.String(),
                        State = c.String(),
                        RequiredSkills = c.String(),
                        Photo = c.String(),
                        Pay = c.Int(nullable: false),
                        IsComplete = c.Boolean(nullable: false),
                        Employer_ID = c.Int(),
                        Student_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UserModel", t => t.Employer_ID)
                .ForeignKey("dbo.UserModel", t => t.Student_ID)
                .ForeignKey("dbo.UserModel", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.Employer_ID)
                .Index(t => t.Student_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobsModel", "UserID", "dbo.UserModel");
            DropForeignKey("dbo.JobsModel", "Student_ID", "dbo.UserModel");
            DropForeignKey("dbo.JobsModel", "Employer_ID", "dbo.UserModel");
            DropIndex("dbo.JobsModel", new[] { "Student_ID" });
            DropIndex("dbo.JobsModel", new[] { "Employer_ID" });
            DropIndex("dbo.JobsModel", new[] { "UserID" });
            DropTable("dbo.JobsModel");
            DropTable("dbo.UserModel");
        }
    }
}
