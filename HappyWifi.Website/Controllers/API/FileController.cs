using HappyWifi.Website.Models;
using HappyWifi.Website.Models.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace HappyWifi.Website.Controllers.API
{
    [RoutePrefix("api/v1/file")]
    public class FileController : ApiController
    {

        ImageController imageController = new ImageController();//new ImageController(Path.Combine(HttpContext.Current.Server.MapPath(@"~/Images/Uploads/"), "image_litedb.db"));

        [HttpGet]
        [Route("")]
        public IEnumerable<Image> Get()
        {
            return this.imageController.GetAll();
        }

        [HttpGet]
        [Route("id/{id:int}")]
        public Image Get(int id)
        {
            return this.imageController.GetById(id);
        }

        [HttpPost]
        [Route("upload")]
        public string Upload()
        {
            string filePath = string.Empty;
            string serverPath = "~/Images/Uploads/";

            if (HttpContext.Current.Request.Files["image"] != null && HttpContext.Current.Request["Caption"] != null && HttpContext.Current.Request["Title"] != null)
            {
                string title = HttpContext.Current.Request["Title"] as string;
                string caption = HttpContext.Current.Request["Caption"] as string;
                HttpPostedFile file = HttpContext.Current.Request.Files["image"];

                //validate file
                if(ValidateFile(file))
                {
                    //upload file
                    string fileName = string.Format("{0}", file.FileName);

                    var test = HttpContext.Current.Server.MapPath(serverPath);
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(serverPath)))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(serverPath));
                    }

                    // Get the complete file path then save.
                    string fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath(serverPath), fileName);
                    file.SaveAs(fileSavePath);
                    filePath = String.Format("{0}{1}", serverPath, fileName);

                    if(filePath.Contains("~"))
                    {
                        filePath = filePath.Replace("~","");
                    }

                    //save path to db
                    Image image = new Image();
                    image.Caption = caption;
                    image.Title = title;
                    image.ImageUrl = filePath;
                    image.IsHidden = false;
                    this.imageController.Add(image);
                }
                else
                {
                    this.ThrowHttpException("Bad image file.", HttpStatusCode.BadRequest);
                }
            }
            else
            {
                this.ThrowHttpException("No image file on request.", HttpStatusCode.BadRequest);
            }

            return filePath;
        }

        [HttpPost]
        [Route("update/{id:int}")]
        public bool Update(int id)
        {
            try
            {
                string filePath = string.Empty;
                string serverPath = "~/Images/Uploads/";
                Image image = this.imageController.GetById(id);

                if(image == null)
                {
                    return false;
                }

                if (HttpContext.Current.Request["Caption"] != null)
                {
                    image.Caption = HttpContext.Current.Request["Caption"] as string;
                }

                if (HttpContext.Current.Request["Title"] != null)
                {
                    image.Title = HttpContext.Current.Request["Title"] as string;
                }

                if (HttpContext.Current.Request["IsHidden"] != null)
                {
                    bool isHidden = false;

                    if (HttpContext.Current.Request["IsHidden"].ToString().ToLower() == "true")
                        isHidden = true;
                    image.IsHidden = isHidden;
                }

                if (HttpContext.Current.Request.Files["image"] != null)
                {
                    HttpPostedFile file = HttpContext.Current.Request.Files["image"];
                    //validate file
                    if (ValidateFile(file))
                    {
                        //upload file
                        string fileName = string.Format("{0}", file.FileName);

                        var test = HttpContext.Current.Server.MapPath(serverPath);
                        if (!Directory.Exists(HttpContext.Current.Server.MapPath(serverPath)))
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(serverPath));
                        }

                        // Get the complete file path then save.
                        string fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath(serverPath), fileName);
                        file.SaveAs(fileSavePath);
                        filePath = String.Format("{0}{1}", serverPath, fileName);

                        if (filePath.Contains("~"))
                        {
                            filePath = filePath.Replace("~", "");
                        }

                        image.ImageUrl = filePath;
                    }
                }

                //save to db
                this.imageController.Update(image);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        [HttpPost]
        [Route("delete/{id:int}")]
        public void Delete(int id)
        {
            this.imageController.Delete(id);
        }

        private void ThrowHttpException(string message = "Server Error", HttpStatusCode code = HttpStatusCode.InternalServerError)
        {
            var resp = new HttpResponseMessage(code)
            {
                Content = new StringContent(string.Format(message)),
                ReasonPhrase = message
            };
            throw new HttpResponseException(resp);
        }

        private bool ValidateFile(HttpPostedFile file)
        {
            int size = file.ContentLength;

            if (size > 20000000)
                return false;

            if (!(file.FileName.ToLower().Contains(".jpg") || file.FileName.ToLower().Contains(".png") || file.FileName.ToLower().Contains(".jpeg")))
                return false;

            return true;
        }
    }


}