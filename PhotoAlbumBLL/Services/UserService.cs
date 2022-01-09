using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using PhotoAlbumBLL.DTO;
using PhotoAlbumBLL.Interfaces;
using PhotoAlbumBLL.Transform;
using PhotoAlbumDAL.Interfaces;
using PhotoAlbumDAL.Managers;
using PhotoAlbumDAL.Models;
using PhotoAlbumDAL.Repositories;

namespace PhotoAlbumBLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserManager _userManager;
        private readonly IRepository<PhotoUser> _photoUsersRepository;
        private readonly ITransform<User, UserCreate> _userTransform;

        public UserService(IUserManager userManager, IRepository<PhotoUser> photoUsersRepository, ITransform<User, UserCreate> userTransform)
        {
            _userManager = userManager;
            _photoUsersRepository = photoUsersRepository;
            this._userTransform = userTransform;
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            return _userManager.GetAll();
        }

        public Task<User> GetById(int userId)
        {
            return _userManager.GetById(userId);
        }

        public async Task<User> CreateAsync(UserCreate user)
        {
            var newUser = _userTransform.Transform(user);
            var newG = await _photoUsersRepository.GetAll();
            await _userManager.Create(newUser, user.Password);
            newUser = await _userManager.FindUser(x => x.Login == user.Login);
            await _photoUsersRepository.Create(new PhotoUser() { User = newUser });
            return newUser;
        }

        public async Task<User> ChangeAsync(User user)
        {
            var oldUser = await _userManager.FindUser(x => x.UserId == user.UserId);
            user.UserId = oldUser.UserId;
            if (await _userManager.UpdateUser(user))
            {
                return await _userManager.GetById(user.UserId);
            }
            return oldUser;
        }

        public async Task<User> ChangeLoginAndPasswordAsync(User user, UserUpdate userUpdate)
        {
            var oldUser = await _userManager.FindUser(x => x.UserId == user.UserId);
            user.UserId = oldUser.UserId;
            if (await _userManager.Update(user, userUpdate.PasswordNew, userUpdate.LoginNew, userUpdate.CurrentPassword))
            {
                return await _userManager.GetById(user.UserId);
            }
            return oldUser;
        }

        public async Task DeleteAsync(User user)
        {
            user = await _userManager.FindUser(x => x.Login == user.Login);
            await _userManager.Delete(user.UserId);
            var photoUser = await _photoUsersRepository.Find(x => x.User.UserId == user.UserId);
            await _photoUsersRepository.Delete(photoUser.Id);
        }

        public async Task<ClaimsIdentity> Login(LoginInfo user, string applicationCookie)
        {
            return await _userManager.LoginIn(user, applicationCookie);
        }
    }
}