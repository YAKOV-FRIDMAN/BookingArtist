using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingArtistMvcCore.Data.ModelsData
{
    public class ArtistCard
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public decimal Price { get; set; }
    }
}
