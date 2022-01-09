using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoAlbumDAL.Models;

namespace PhotoAlbumDAL.Interfaces
{
    public interface IUsersRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<User> GetUserByUserId(int userId);

        Task<User> CreateUserAsync(User user);

        Task CreateUserRolesAsync();
    }
}