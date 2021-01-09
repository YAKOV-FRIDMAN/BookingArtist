using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestLoginWithFacebook.Data.ModelsData
{
    public class Orders
    {
        public int Id { get; set; }
        public string NameClient { get; set; }
        public string PhoneNumberClient { get; set; }
        public int IdCity { get; set; }
        public bool IfPaid { get; set; }
        public bool IfApprovedOrder { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DateEvent { get; set; }
        public decimal Price { get; set; }

        public int IdAtris { get; set; }
        [ForeignKey("FkIdArtis")]
        public Artist Artist { get; set; }


    }
}
