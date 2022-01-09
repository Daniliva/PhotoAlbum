using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoAlbumBLL.Interfaces;
using PhotoAlbumDAL.Interfaces;
using PhotoAlbumDAL.Models;
using PhotoAlbumDAL.Repositories;

namespace PhotoAlbumBLL.Services
{
    public class Service : IService<User>
    {
        private readonly IUsersRepository _usersRepository;

        public Service(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<User> CreateAsync(User user)
        {
            return await _usersRepository.CreateUserAsync(user);
        }

        public Task<User> ChangeAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> DeleteAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _usersRepository.GetAllUsersAsync();
        }

        public async Task<User> GetById(int userId)
        {
            return await _usersRepository.GetUserByUserId(userId);
        }
    }
}