﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingArtistMvcCore.ViewModels
{
    public class Post : PostNew
    {
    
    
        public string ImageProfile { get; set; }
        public int Likes { get; set; }
        public int Views { get; set; }


     }
}
