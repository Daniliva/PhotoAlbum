using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbumDAL.Models
{
    public class Photo
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }

        public int? PhotoUserId { get; set; }

        public virtual PhotoUser PhotoUsers
        { get; set; }
    }
}