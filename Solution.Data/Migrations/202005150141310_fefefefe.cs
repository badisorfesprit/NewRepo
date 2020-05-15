namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fefefefe : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        idUser = c.Int(nullable: false),
                        idPub = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.idUser, t.idPub })
                .ForeignKey("dbo.Publications", t => t.idPub, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.idUser, cascadeDelete: true)
                .Index(t => t.idUser)
                .Index(t => t.idPub);
            
            CreateTable(
                "dbo.Publications",
                c => new
                    {
                        PublicationId = c.Int(nullable: false, identity: true),
                        title = c.String(),
                        description = c.String(),
                        image = c.String(),
                        visibility = c.Int(nullable: false),
                        creationDate = c.DateTime(nullable: false),
                        ownerimg = c.String(),
                        nomuser = c.String(),
                        OwnerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PublicationId)
                .ForeignKey("dbo.Users", t => t.OwnerId)
                .Index(t => t.OwnerId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        Role = c.String(),
                        image = c.String(),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Publication_PublicationId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Publications", t => t.Publication_PublicationId)
                .Index(t => t.Publication_PublicationId);
            
            CreateTable(
                "dbo.CustomUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Reclamations",
                c => new
                    {
                        ReclamationId = c.Int(nullable: false, identity: true),
                        Comment = c.String(nullable: false),
                        DateReclamation = c.DateTime(nullable: false),
                        ComplaintType = c.Int(nullable: false),
                        state = c.Int(nullable: false),
                        senderID = c.Int(nullable: false),
                        receiverID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReclamationId)
                .ForeignKey("dbo.Users", t => t.receiverID)
                .ForeignKey("dbo.Users", t => t.senderID)
                .Index(t => t.senderID)
                .Index(t => t.receiverID);
            
            CreateTable(
                "dbo.Responses",
                c => new
                    {
                        ResponseId = c.Int(nullable: false, identity: true),
                        response = c.String(nullable: false),
                        DateResponse = c.DateTime(nullable: false),
                        reclamationID = c.Int(nullable: false),
                        authorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ResponseId)
                .ForeignKey("dbo.Users", t => t.authorId)
                .ForeignKey("dbo.Reclamations", t => t.reclamationID)
                .Index(t => t.reclamationID)
                .Index(t => t.authorId);
            
            CreateTable(
                "dbo.CustomUserLogins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.CustomUserRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        CustomRole_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.CustomRoles", t => t.CustomRole_Id)
                .Index(t => t.UserId)
                .Index(t => t.CustomRole_Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        Contenu = c.String(),
                        PublicationId = c.Int(),
                        ownerimg = c.String(),
                        nomuser = c.String(),
                        OwnerId = c.String(),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Publications", t => t.PublicationId, cascadeDelete: true)
                .Index(t => t.PublicationId);
            
            CreateTable(
                "dbo.Replies",
                c => new
                    {
                        ReplyId = c.Int(nullable: false, identity: true),
                        Contenu = c.String(),
                        CommentId = c.Int(nullable: false),
                        ownerimg = c.String(),
                        nomuser = c.String(),
                        OwnerId = c.String(),
                    })
                .PrimaryKey(t => t.ReplyId)
                .ForeignKey("dbo.Comments", t => t.CommentId, cascadeDelete: true)
                .Index(t => t.CommentId);
            
            CreateTable(
                "dbo.CustomRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.IdentityUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomUserRoles", "CustomRole_Id", "dbo.CustomRoles");
            DropForeignKey("dbo.Likes", "idUser", "dbo.Users");
            DropForeignKey("dbo.Likes", "idPub", "dbo.Publications");
            DropForeignKey("dbo.Publications", "OwnerId", "dbo.Users");
            DropForeignKey("dbo.Comments", "PublicationId", "dbo.Publications");
            DropForeignKey("dbo.Replies", "CommentId", "dbo.Comments");
            DropForeignKey("dbo.Users", "Publication_PublicationId", "dbo.Publications");
            DropForeignKey("dbo.CustomUserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.CustomUserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.Reclamations", "senderID", "dbo.Users");
            DropForeignKey("dbo.Responses", "reclamationID", "dbo.Reclamations");
            DropForeignKey("dbo.Responses", "authorId", "dbo.Users");
            DropForeignKey("dbo.Reclamations", "receiverID", "dbo.Users");
            DropForeignKey("dbo.CustomUserClaims", "UserId", "dbo.Users");
            DropIndex("dbo.Replies", new[] { "CommentId" });
            DropIndex("dbo.Comments", new[] { "PublicationId" });
            DropIndex("dbo.CustomUserRoles", new[] { "CustomRole_Id" });
            DropIndex("dbo.CustomUserRoles", new[] { "UserId" });
            DropIndex("dbo.CustomUserLogins", new[] { "UserId" });
            DropIndex("dbo.Responses", new[] { "authorId" });
            DropIndex("dbo.Responses", new[] { "reclamationID" });
            DropIndex("dbo.Reclamations", new[] { "receiverID" });
            DropIndex("dbo.Reclamations", new[] { "senderID" });
            DropIndex("dbo.CustomUserClaims", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "Publication_PublicationId" });
            DropIndex("dbo.Publications", new[] { "OwnerId" });
            DropIndex("dbo.Likes", new[] { "idPub" });
            DropIndex("dbo.Likes", new[] { "idUser" });
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.CustomRoles");
            DropTable("dbo.Replies");
            DropTable("dbo.Comments");
            DropTable("dbo.CustomUserRoles");
            DropTable("dbo.CustomUserLogins");
            DropTable("dbo.Responses");
            DropTable("dbo.Reclamations");
            DropTable("dbo.CustomUserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.Publications");
            DropTable("dbo.Likes");
        }
    }
}
