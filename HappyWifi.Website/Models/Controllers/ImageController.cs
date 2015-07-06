using LiteDB;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace HappyWifi.Website.Models.Controllers
{
    public class ImageController
    {
        string liteDBPath =  @"image_litedb.db";
        string sqlConnectionString = "";

        public ImageController()
        {
            //liteDBPath = HostingEnvironment.ApplicationPhysicalPath + liteDBPath;
            //liteDBPath = liteDBPath.Replace(@"\\",@"\");
            this.sqlConnectionString = ConfigurationManager.AppSettings["sqlconnectionstring"];
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
                //var col = db.GetCollection<Image>("images");
                //return col.FindAll().ToList<Image>();
                List<Image> images = new List<Image>();

                using (SqlConnection con = new SqlConnection(this.sqlConnectionString))
                {
                    con.Open();

                    string sql = @"";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    SqlDataReader data = cmd.ExecuteReader();

                    if(data.HasRows)
                    {
                        while(data.Read())
                        {
                            Image image = new Image();
                            image.Id = Int32.Parse(data["Id"].ToString());
                            image.Title = data["Title"].ToString();
                            image.Caption = data["Caption"].ToString();
                        }
                    }

                    data.Dispose();
                    cmd.Dispose();
                    con.Close();
                }

                return images;
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