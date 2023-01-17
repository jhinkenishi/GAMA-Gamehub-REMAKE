using GAMA_Gamehub_REMAKE.database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace GAMA_Gamehub_REMAKE.model
{
    public class GameCart : DBData
    {
        private int id;
        private string username;
        private int gameId;
        private GGDatabase database = GGDatabase.GetInstance();

        public int Id { get => id; set => id = value; }
        public string Username { get => username; set => username = value; }
        public int GameId { get => gameId; set => gameId = value; }

        public GameCart(int id, string username, int gameId)
        {
            this.Id = id;
            this.Username = username;
            this.GameId = gameId;
        }
        public GameCart( string username, int gameId)
        {
            this.Username = username;
            this.GameId = gameId;
        }
        public GameCart() { }

        public List<GameCart> GetList()
        {
            List<GameCart> list = new List<GameCart>();
            string sql = "SELECT * FROM game_cart";
            MySqlDataReader reader = database.QueryFirstRow(sql);
            while (reader.Read())
            {
                list.Add(new GameCart(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
            }
            reader.Close();
            return list;
        }
        public List<GameCart> GetList(string username)
        {
            List<GameCart> list = new List<GameCart>();
            string sql = string.Format("SELECT * FROM game_cart WHERE username='{0}'", username);
            MySqlDataReader reader = database.QueryFirstRow(sql);
            while (reader.Read())
            {
                list.Add(new GameCart(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
            }
            reader.Close();
            return list;
        }

        public void Insert()
        {
            string sql = string.Format("INSERT INTO game_cart (username, game_id) VALUES ('{0}', '{1}')", username, gameId);
            database.Query(sql);
        }

        public void Update()
        {
            string sql = string.Format("UPDATE game_cart SET username = '{0}', game_id='{1}' WHERE id='{2}'", username, gameId, id);
            database.Query(sql);
        }

        public void Delete()
        {
            string sql = string.Format("DELETE FROM game_cart WHERE id='{0}'", id);
            database.Query(sql);
        }

        public void Select()
        {
        }
    }
}
