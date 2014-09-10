namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedReferencesId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BootCampSessions", "Bootcamp_Id", "dbo.Bootcamps");
            DropForeignKey("dbo.Comments", "Session_Id", "dbo.BootCampSessions");
            DropIndex("dbo.BootCampSessions", new[] { "Bootcamp_Id" });
            DropIndex("dbo.Comments", new[] { "Session_Id" });
            RenameColumn(table: "dbo.BootCampSessions", name: "Bootcamp_Id", newName: "BootcampId");
            RenameColumn(table: "dbo.Comments", name: "Session_Id", newName: "SessionId");
            AlterColumn("dbo.BootCampSessions", "BootcampId", c => c.Int(nullable: false));
            AlterColumn("dbo.Comments", "SessionId", c => c.Int(nullable: false));
            CreateIndex("dbo.BootCampSessions", "BootcampId");
            CreateIndex("dbo.Comments", "SessionId");
            AddForeignKey("dbo.BootCampSessions", "BootcampId", "dbo.Bootcamps", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Comments", "SessionId", "dbo.BootCampSessions", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "SessionId", "dbo.BootCampSessions");
            DropForeignKey("dbo.BootCampSessions", "BootcampId", "dbo.Bootcamps");
            DropIndex("dbo.Comments", new[] { "SessionId" });
            DropIndex("dbo.BootCampSessions", new[] { "BootcampId" });
            AlterColumn("dbo.Comments", "SessionId", c => c.Int());
            AlterColumn("dbo.BootCampSessions", "BootcampId", c => c.Int());
            RenameColumn(table: "dbo.Comments", name: "SessionId", newName: "Session_Id");
            RenameColumn(table: "dbo.BootCampSessions", name: "BootcampId", newName: "Bootcamp_Id");
            CreateIndex("dbo.Comments", "Session_Id");
            CreateIndex("dbo.BootCampSessions", "Bootcamp_Id");
            AddForeignKey("dbo.Comments", "Session_Id", "dbo.BootCampSessions", "Id");
            AddForeignKey("dbo.BootCampSessions", "Bootcamp_Id", "dbo.Bootcamps", "Id");
        }
    }
}
