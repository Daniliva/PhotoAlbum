using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoAlbumBLL.Interfaces
{
    public interface IService<T>
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetById(int userId);

        Task<T> CreateAsync(T item);

        Task<T> ChangeAsync(T item);

        Task<T> DeleteAsync(T item);
    }
}