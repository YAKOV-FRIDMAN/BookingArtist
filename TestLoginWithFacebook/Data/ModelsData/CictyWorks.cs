using Microsoft.AspNetCore.Identity;
 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingArtistMvcCore.Data.ModelsData
{
    public class CictyWorks
    {
        public int IdArtis { get; set; }
        public  Artist Artists { get; set; }
        public int IdCity { get; set; }
        public Citys Citys { get; set; }


        
    }
}
