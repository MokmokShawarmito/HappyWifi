using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HappyWifi.Website.Models
{
    public class User
    {
        public int Id { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String Email{ get; set; }
    }
}