using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbumBLL.DTO
{
    public class UserUpdate
    {
        public string PasswordNew { get; set; }
        public string LoginNew { get; set; }
        public string CurrentPassword { get; set; }
    }
}