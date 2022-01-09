using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using PhotoAlbumDAL.Interfaces;
using PhotoAlbumDAL.Models;

namespace PhotoAlbumDAL.Repositories
{
    public class PhotoRepository : IRepository<Photo>
    {
        private readonly AlbumContext _albumContext;

        public PhotoRepository(AlbumContext albumContext)
        {
            _albumContext = albumContext;
        }

        public async Task<IEnumerable<Photo>> GetAll()
        {
            return await _albumContext.Photos.ToListAsync();
        }

        public async Task<Photo> GetById(int id)
        {
            return await _albumContext.Photos.FindAsync(id);
        }

        public async Task<Photo> Find(Func<Photo, bool> predicate)
        {
            return await _albumContext.Photos.FindAsync(predicate);
        }

        public async Task Create(Photo item)
        {
            _albumContext.Photos.Add(item);
            await _albumContext.SaveChangesAsync();
        }

        public async Task<bool> Update(Photo item)
        {
            var photo = _albumContext.Photos.Attach(item);
            _albumContext.Entry(item).State = EntityState.Modified;
            await _albumContext.SaveChangesAsync();
            return item != null;
        }

        public async Task<bool> Delete(int id)
        {
            var item = await _albumContext.Photos.FindAsync(id);
            var result = item != null;
            if (result)
            {
                _albumContext.Entry(item).State = EntityState.Deleted;
                await _albumContext.SaveChangesAsync();
            }

            return result;
        }
    }
}