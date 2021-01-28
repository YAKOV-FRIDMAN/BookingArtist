using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingArtistMvcCore.Data.ModelsData
{
    public class PostCardView :Post
    {
        public string FullName { get; set; }
        public byte[] ImageProfile { get; set; }
    }
}
