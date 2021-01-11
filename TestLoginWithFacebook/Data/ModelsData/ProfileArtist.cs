using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestLoginWithFacebook.Data.ModelsData
{
    public class ProfileArtist
    {
        
        public int Id { get; set; }
        public byte[] ImageProfile { get; set; }
        public string About { get; set; }
        public string FullName { get; set; }
        public int IdArtit { get; set; }

        [ForeignKey("FkIdArtis")]
        public virtual Artist Artist { get; set; }

    }
}
