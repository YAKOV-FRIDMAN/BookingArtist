using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingArtistMvcCore.ViewModels
{
    public class ProfileArtist
    {
        public string Image { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public IFormFile FileImg { get; set; }

    }
}
