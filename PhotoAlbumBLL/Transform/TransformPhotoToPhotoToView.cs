using PhotoAlbumBLL.DTO;
using PhotoAlbumDAL.Models;

namespace PhotoAlbumBLL.Transform
{
    public class TransformPhotoToPhotoToView : ITransform<Photo, PhotoToView>
    {
        public Photo Transform(PhotoToView item)
        {
            return new Photo() { Id = item.Id, Description = item.Description, Name = item.Name, Image = item.Image };
        }

        public PhotoToView Transform(Photo item)
        {
            return new PhotoToView() { Id = item.Id, Description = item.Description, Name = item.Name, Image = item.Image, UserLogin = item?.PhotoUsers?.User?.Login };
        }
    }
}