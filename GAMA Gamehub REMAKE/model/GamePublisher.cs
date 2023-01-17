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
    public class GamePublisher : DBData
    {
        private int id;
        private int gameId;
        private int publisherId;
        private GGDatabase database = GGDatabase.GetInstance();

        public GamePublisher(int id, int gameId, int publisherId)
        {
            this.id = id;
            this.gameId = gameId;
            this.publisherId = publisherId;
        }
        public GamePublisher(int gameId, int publisherId)
        {
            this.gameId = gameId;
            this.publisherId = publisherId;
        }
        public GamePublisher(int gameId)
        {
            this.gameId = gameId;
        }


        public GamePublisher()
        {
        }

        public int Id { get => id; set => id = value; }
        public int GameId { get => gameId; set => gameId = value; }
        public int PublisherId { get => publisherId; set => publisherId = value; }

        public void Delete()
        {
            string sql = string.Format("DELETE FROM game_publisher WHERE id='{0}'", id);
            database.Query(sql);
        }

        public void DeleteByGame(Game game)
        {
            string sql = string.Format("DELETE FROM game_publisher WHERE game_id = {0}", game.Id);
            database.Query(sql);
        }

        public void Insert()
        {
            string sql = string.Format("INSERT INTO game_publisher (game_id, publisher_id) VALUES ('{0}', '{1}')", gameId, publisherId);
            database.Query(sql);
        }

        public void Select()
        {
        }

        public List<GamePublisher> GetList()
        {
            List<GamePublisher> list = new List<GamePublisher>();
            string sql = "SELECT * FROM game_publisher";
            MySqlDataReader reader = database.QueryFirstRow(sql);
            while (reader.Read())
            {
                list.Add(new GamePublisher(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2)));
            }
            reader.Close();
            return list;
        }
        public void Update()
        {
            string sql = string.Format("UPDATE game_publisher SET game_id = '{0}', publisher_id='{1}' WHERE id='{2}'", gameId, publisherId, id);
            database.Query(sql);
        }
    }
}
