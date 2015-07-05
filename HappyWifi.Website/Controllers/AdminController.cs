using HappyWifi.Website.Models;
using HappyWifi.Website.Models.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HappyWifi.Website.Controllers
{
    [RoutePrefix("admin")]
    public class AdminController : Controller
    {
        //admin panel
        [HttpGet]
        [Route("")]
        public ActionResult Index()
        {
            var images = new ImageController().GetAll();
            return View(images);
        }

        [HttpGet]
        [Route("login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public ActionResult Login(string username, string password)
        {
            return View();
        }
    }
}