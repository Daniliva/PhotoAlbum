namespace PhotoAlbumDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAplicationUser5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.IdentityUserClaim", "IdentityUser_Id", "dbo.IdentityUser");
            DropForeignKey("dbo.IdentityUserLogin", "IdentityUser_Id", "dbo.IdentityUser");
            DropForeignKey("dbo.IdentityUserRole", "IdentityUser_Id", "dbo.IdentityUser");
            DropForeignKey("dbo.IdentityUser", "User_UserId", "dbo.User");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropIndex("dbo.IdentityUser", new[] { "User_UserId" });
            DropIndex("dbo.IdentityUserClaim", new[] { "IdentityUser_Id" });
            DropIndex("dbo.IdentityUserLogin", new[] { "IdentityUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropTable("dbo.IdentityUser");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId });
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
                        User_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.IdentityUserRole", "IdentityRole_Id");
            CreateIndex("dbo.IdentityUserRole", "IdentityUser_Id");
            CreateIndex("dbo.IdentityUserLogin", "IdentityUser_Id");
            CreateIndex("dbo.IdentityUserClaim", "IdentityUser_Id");
            CreateIndex("dbo.IdentityUser", "User_UserId");
            AddForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole", "Id");
            AddForeignKey("dbo.IdentityUser", "User_UserId", "dbo.User", "UserId");
            AddForeignKey("dbo.IdentityUserRole", "IdentityUser_Id", "dbo.IdentityUser", "Id");
            AddForeignKey("dbo.IdentityUserLogin", "IdentityUser_Id", "dbo.IdentityUser", "Id");
            AddForeignKey("dbo.IdentityUserClaim", "IdentityUser_Id", "dbo.IdentityUser", "Id");
        }
    }
}
