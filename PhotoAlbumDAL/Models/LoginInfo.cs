using System.ComponentModel.DataAnnotations;

namespace PhotoAlbumDAL.Models
{
    public class LoginInfo
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}