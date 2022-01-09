using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoAlbumDAL.Models
{
    public class PhotoUser
    {
        [Key]
        [ForeignKey("User")]
        public int Id { get; set; }

        public virtual List<Photo> Photo { get; set; }

        public virtual User User { get; set; }
    }
}