﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingArtistMvcCore.Data.ModelsData
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int idArtist { get; set; }
        public DateTime UploadTime { get; set; }
        public Artist Artist { get; set; }

    }
}
