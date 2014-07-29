namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DocumentsDatabaseCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        PresentationID = c.Int(nullable: false),
                        CommentText = c.String(),
                        TimeStamp = c.DateTime(nullable: false),
                        ParentCommentID = c.Int(nullable: false),
                        Likes = c.Int(nullable: false),
                        DisLikes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PresentationDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PresentationsRepositories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PresentationID = c.Int(nullable: false),
                        FileLocation = c.String(),
                        Category = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        PresentationID = c.Int(nullable: false),
                        ScoreID = c.Int(nullable: false),
                        VotedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ScoresMasters",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PresentationID = c.Int(nullable: false),
                        Score1 = c.Int(nullable: false),
                        Score2 = c.Int(nullable: false),
                        Score3 = c.Int(nullable: false),
                        Score4 = c.Int(nullable: false),
                        Score5 = c.Int(nullable: false),
                        AverageScore = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserAssociationWithPresentations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        PresentationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserRolesMasters",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserEmail = c.String(),
                        UserName = c.String(),
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.UserRolesMasters");
            DropTable("dbo.UserAssociationWithPresentations");
            DropTable("dbo.ScoresMasters");
            DropTable("dbo.Ratings");
            DropTable("dbo.PresentationsRepositories");
            DropTable("dbo.PresentationDetails");
            DropTable("dbo.Comments");
        }
    }
}
