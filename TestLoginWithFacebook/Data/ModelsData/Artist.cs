using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static BookingArtistMvcCore.Data.ModelsData.Enums;

namespace BookingArtistMvcCore.Data.ModelsData
{
    public class Artist
    {
        [Key]
        public int Id { get; set; }
        public ArtistType ArtistType { get; set; }
        public EventType EventType { get; set; }
        public List<CictyWorks> CictyWorks { get; set; }
        public ProfileArtist ProfileArtist { get; set; }
        public DaysWork DaysWork { get; set; }
        public decimal Price { get; set; }
        [ForeignKey("FkIdUser")]
        public string IdUser { get; set; }

        public IdentityUser IdentityUser { get; set; }
        public ICollection<Orders> Orders { get; set; }
        public ICollection<Post> Posts { get; set; }

       

    }
}
