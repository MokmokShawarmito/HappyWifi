using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace HappyWifi.Website.Models.Controllers
{
    public class ImageController
    {
        string liteDBPath =  @"image_litedb.db";

        public ImageController()
        {
            liteDBPath = HostingEnvironment.ApplicationPhysicalPath + liteDBPath;
            liteDBPath = liteDBPath.Replace(@"\\",@"\");
        }

        public bool Add(Image image)
        {
            try
            {
                using (var db = new LiteDatabase(liteDBPath))
                {
                    // Get a collection (or create, if not exits)
                    var col = db.GetCollection<Image>("images");
                    col.Insert(image);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<Image> GetAll()
        {
            using (var db = new LiteDatabase(liteDBPath))
            {
                // Get a collection (or create, if not exits)
                var col = db.GetCollection<Image>("images");
                return col.FindAll().ToList<Image>();
            }
        }

        public Image GetById(int id)
        {
            using (var db = new LiteDatabase(liteDBPath))
            {
                var col = db.GetCollection<Image>("images");
                col.EnsureIndex(x => x.Id);
                return col.FindOne(x => x.Id == id);
            }
        }

        public bool Update(Image image)
        {
            using (var db = new LiteDatabase(liteDBPath))
            {
                var col = db.GetCollection<Image>("images");
                return col.Update(image);
            }
        }

        public bool Delete(int id)
        {
            using (var db = new LiteDatabase(liteDBPath))
            {
                // Get a collection (or create, if not exits)
                var col = db.GetCollection<Image>("images");
                var note = col.FindOne(x => x.Id == id);

                if (note == null)
                    return false;

                if (col.Delete(x => x.Id == id) <= 0)
                    return false;

                return true;
            }
        }
    }
}