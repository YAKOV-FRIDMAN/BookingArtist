using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BookingArtistMvcCore.Data.ModelsData;

namespace BookingArtistMvcCore.ViewModels
{
    public class ArtistSetings
    {
        [Display(Name = "EVENT TYPE")]
        public EventType TypeEvent { get; set; }
        [Display(Name = "ARTIST TYPE")]
        public ArtistType TypeArtist { get; set; }
        public string Citys { get; set; }
        public decimal Price { get; set; }
        public DaysWork DaysWork { get; set; }
    }
}
