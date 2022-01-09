namespace PhotoAlbumDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateClassPhotoAndPhotoUser1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserProfile", newName: "PhotoUser");
            DropForeignKey("dbo.Photo", "UserProfile_Id", "dbo.UserProfile");
            DropIndex("dbo.Photo", new[] { "UserProfile_Id" });
            DropColumn("dbo.Photo", "PhotoUsersId");
            RenameColumn(table: "dbo.Photo", name: "UserProfile_Id", newName: "PhotoUsersId");
            AlterColumn("dbo.Photo", "PhotoUsersId", c => c.Int(nullable: false));
            CreateIndex("dbo.Photo", "PhotoUsersId");
            AddForeignKey("dbo.Photo", "PhotoUsersId", "dbo.PhotoUser", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photo", "PhotoUsersId", "dbo.PhotoUser");
            DropIndex("dbo.Photo", new[] { "PhotoUsersId" });
            AlterColumn("dbo.Photo", "PhotoUsersId", c => c.Int());
            RenameColumn(table: "dbo.Photo", name: "PhotoUsersId", newName: "UserProfile_Id");
            AddColumn("dbo.Photo", "PhotoUsersId", c => c.Int(nullable: false));
            CreateIndex("dbo.Photo", "UserProfile_Id");
            AddForeignKey("dbo.Photo", "UserProfile_Id", "dbo.UserProfile", "Id");
            RenameTable(name: "dbo.PhotoUser", newName: "UserProfile");
        }
    }
}
