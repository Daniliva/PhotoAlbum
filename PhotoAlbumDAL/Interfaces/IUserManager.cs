using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PhotoAlbumDAL.Models;

namespace PhotoAlbumDAL.Interfaces
{
    public interface IUserManager
    {
        Task<IEnumerable<User>> GetAll();

        Task<User> GetById(int id);

        Task<User> FindUser(Func<User, bool> predicate);

        Task Create(User item, string password);

        Task<bool> UpdateUser(User item);

        Task<bool> Delete(int id);

        Task<bool> Update(User item, string passwordNew, string loginNew, string currentPassword);

        Task<IdentityUser> Find(LoginInfo loginInfo);

        Task<IList<string>> GetRoles(string id);

        Task InitializeAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager);

        Task<ClaimsIdentity> LoginIn(LoginInfo loginInfo, string applicationCookie);
    }
}