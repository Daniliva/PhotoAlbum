using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PhotoAlbumDAL.Interfaces;
using PhotoAlbumDAL.Models;

namespace PhotoAlbumDAL.Repositories
{
    public class PhotoUserRepository : IRepository<PhotoUser>
    {
        private readonly AlbumContext _albumContext;

        public PhotoUserRepository(AlbumContext albumContext)
        {
            _albumContext = albumContext;
        }

        public async Task<IEnumerable<PhotoUser>> GetAll()
        {
            return await _albumContext.PhotoUsers.ToListAsync();
        }

        public async Task<PhotoUser> GetById(int id)
        {
            return await _albumContext.PhotoUsers.FindAsync(id);
        }

        public async Task<PhotoUser> Find(Func<PhotoUser, bool> predicate)
        {
            var du = await _albumContext.PhotoUsers.ToListAsync();
            var d = du.Where(predicate).First();
            return d;
        }

        public async Task Create(PhotoUser item)
        {
            item.Id = item.User.UserId;
            item.User = null;
            _albumContext.PhotoUsers.Add(item);
            await _albumContext.SaveChangesAsync();
        }

        public async Task<bool> Update(PhotoUser item)
        {
            var photo = _albumContext.PhotoUsers.Attach(item);
            _albumContext.Entry(item).State = EntityState.Modified;
            await _albumContext.SaveChangesAsync();
            return item != null;
        }

        public async Task<bool> Delete(int id)
        {
            var item = await _albumContext.PhotoUsers.FindAsync(id);
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