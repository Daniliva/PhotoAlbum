using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoAlbumDAL.Interfaces;
using PhotoAlbumDAL.Models;
using PhotoAlbumDAL.Repositories;

namespace PhotoAlbumBLL.Services
{
    public class PhotoUserService
    {
        private readonly IRepository<PhotoUser> _photoUsersRepository;

        public PhotoUserService(IRepository<PhotoUser> photoUsersRepository)
        {
            _photoUsersRepository = photoUsersRepository;
        }

        public Task<IEnumerable<PhotoUser>> GetAllAsync()
        {
            return _photoUsersRepository.GetAll();
        }

        public Task<PhotoUser> GetById(int userId)
        {
            return _photoUsersRepository.GetById(userId);
        }

        public async Task<PhotoUser> GetByUser(User user)
        {
            return await _photoUsersRepository.Find(x => x.User.Login == user.Login);
        }
    }
}