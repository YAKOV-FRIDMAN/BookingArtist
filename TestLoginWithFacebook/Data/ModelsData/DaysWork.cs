using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingArtistMvcCore.Data.ModelsData
{
    public class DaysWork
    {
        public int Id { get; set; }
        public bool Sunday { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }

        public int IdArtit { get; set; }

        [ForeignKey("FkIdArtis")]
        public virtual Artist Artist { get; set; }
      //  public ICollection<Artist> Artists { get; set; }
    }
}
