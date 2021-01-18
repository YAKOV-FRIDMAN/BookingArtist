using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingArtistMvcCore.ViewModels
{
    public class Order
    {
        [Display(Name = "Name Artist")]
        public string NameArtist { get; set; }
        public string City { get; set; }
        [Display(Name = "Date Time Event")]
        public DateTime DateTimeEvent { get; set; }
        [Display(Name = "Artist Type")]
        public ArtistType ArtistType { get; set; }
        [Display(Name = "Event Type")]
        public EventType EventType { get; set; }
        public decimal Price { get; set; }
        public int IdArtist { get; set; }

        [Required]
        [Display(Name = "Your name")]
        public string NameClient { get; set; }
        [Required]
        [Display(Name = "Your phone number")]
        public string PhoneClient { get; set; }
        [Required]
        [Display(Name = "Exact address of the event")]
        public string Address { get; set; }


    }
}
