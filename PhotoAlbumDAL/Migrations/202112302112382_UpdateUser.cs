namespace PhotoAlbumDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "LastName", c => c.String());
            AddColumn("dbo.User", "Email", c => c.String());
            AddColumn("dbo.User", "Login", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "Login");
            DropColumn("dbo.User", "Email");
            DropColumn("dbo.User", "LastName");
        }
    }
}
