using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PhotoAlbumDAL.Interfaces;

//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
using PhotoAlbumDAL.Models;

namespace PhotoAlbumDAL.Repositories
{
    public class UsersRepository1 : IUsersRepository
    {
        private readonly AlbumContext _albumContext;

        public UsersRepository1(AlbumContext albumContext)
        {
            _albumContext = albumContext;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);
            var dw = new IdentityUserLogin() { LoginProvider = user.Login };
            IdentityResult result = await manager.CreateAsync(new IdentityUser()
            {
                UserName = user.Login,
                Email = user.Email
            }, "TestPassword");
            if (result.Succeeded)
            {
                var dr = await manager.Users.ToListAsync();
                user.ApplicationUserId = dr.FirstOrDefault(x => x.Email == user.Email)?.Id;
                result = await manager.AddToRoleAsync(user.ApplicationUserId, "Admin");
                //   result = await manager.AddToRoleAsync(user.ApplicationUserId, "1");
                _albumContext.Users.Add(user);
                await manager.AddLoginAsync(user.ApplicationUserId, new UserLoginInfo(user.Login, ""));
                await _albumContext.SaveChangesAsync();
            }

            return user;
        }

        public async Task CreateUserRolesAsync()
        {
            var roleStore = new RoleStore<IdentityRole>();
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var identityResult = await roleManager.CreateAsync(new IdentityRole
            {
                Name = "Admin"
            });
            var identityResult2 = await roleManager.CreateAsync(new IdentityRole
            {
                Name = "User"
            });

            var userStore = new UserStore<IdentityUser>();
            var userManager = new UserManager<IdentityUser>(userStore);
            var user = await userManager.FindAsync("TestUserName", "TestPassword");

            await userManager.AddToRoleAsync(user.Id, "Admin");
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _albumContext.Users.ToListAsync();
        }

        public async Task<User> GetUserByUserId(int userId)
        {
            return await _albumContext.Users.FindAsync(userId);
        }
    }
}