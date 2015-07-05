using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HappyWifi.Website.Models
{
    public class Image
    {
        public int Id { get; set; }
        //[Required]
        public string Title { get; set; }
        //public string AltText { get; set; }
        public string Caption { get; set; }
        public bool IsHidden { get; set; }
        //[Required]
        //[DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
        public string Location { get; set; }
        public string Website { get; set; }
        public string ContactNo { get; set; }

    }
}