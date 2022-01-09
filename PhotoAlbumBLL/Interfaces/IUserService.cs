using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using PhotoAlbumBLL.DTO;
using PhotoAlbumDAL.Models;

namespace PhotoAlbumBLL.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();

        Task<User> GetById(int userId);

        Task<User> CreateAsync(UserCreate user);

        Task<User> ChangeAsync(User user);

        Task DeleteAsync(User user);

        Task<ClaimsIdentity> Login(LoginInfo user, string applicationCookie);

        Task<User> ChangeLoginAndPasswordAsync(User user, UserUpdate userUpdate);
    }
}