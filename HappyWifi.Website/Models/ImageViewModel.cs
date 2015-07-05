using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HappyWifi.Website.Models
{
    public class ImageViewModel
    {
        [Required]
        public string Title { get; set; }
        //public string AltText { get; set; }
        public string Caption { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFile Image { get; set; }
    }
}