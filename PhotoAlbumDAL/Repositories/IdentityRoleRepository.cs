using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PhotoAlbumDAL.Repositories
{
    public class IdentityRoleRepository : RoleManager<IdentityRole>
    {
        public IdentityRoleRepository(IRoleStore<IdentityRole, string> store) : base(store)
        {
        }
    }
}