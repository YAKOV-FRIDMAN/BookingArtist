using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace BookingArtistMvcCore.ViewModels.Validations
{
    public class MyModel
    {

        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please press title for post")]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime UploadTime { get; set; }


 
    }
}