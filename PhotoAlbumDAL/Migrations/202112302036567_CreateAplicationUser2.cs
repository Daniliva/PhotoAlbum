namespace PhotoAlbumDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAplicationUser2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.IdentityUser", name: "UserIdId", newName: "User_UserId");
            RenameIndex(table: "dbo.IdentityUser", name: "IX_UserIdId", newName: "IX_User_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.IdentityUser", name: "IX_User_UserId", newName: "IX_UserIdId");
            RenameColumn(table: "dbo.IdentityUser", name: "User_UserId", newName: "UserIdId");
        }
    }
}
