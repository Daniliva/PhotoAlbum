namespace PhotoAlbumDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateClassPhotoAndPhotoUsers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProfile", "Id", "dbo.User");
            DropIndex("dbo.UserProfile", new[] { "Id" });
            DropTable("dbo.UserProfile");
        }
    }
}
