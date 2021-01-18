using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingArtistMvcCore.ViewModels
{
    public class OrderForArtist : Order
    {
        [Display(Name = "If Paid")]
        public bool IfPaid { get; set; }
        [Display(Name = "If Approved Order")]
        public bool IfApprovedOrder { get; set; }
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }
        public string Email { get; set; }
    }
}
