using System.IO;
using System.Web;
using PhotoAlbumBLL.DTO;
using PhotoAlbumDAL.Models;

namespace PhotoAlbumBLL.Transform
{
    public class TransformPhotoToPhotoCreate : ITransform<Photo, PhotoCreate>
    {
        public Photo Transform(PhotoCreate item)
        {
            byte[] imageData = null;
            // считываем переданный файл в массив байтов
            using (var binaryReader = new BinaryReader(item.UploadImage.InputStream))
            {
                imageData = binaryReader.ReadBytes(item.UploadImage.ContentLength);
                return new Photo() { Description = item.Description, Id = item.Id, Name = item.Name, Image = imageData };
            }
        }

        public PhotoCreate Transform(Photo item)
        {
            HttpPostedFileBase objFile = (HttpPostedFileBase)new MemoryPostedFile(item.Image, item.Name);
            return new PhotoCreate() { Description = item.Description, Id = item.Id, Name = item.Name, UploadImage = objFile };
        }
    }
}