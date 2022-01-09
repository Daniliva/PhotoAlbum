using System.Web;

namespace PhotoAlbumBLL.DTO
{
    public class PhotoCreate
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public HttpPostedFileBase UploadImage { get; set; }
    }
}