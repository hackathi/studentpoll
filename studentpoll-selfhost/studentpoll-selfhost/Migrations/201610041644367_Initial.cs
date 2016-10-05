namespace studentpoll.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PollAnswers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        PollId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Polls", t => t.PollId, cascadeDelete: true)
                .Index(t => t.PollId);
            
            CreateTable(
                "dbo.Polls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PollVotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PollAnswerId = c.Int(nullable: false),
                        PollId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PollAnswers", t => t.PollAnswerId, cascadeDelete: true)
                .Index(t => t.PollAnswerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PollVotes", "PollAnswerId", "dbo.PollAnswers");
            DropForeignKey("dbo.PollAnswers", "PollId", "dbo.Polls");
            DropIndex("dbo.PollVotes", new[] { "PollAnswerId" });
            DropIndex("dbo.PollAnswers", new[] { "PollId" });
            DropTable("dbo.PollVotes");
            DropTable("dbo.Polls");
            DropTable("dbo.PollAnswers");
        }
    }
}
