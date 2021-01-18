using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingArtistMvcCore.ViewModels
{
    // ARTIST 
    public class SearchArtist
    {
        [Display(Name = "LOCATION OF THE EVENT")]
        [Required]
        public string City { get; set; }

        [Display(Name = "WHEN IS THE EVENT")]
        [Required]
        public DateTime TimeEvent { get; set; }
        [Display(Name = "EVENT TYPE")]
        [Required]
        public EventType TypeEvent { get; set; }
        [Display(Name = "ARTIST TYPE")]
        [Required]
        public ArtistType TypeArtist { get; set; }


    }

    public enum ArtistType
    {
        Singer,
        Keyboard,
        DJ
    }
    public enum EventType
    {
        [Display(Name = "Ab Big Event")]
        AbBigEvent,
        [Display(Name = "Medium Event")]
        MediumEvent,
        [Display(Name = "ASmall Event")]
        ASmallEvent

    }
}
