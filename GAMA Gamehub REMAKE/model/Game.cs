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
    public class Game : DBData
    {
        private int id, genreId;
        private string name;
        private double rating;
        private double price;
        private int sold;
        private GGDatabase database = GGDatabase.GetInstance();


        public Game(int id, int genreId, string name, double rating, double price, int sold)
        {
            this.Id = id;
            this.GenreId = genreId;
            this.Name = name;
            this.rating = rating;
            this.price = price;
            this.sold = sold;
        }
        public Game(int genreId, string name, double rating, double price, int sold)
        {
            this.GenreId = genreId;
            this.Name = name;
            this.rating = rating;
            this.price = price;
            this.sold = sold;
        }

        public Game()
        {
        }

        public int Id { get => id; set => id = value; }
        public int GenreId { get => genreId; set => genreId = value; }
        public string Name { get => name; set => name = value; }
        public double Price { get => price; set => price = value; }
        public int Sold { get => sold; set => sold = value; }
        public double Rating { get => rating; set => rating = value; }

        public void Delete()
        {
            MessageBox.Show("GAME ID: " + id);
            string sql = string.Format("DELETE FROM game WHERE id='{0}'", id);
            database.Query(sql);
        }

        public void Insert()
        {
            string sql = string.Format("INSERT INTO game (genre_id, game_name, game_rating, game_price, game_sold) VALUES ('{0}', '{1}', '{2}', '{3}','{4}')", genreId, name, rating, price, sold);
            database.Query(sql);
        }

        public void Select()
        {
        }
        public List<Game> GetList()
        {
            List<Game> list = new List<Game>();
            string sql = "SELECT * FROM game LIMIT 30";
            MySqlDataReader reader = database.QueryFirstRow(sql);
            while (reader.Read())
            {
                list.Add(new Game(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetDouble(3), reader.GetDouble(4), reader.GetInt32(5)));
            }
            reader.Close();
            return list;
        }
        public List<Game> GetListByName(string name)
        {
            List<Game> list = new List<Game>();
            string sql = string.Format("SELECT * FROM game  WHERE game_name LIKE '%{0}%' LIMIT 30", name);
            MySqlDataReader reader = database.QueryFirstRow(sql);
            while (reader.Read())
            {
                list.Add(new Game(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetDouble(3), reader.GetDouble(4), reader.GetInt32(5)));
            }
            reader.Close();
            return list;
        }
        public Game SelectLast()
        {
            Game game = new Game();
            string sql = "SELECT * FROM game ORDER BY id DESC LIMIT 1";
            MySqlDataReader reader = database.QueryFirstRow(sql);
            while (reader.Read())
            {

                game = new Game(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetDouble(3), reader.GetDouble(4), reader.GetInt32(5));

            }
            return game;
        }
        public List<Game> GetListById(int id)
        {
            List<Game> list = new List<Game>();
            string sql = string.Format("SELECT * FROM game  WHERE id='{0}' LIMIT 30", id);
            MySqlDataReader reader = database.QueryFirstRow(sql);
            while (reader.Read())
            {
                list.Add(new Game(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetDouble(3), reader.GetDouble(4), reader.GetInt32(5)) );
            }
            reader.Close();
            return list;
        }
        public void Update()
        {
            string sql = string.Format("UPDATE game SET genre_id='{0}', game_name='{1}', game_rating='{2}', game_price='{3}', game_sold='{4}' WHERE id='{5}'", genreId, name, rating, price, sold, id);
            database.Query(sql);
        }

        public override string ToString()
        {
            return name;
        }
    }
}
