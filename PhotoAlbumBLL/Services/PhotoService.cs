using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhotoAlbumBLL.DTO;
using PhotoAlbumBLL.Interfaces;
using PhotoAlbumBLL.Transform;
using PhotoAlbumDAL.Interfaces;
using PhotoAlbumDAL.Models;
using PhotoAlbumDAL.Repositories;

namespace PhotoAlbumBLL.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IRepository<PhotoUser> _photoUsersRepository;
        private readonly IRepository<Photo> _photoRepository;
        private readonly ITransform<Photo, PhotoToView> _photoTransform;
        private readonly ITransform<Photo, PhotoCreate> _photoToCreateTransform;

        public PhotoService(IRepository<PhotoUser> photoUsersRepository, IRepository<Photo> photoRepository, ITransform<Photo, PhotoToView> photoTransform, ITransform<Photo, PhotoCreate> photoToCreateTransform)
        {
            _photoUsersRepository = photoUsersRepository;
            _photoRepository = photoRepository;
            _photoTransform = photoTransform;
            _photoToCreateTransform = photoToCreateTransform;
        }

        public async Task<IEnumerable<PhotoToView>> GetAllAsync()
        {
            var collection = await _photoRepository.GetAll();
            return collection.Select(x => _photoTransform.Transform(x));
        }

        public async Task<IEnumerable<PhotoToView>> GetAllByUserLoginAsync(string email)
        {
            var photoUser = await _photoUsersRepository.Find(x => x.User.Email == email);

            return photoUser.Photo.Select(x => _photoTransform.Transform(x));
        }

        public async Task<PhotoToView> GetById(int id)
        {
            return _photoTransform.Transform(await _photoRepository.GetById(id));
        }

        public async Task<Photo> CreateAsync(PhotoCreate photoCreate, string email)
        {
            Photo item = _photoToCreateTransform.Transform(photoCreate);
            var photoUser = await _photoUsersRepository.Find(x => x.User.Email == email);
            item.PhotoUserId = photoUser.Id;
            item.PhotoUsers = photoUser;
            await _photoRepository.Create(item);

            return item;
        }

        public async Task DeleteAsync(int id, string login)
        {
            var photoUser = await _photoUsersRepository.Find(x => x.User.Email == login);
            if (photoUser.Photo.Find(x => x.Id == id) != null)
                await _photoRepository.Delete(id);
        }
    }
}