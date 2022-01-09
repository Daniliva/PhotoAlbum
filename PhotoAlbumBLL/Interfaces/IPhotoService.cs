using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoAlbumBLL.DTO;
using PhotoAlbumDAL.Models;

namespace PhotoAlbumBLL.Interfaces
{
    public interface IPhotoService
    {
        Task<IEnumerable<PhotoToView>> GetAllAsync();

        Task<IEnumerable<PhotoToView>> GetAllByUserLoginAsync(string login);

        Task<PhotoToView> GetById(int id);

        Task<Photo> CreateAsync(PhotoCreate item, string email);

        Task DeleteAsync(int id, string login);
    }
}