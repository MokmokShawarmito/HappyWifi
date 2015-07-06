using HappyWifi.Website.Models.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HappyWifi.Website.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var images = new ImageController().GetAll().Where(x => x.IsHidden == false).ToList();
            return View(images);
        }
    }
}