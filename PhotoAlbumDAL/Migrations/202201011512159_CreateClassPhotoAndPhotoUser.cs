namespace PhotoAlbumDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateClassPhotoAndPhotoUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Photo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Image = c.Binary(),
                        PhotoUsersId = c.Int(nullable: false),
                        UserProfile_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfile", t => t.UserProfile_Id)
                .Index(t => t.UserProfile_Id);
            
            DropColumn("dbo.UserProfile", "Name");
            DropColumn("dbo.UserProfile", "Age");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProfile", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.UserProfile", "Name", c => c.String());
            DropForeignKey("dbo.Photo", "UserProfile_Id", "dbo.UserProfile");
            DropIndex("dbo.Photo", new[] { "UserProfile_Id" });
            DropTable("dbo.Photo");
        }
    }
}
