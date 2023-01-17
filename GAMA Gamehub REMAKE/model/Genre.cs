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
    public class Genre : DBData
    {
        private int id;
        private string name;
        private GGDatabase database = GGDatabase.GetInstance();

        public Genre(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
        public Genre() { }
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }

        public void Delete()
        {
            string sql = string.Format("DELETE FROM genre WHERE id='{0}'", id);
            database.Query(sql);
        }

        public void Insert()
        {
            string sql = string.Format("INSERT INTO genre (genre_name) VALUES ('{0}')", name);
            database.Query(sql);
        }

        public void Select()
        {
        }


        public List<Genre> GetList()
        {
            List<Genre> list = new List<Genre>();
            string sql = "SELECT * FROM genre";
            MySqlDataReader reader = database.QueryFirstRow(sql);
            while (reader.Read())
            {
                list.Add(new Genre(reader.GetInt32(0), reader.GetString(1)));
            }
            reader.Close();
            return list;
        }
        public List<Genre> GetListById(int id)
        {
            List<Genre> list = new List<Genre>();
            string sql = string.Format( "SELECT * FROM genre WHERE id='{0}'", id);
            MySqlDataReader reader = database.QueryFirstRow(sql);
            while (reader.Read())
            {
                list.Add(new Genre(reader.GetInt32(0), reader.GetString(1)));
            }
            reader.Close();
            return list;
        }

        public void Update()
        {
            string sql = string.Format("UPDATE genre SET genre_name = '{0}' WHERE id='{1}'", name, id);
            database.Query(sql);
        }

        public override string ToString()
        {
            return name;
        }
    }
}
