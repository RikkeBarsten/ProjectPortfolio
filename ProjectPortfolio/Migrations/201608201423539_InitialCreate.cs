namespace ProjectPortfolio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AProjects",
                c => new
                    {
                        ProjectId = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        Remark = c.String(),
                        Person = c.String(),
                        Responsible = c.String(),
                        ProgramId = c.String(nullable: false, maxLength: 128),
                        Budget = c.Decimal(precision: 18, scale: 2),
                        SelfFinancing = c.Decimal(precision: 18, scale: 2),
                        Owner = c.Int(),
                        ProjectNumber = c.Int(),
                        Status = c.Int(),
                        ExtProjectNumber = c.String(),
                        FunderId = c.Int(),
                        Hours = c.Int(),
                        SteeringCommittee = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.Programs", t => t.ProgramId, cascadeDelete: true)
                .ForeignKey("dbo.Funders", t => t.FunderId)
                .Index(t => t.ProgramId)
                .Index(t => t.FunderId);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        ContentType = c.String(maxLength: 100),
                        Content = c.Binary(),
                        FileType = c.Int(nullable: false),
                        ProjectId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.AProjects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Programs",
                c => new
                    {
                        ProgramId = c.String(nullable: false, maxLength: 128),
                        ProgramName = c.String(),
                    })
                .PrimaryKey(t => t.ProgramId);
            
            CreateTable(
                "dbo.Funders",
                c => new
                    {
                        FunderId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.FunderId);
            
            CreateTable(
                "dbo.Deadlines",
                c => new
                    {
                        DeadlineId = c.Guid(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        FunderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DeadlineId)
                .ForeignKey("dbo.Funders", t => t.FunderId, cascadeDelete: true)
                .Index(t => t.FunderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AProjects", "FunderId", "dbo.Funders");
            DropForeignKey("dbo.Deadlines", "FunderId", "dbo.Funders");
            DropForeignKey("dbo.Files", "ProjectId", "dbo.AProjects");
            DropForeignKey("dbo.AProjects", "ProgramId", "dbo.Programs");
            DropIndex("dbo.Deadlines", new[] { "FunderId" });
            DropIndex("dbo.Files", new[] { "ProjectId" });
            DropIndex("dbo.AProjects", new[] { "FunderId" });
            DropIndex("dbo.AProjects", new[] { "ProgramId" });
            DropTable("dbo.Deadlines");
            DropTable("dbo.Funders");
            DropTable("dbo.Programs");
            DropTable("dbo.Files");
            DropTable("dbo.AProjects");
        }
    }
}
