﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestLoginWithFacebook.Data.ModelsData
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public int idArtist { get; set; }
        public Artist Artist { get; set; }

    }
}