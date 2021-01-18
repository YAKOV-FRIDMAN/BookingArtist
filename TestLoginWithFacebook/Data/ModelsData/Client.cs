using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingArtistMvcCore.Data.ModelsData
{
    public class Client
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string IdUser { get; set; }

        public IdentityUser IdentityUser { get; set; }
    }
}
