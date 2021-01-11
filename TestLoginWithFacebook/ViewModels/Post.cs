using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestLoginWithFacebook.ViewModels
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string ImageProfile { get; set; }
        public int Likes { get; set; }
        public int Views { get; set; }
    }
}
