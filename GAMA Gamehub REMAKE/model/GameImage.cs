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
    public class GameImage : DBData
    {
        private int id, gameId, imageId;
        private GGDatabase database = GGDatabase.GetInstance();

        public GameImage(int gameId, int imageId)
        {
            this.gameId = gameId;
            this.imageId = imageId;
        }
        public GameImage(int id, int gameId, int imageId)
        {
            this.id = id;
            this.gameId = gameId;
            this.imageId = imageId;
        }

        public GameImage()
        {
        }

        public int Id { get => id; set => id = value; }
        public int GameId { get => gameId; set => gameId = value; }
        public int ImageId { get => imageId; set => imageId = value; }

        public void Delete()
        {
            string sql = string.Format("DELETE FROM game_image WHERE id='{0}'", id);
            database.Query(sql);
        }
        public void DeleteByGame(Game game)
        {
            string sql = string.Format("DELETE FROM game_image WHERE game_id='{0}'", game.Id);
            database.Query(sql);
        }


        public void Insert()
        {
            string sql = string.Format("INSERT INTO game_image (game_id, image_id) VALUES ('{0}', '{1}')", gameId, imageId);
            database.Query(sql);
        }

        public void Select()
        {
        }
        public List<GameImage> GetList()
        {
            List<GameImage> list = new List<GameImage>();
            string sql = "SELECT * FROM game_image";
            MySqlDataReader reader = database.QueryFirstRow(sql);
            while (reader.Read())
            {
                list.Add(new GameImage(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2)));
            }
            reader.Close();
            return list;
        }
        public List<GameImage> GetListByGame(Game game)
        {
            List<GameImage> list = new List<GameImage>();
            string sql = string.Format("SELECT * FROM game_image WHERE game_id = {0}", game.Id);
            MySqlDataReader reader = database.QueryFirstRow(sql);
            while (reader.Read())
            {
                list.Add(new GameImage(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2)));
            }
            reader.Close();
            return list;
        }

        public void Update()
        {
            string sql = string.Format("UPDATE game_image SET game_id = '{0}', image_id = '{1}' WHERE id='{2}'", gameId, imageId, id);
            database.Query(sql);
        }
    }
}
