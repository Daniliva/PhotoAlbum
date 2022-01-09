using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoAlbumDAL.Models;

namespace PhotoAlbumBLL.DTO
{
    public class UserCreate : User
    {
        public string Password { get; set; }
    }
}