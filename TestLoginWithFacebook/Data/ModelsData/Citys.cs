﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingArtistMvcCore.Data.ModelsData
{
    public class Citys
    {
        [Key]
        public int Id { get; set; }
        public string City { get; set; }
        public string CityEn { get; set; }


        public List<CictyWorks> CictyWorks { get; set; }
    }
}
