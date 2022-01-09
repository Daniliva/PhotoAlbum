namespace PhotoAlbumDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateClassPhotoAndPhotoUsersFinal : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Photo", "PhotoUsersId", "dbo.PhotoUser");
            DropIndex("dbo.Photo", new[] { "PhotoUsersId" });
            RenameColumn(table: "dbo.Photo", name: "PhotoUsersId", newName: "PhotoUserId");
            AlterColumn("dbo.Photo", "PhotoUserId", c => c.Int());
            CreateIndex("dbo.Photo", "PhotoUserId");
            AddForeignKey("dbo.Photo", "PhotoUserId", "dbo.PhotoUser", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photo", "PhotoUserId", "dbo.PhotoUser");
            DropIndex("dbo.Photo", new[] { "PhotoUserId" });
            AlterColumn("dbo.Photo", "PhotoUserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Photo", name: "PhotoUserId", newName: "PhotoUsersId");
            CreateIndex("dbo.Photo", "PhotoUsersId");
            AddForeignKey("dbo.Photo", "PhotoUsersId", "dbo.PhotoUser", "Id", cascadeDelete: true);
        }
    }
}
