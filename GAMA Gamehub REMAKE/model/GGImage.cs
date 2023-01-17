using GAMA_Gamehub_REMAKE.database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GAMA_Gamehub_REMAKE.model
{
    public class GGImage : DBData
    {
        private int id;
        private string imagePath;
        private GGDatabase database = GGDatabase.GetInstance();


        public GGImage(string image_path)
        {
            this.imagePath = image_path;
        }
        public GGImage(int id, string image_path)
        {
            this.id = id;
            this.imagePath = image_path;
        }


        public GGImage() { }
        public int Id { get{
                //string sql = "SELECT * FROM image ORDER BY id DESC LIMIT 1";
                //MySqlDataReader reader = database.QueryFirstRow(sql);
                //id = (int)reader.GetInt32(0);
                return id; 
            }
                
                set => id = value; }
        public string ImagePath { get => imagePath; set => imagePath = value; }

        public void Delete()
        {
            string sql = string.Format("DELETE FROM image WHERE id='{0}'", id);
            database.Query(sql);
        }

        public void Insert()
        {
            string sql = string.Format("INSERT INTO image (image_path) VALUES ('{0}')", imagePath);
            database.Query(sql);
        }

        public void Select()
        {

        }
        public GGImage SelectLast()
        {
            GGImage image = new GGImage();
            string sql = "SELECT * FROM image ORDER BY id DESC LIMIT 1";
            MySqlDataReader reader = database.QueryFirstRow(sql);
            while (reader.Read())
            {
                image.Id = reader.GetInt32(0);
                image.ImagePath = reader.GetString(1);

            }
            return image;
        }

        public List<GGImage> GetList()
        {
            List<GGImage> list = new List<GGImage>();
            string sql = "SELECT * FROM image";
            MySqlDataReader reader = database.QueryFirstRow(sql);
            while (reader.Read())
            {
                list.Add(new GGImage(reader.GetInt32(0), reader.GetString(1)));
            }
            reader.Close();
            return list;
        }

        public void Update()
        {
            string sql = string.Format("UPDATE image SET image_path = '{0}' WHERE id='{1}'", imagePath, id);
            database.Query(sql);
        }
    }
}
