﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbumBLL.DTO
{
    public class PhotoToView
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public string UserLogin { get; set; }
    }
}