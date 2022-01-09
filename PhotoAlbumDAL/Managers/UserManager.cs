using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PhotoAlbumDAL.Interfaces;
using PhotoAlbumDAL.Models;
using PhotoAlbumDAL.Repositories;

namespace PhotoAlbumDAL.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IRepository<User> _usersRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserManager(IRepository<User> usersRepository/*, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager*/)
        {
            _usersRepository = usersRepository;
            var userStore = new UserStore<IdentityUser>();
            var roleStore = new RoleStore<IdentityRole>();
            _userManager = new UserManager<IdentityUser>(userStore);
            _roleManager = new RoleManager<IdentityRole>(roleStore);
        }

        public async Task InitializeAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@gmail.com";
            string password = "_Aa123456";
            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("User") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                IdentityUser admin = new IdentityUser() { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin.Id, "admin");
                }
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _usersRepository.GetAll();
        }

        public async Task<User> GetById(int id)
        {
            return await _usersRepository.GetById(id);
        }

        public async Task<User> FindUser(Func<User, bool> predicate)
        {
            return await _usersRepository.Find(predicate);
        }

        public async Task Create(User item, string password)
        {
            try
            {
                var dw = new IdentityUserLogin() { LoginProvider = item.Login };
                IdentityResult result =
               _userManager.Create(new IdentityUser()
               {
                   UserName = item.Email,
                   Email = item.Email
               }, password);
                if (result.Succeeded)
                {
                    var dr = await _userManager.Users.ToListAsync();
                    item.ApplicationUserId = dr.FirstOrDefault(x => x.Email == item.Email)?.Id;
                    //check if User not create
                    result = _userManager.AddToRole(item.ApplicationUserId, "User");
                    await _usersRepository.Create(item);
                }
            }
            catch (Exception exception)
            {
                var t = exception;
            }
        }

        public async Task<bool> UpdateUser(User item)
        {
            var itemOld = await _usersRepository.Find(x => x.UserId == item.UserId);
            if (itemOld == null)
            {
                return false;
            }
            /*var identityUser = _userManager.Users.FirstOrDefault(x => x.Email == itemOld.Email);
            if (identityUser == null)
            {
                return false;
            }

            identityUser.UserName = item.Email;
            identityUser.Email = item.Email;
            _userManager.AddLogin(item.ApplicationUserId, new UserLoginInfo(item.Login, ""));
            _userManager.Update(identityUser);*/
            itemOld.Email = item.Email;
            itemOld.FirstName = item.FirstName;
            itemOld.LastName = item.LastName;

            await _usersRepository.Update(itemOld);
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var itemOld = await _usersRepository.Find(x => x.UserId == id);
            if (itemOld == null)
            {
                return false;
            }
            var identityUser = _userManager.Users.FirstOrDefault(x => x.Email == itemOld.Email);
            if (identityUser == null)
            {
                return false;
            }

            var loginList = await _userManager.GetLoginsAsync(identityUser.Id);
            foreach (var login in loginList)
            {
                _userManager.RemoveLogin(identityUser.Id, login);
            }

            await _userManager.DeleteAsync(identityUser);
            await _usersRepository.Delete(id);

            return true;
        }

        public async Task<bool> Update(User item, string passwordNew, string loginNew, string currentPassword)
        {
            var itemOld = await _usersRepository.Find(x => x.UserId == item.UserId);
            if (itemOld == null)
            {
                return false;
            }
            var identityUser = await _userManager.FindAsync(item.Email, currentPassword);
            if (identityUser == null)
            {
                return false;
            }

            var loginList = await _userManager.GetLoginsAsync(identityUser.Id);
            foreach (var login in loginList)
            {
                _userManager.RemoveLogin(identityUser.Id, login);
            }

            item.Login = loginNew;
            await _userManager.AddLoginAsync(item.ApplicationUserId, new UserLoginInfo(item.Login, ""));

            await _userManager.ChangePasswordAsync(item.ApplicationUserId, currentPassword, passwordNew);
            await _usersRepository.Update(item);

            return true;
        }

        public async Task<IdentityUser> Find(LoginInfo loginInfo)
        { return await _userManager.FindAsync(loginInfo.Username, loginInfo.Password); }

        public async Task<IList<string>> GetRoles(string id)
        { return await _userManager.GetRolesAsync(id); }

        public async Task<ClaimsIdentity> LoginIn(LoginInfo loginInfo, string applicationCookie)
        {
            var user = await _userManager.FindAsync(loginInfo.Username, loginInfo.Password);
            var roles = await _userManager.GetRolesAsync(user.Id);
            ClaimsIdentity userIdentity = _userManager.CreateIdentity(user,
                applicationCookie);
            return userIdentity;
        }
    }
}