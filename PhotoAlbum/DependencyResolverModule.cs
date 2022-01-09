using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject.Modules;
using PhotoAlbumBLL.DTO;
using PhotoAlbumBLL.Interfaces;
using PhotoAlbumBLL.Services;
using PhotoAlbumBLL.Transform;
using PhotoAlbumDAL.Interfaces;
using PhotoAlbumDAL.Managers;
using PhotoAlbumDAL.Models;
using PhotoAlbumDAL.Repositories;

namespace PhotoAlbum
{
    public class DependencyResolverModule : NinjectModule
    {
        public override void Load()
        {
            /*   Bind<IGoodsService>().To<GoodsService>();
               Bind<IGoodsRepository>().To<GoodsRepository>();*/
            Bind<IUsersRepository>().To<UsersRepository1>();
            Bind<IService<User>>().To<Service>();
            Bind<IUserService>().To<UserService>();
            // Bind<IUserService>().To<UserService>();
            Bind<IUserManager>().To<UserManager>();
            Bind<IRepository<PhotoUser>>().To<PhotoUserRepository>();
            Bind<IRepository<User>>().To<UserRepository>();
            Bind<IRepository<Photo>>().To<PhotoRepository>();

            Bind<ITransform<User, UserCreate>>().To<TransformUserToUserCreate>();
            Bind<ITransform<Photo, PhotoToView>>().To<TransformPhotoToPhotoToView>();
            Bind<IPhotoService>().To<PhotoService>();
            Bind<ITransform<Photo, PhotoCreate>>().To<TransformPhotoToPhotoCreate>();

            /*Bind<UserStore<IdentityUser>>().To<UserStore<IdentityUser>>();//RoleStore : RoleStore<IdentityRole>
            Bind<RoleStore<IdentityRole>>().To<RoleStore<IdentityRole>>();
            Bind<UserManager<IdentityUser>>().To<UserManager<IdentityUser>>();
            Bind<RoleManager<IdentityRole>>().To<RoleManager<IdentityRole>>();//UserStore<IdentityUser>*/
        }
    }
}