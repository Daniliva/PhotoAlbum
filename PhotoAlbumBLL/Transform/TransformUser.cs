using PhotoAlbumBLL.DTO;
using PhotoAlbumDAL.Models;

namespace PhotoAlbumBLL.Transform
{
    public class TransformUserToUserCreate : ITransform<User, UserCreate>
    {
        public User Transform(UserCreate item)
        {
            return new User() { UserId = item.UserId, ApplicationUserId = item.ApplicationUserId, Email = item.Email, FirstName = item.FirstName, LastName = item.LastName, Login = item.Login };
        }

        public UserCreate Transform(User item)
        {
            return new UserCreate() { UserId = item.UserId, ApplicationUserId = item.ApplicationUserId, Email = item.Email, FirstName = item.FirstName, LastName = item.LastName, Login = item.Login };
        }
    }
}