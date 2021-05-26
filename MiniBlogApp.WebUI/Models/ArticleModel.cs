using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MiniBlogApp.WebUI.Models
{
    public class ArticleModel
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter Description")]
        public string Description { get; set; }
        //  [Required(ErrorMessage = "")]
        public string ImageUrl { get; set; }

        //[Required(ErrorMessage = "Please enter title")]
        public string Url { get; set; }
    }
}
