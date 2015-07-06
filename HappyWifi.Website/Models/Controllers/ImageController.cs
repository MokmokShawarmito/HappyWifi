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
        string errorMessage = "";

        public ImageController()
        {
            //liteDBPath = HostingEnvironment.ApplicationPhysicalPath + liteDBPath;
            //liteDBPath = liteDBPath.Replace(@"\\",@"\");
            this.sqlConnectionString = ConfigurationManager.ConnectionStrings["MSConnectionString"].ConnectionString;
        }

        public bool Add(Image image)
        {
            int rowAffected = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(this.sqlConnectionString))
                {
                    con.Open();

                    string sql = @"INSERT INTO Company(Title,ImageURL,Location,ContactNo,Caption,IsHidden,Website) VALUES(@Title,@ImageURL,@Location,@ContactNo,@Caption,@IsHidden,@Website)";
                    SqlCommand cmd = new SqlCommand(sql, con);

                    cmd.Parameters.AddWithValue("@Title", image.Title);
                    cmd.Parameters.AddWithValue("@ImageURL", image.ImageUrl);
                    cmd.Parameters.AddWithValue("@Location", image.Location);
                    cmd.Parameters.AddWithValue("@ContactNo", image.ContactNo);
                    cmd.Parameters.AddWithValue("@Caption", image.Caption);
                    cmd.Parameters.AddWithValue("@IsHidden", image.IsHidden);
                    cmd.Parameters.AddWithValue("@Website", image.Website);

                    rowAffected = cmd.ExecuteNonQuery();

                    cmd.Dispose();
                    con.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }

            if (rowAffected > 0)
                return true;
            else
                return false;
        }

        public IEnumerable<Image> GetAll()
        {
            List<Image> images = new List<Image>();

            using (SqlConnection con = new SqlConnection(this.sqlConnectionString))
            {
                con.Open();

                string sql = @"SELECT * FROM Company";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader data = cmd.ExecuteReader();

                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        Image image = new Image();

                        image.Id = Int32.Parse(data["Id"].ToString());
                        image.Title = data["Title"].ToString();
                        image.ImageUrl = data["ImageURL"].ToString();
                        image.Location = data["Location"].ToString();
                        image.ContactNo = data["ContactNo"].ToString();
                        image.Caption = data["Caption"].ToString();
                        image.IsHidden = Boolean.Parse(data["IsHidden"].ToString());
                        image.Website = data["Website"].ToString();

                        images.Add(image);
                    }
                }

                data.Dispose();
                cmd.Dispose();
                con.Close();
            }

            return images;
        }

        public Image GetById(int id)
        {
            Image image = null;

            using (SqlConnection con = new SqlConnection(this.sqlConnectionString))
            {
                con.Open();

                string sql = @"SELECT * FROM Company WHERE ID=@ID";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@ID", id);
                
                SqlDataReader data = cmd.ExecuteReader();

                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        image = new Image();

                        image.Id = Int32.Parse(data["Id"].ToString());
                        image.Title = data["Title"].ToString();
                        image.ImageUrl = data["ImageURL"].ToString();
                        image.Location = data["Location"].ToString();
                        image.ContactNo = data["ContactNo"].ToString();
                        image.Caption = data["Caption"].ToString();
                        image.IsHidden = Boolean.Parse(data["IsHidden"].ToString());
                        image.Website = data["Website"].ToString();
                    }
                }

                data.Dispose();
                cmd.Dispose();
                con.Close();
            }

            return image;
        }

        public bool Update(Image image)
        {
            int rowAffected = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(this.sqlConnectionString))
                {
                    con.Open();

                    string sql = @"UPDATE Company SET Title=@Title, ImageURL=@ImageURL, Location=@Location, ContactNo=@ContactNo, Caption=@Caption, IsHidden=@IsHidden, Website=@Website WHERE ID=@ID";
                    SqlCommand cmd = new SqlCommand(sql, con);

                    cmd.Parameters.AddWithValue("@ID", image.Id);
                    cmd.Parameters.AddWithValue("@Title", image.Title);
                    cmd.Parameters.AddWithValue("@ImageURL", image.ImageUrl);
                    cmd.Parameters.AddWithValue("@Location", image.Location);
                    cmd.Parameters.AddWithValue("@ContactNo", image.ContactNo);
                    cmd.Parameters.AddWithValue("@Caption", image.Caption);
                    cmd.Parameters.AddWithValue("@IsHidden", image.IsHidden);
                    cmd.Parameters.AddWithValue("@Website", image.Website);

                    rowAffected = cmd.ExecuteNonQuery();

                    cmd.Dispose();
                    con.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }

            if (rowAffected > 0)
                return true;
            else
                return false;
        }

        public bool Delete(int id)
        {
            int rowAffected = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(this.sqlConnectionString))
                {
                    con.Open();

                    string sql = @"DELETE Company WHERE ID=@ID";
                    SqlCommand cmd = new SqlCommand(sql, con);

                    cmd.Parameters.AddWithValue("@ID", id);

                    rowAffected = cmd.ExecuteNonQuery();

                    cmd.Dispose();
                    con.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }

            if (rowAffected > 0)
                return true;
            else
                return false;
        }
    }
}