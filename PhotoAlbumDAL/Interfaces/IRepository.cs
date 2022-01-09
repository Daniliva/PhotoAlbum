using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoAlbumDAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(int rId);

        Task<T> Find(Func<T, bool> predicate);

        Task Create(T item);

        Task<bool> Update(T item);

        Task<bool> Delete(int id);
    }
}