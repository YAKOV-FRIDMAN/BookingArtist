using BookingArtistMvcCore.ViewModels.Validations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingArtistMvcCore.ViewModels
{
    public class PostNew
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please press title for post")]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime UploadTime { get; set; }


        [Required(AllowEmptyStrings = false,ErrorMessage = "Please select a file." )]
        //[DataType(DataType.Upload)]
        //[AllowedExtensions(new string[] { ".jpg", ".png" })]
        public virtual IFormFile ImageFile { get; set; }

    }
}
