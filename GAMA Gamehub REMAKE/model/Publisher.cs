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
    public class Publisher : DBData
    {
        private int id;
        private string publisherName;
        private GGDatabase database = GGDatabase.GetInstance();

        public Publisher(string publisherName)
        {
            this.publisherName = publisherName;
        }
        public Publisher(int id, string publisherName)
        {
            this.id = id;
            this.publisherName = publisherName;
        }

        public Publisher()
        {
        }

        public int Id { get => id; set => id = value; }
        public string PublisherName { get => publisherName; set => publisherName = value; }

        public void Delete()
        {
            string sql = string.Format("DELETE FROM publisher WHERE id='{1}'", id);
            database.Query(sql);
        }

        public void Insert()
        {
            string sql = string.Format("INSERT INTO publisher (publisher_name) VALUES ('{0}')", publisherName);
            database.Query(sql);
        }

        public void Select()
        {
        }
        public List<Publisher> GetList()
        {
            List<Publisher> list = new List<Publisher>();
            string sql = "SELECT * FROM publisher";
            MySqlDataReader reader = database.QueryFirstRow(sql);
            while (reader.Read())
            {
                list.Add(new Publisher(reader.GetInt32(0), reader.GetString(1)));
            }
            reader.Close();
            return list;
        }

        public void Update()
        {
            string sql = string.Format("UPDATE publisher SET publisher_name = '{0}' WHERE id='{1}'", publisherName, id);
            database.Query(sql);
        }
    }
}
