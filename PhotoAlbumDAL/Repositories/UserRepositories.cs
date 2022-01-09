using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PhotoAlbumDAL.Interfaces;
using PhotoAlbumDAL.Models;

namespace PhotoAlbumDAL.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private AlbumContext _albumContext;

        public UserRepository(AlbumContext albumContext)
        {
            _albumContext = albumContext;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _albumContext.Users.ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _albumContext.Users.FindAsync(id);
        }

        public async Task<User> Find(Func<User, bool> predicate)
        {
            var du = await _albumContext.Users.ToListAsync();
            var d = du.Where(predicate).First();
            //    await _albumContext.Users.LoadAsync();
            return d;
        }

        public async Task Create(User item)
        {
            _albumContext.Users.Add(item);
            await _albumContext.SaveChangesAsync();
        }

        public async Task<bool> Update(User item)
        {
            _albumContext = new AlbumContext();
            var user = _albumContext.Users.Attach(item);
            _albumContext.Entry(item).State = EntityState.Modified;
            _albumContext.SaveChanges();
            return item != null;
        }

        public async Task<bool> Delete(int id)
        {
            var item = await _albumContext.Users.FindAsync(id);
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