namespace PhotoAlbumDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAplicationUser1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "ApplicationUserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "ApplicationUserId", c => c.Int(nullable: false));
        }
    }
}
