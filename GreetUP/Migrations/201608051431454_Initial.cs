namespace GreetUP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventID = c.Int(nullable: false, identity: true),
                        EventName = c.String(maxLength: 50),
                        Time = c.String(maxLength: 100),
                        Date = c.String(maxLength: 50),
                        Description = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.EventID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.RSVPs",
                c => new
                    {
                        RSVPID = c.Int(nullable: false, identity: true),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        Event_EventID = c.Int(),
                    })
                .PrimaryKey(t => t.RSVPID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Events", t => t.Event_EventID)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Event_EventID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RSVPs", "Event_EventID", "dbo.Events");
            DropForeignKey("dbo.RSVPs", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Events", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.RSVPs", new[] { "Event_EventID" });
            DropIndex("dbo.RSVPs", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Events", new[] { "ApplicationUser_Id" });
            DropTable("dbo.RSVPs");
            DropTable("dbo.Events");
        }
    }
}
