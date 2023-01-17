using GAMA_Gamehub_REMAKE.database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAMA_Gamehub_REMAKE.model
{
    public class Description : DBData
    {

        private int id;
        private string message;
        private GGDatabase database = GGDatabase.GetInstance();

        public Description(string message)
        {
            this.message = message;
        }

        public Description(int id, string message)
        {
            this.id = id;
            this.message = message;

        }
        public Description()
        {

        }


        public int Id
        {
            get
            {
                //string sql = "SELECT * FROM description ORDER BY id DESC LIMIT 1";
                //MySqlDataReader reader = database.QueryFirstRow(sql);
                //id = (int)reader.GetInt32(0);
                return id;
            }

            set => id = value;
        }
        public string Message { get => message; set => message = value; }

        public void Delete()
        {
            string sql = string.Format("DELETE FROM description WHERE id='{0}'", id);
            database.Query(sql);
        }

        public void Insert()
        {
            string sql = string.Format("INSERT INTO description (message) VALUES ('{0}')", message);
            database.Query(sql);
        }

        public void Select()
        {

        }
        public Description SelectLast()
        {
            Description description = new Description();
            string sql = "SELECT * FROM description ORDER BY id DESC LIMIT 1";
            MySqlDataReader reader = database.QueryFirstRow(sql);
            while (reader.Read())
            {

                description = new Description(reader.GetInt32(0), reader.GetString(1));

            }
            return description;
        }
        public List<Description> GetList()
        {
            List<Description> list = new List<Description>();
            string sql = "SELECT * FROM description";
            MySqlDataReader reader = database.QueryFirstRow(sql);
            while (reader.Read())
            {
                list.Add(new Description(reader.GetInt32(0), reader.GetString(1)));
            }
            reader.Close();
            return list;
        }

        public void Update()
        {
            string sql = string.Format("UPDATE description SET message = '{0}' WHERE id='{1}'", message, id);
            database.Query(sql);
        }
    }
}
