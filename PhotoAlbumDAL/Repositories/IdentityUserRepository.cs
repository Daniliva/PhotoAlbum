using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PhotoAlbumDAL.Repositories
{
    public class IdentityUserRepository : UserManager<IdentityUser>
    {
        public IdentityUserRepository(IUserStore<IdentityUser> store) : base(new UserStore<IdentityUser>())
        {
        }
    }
}