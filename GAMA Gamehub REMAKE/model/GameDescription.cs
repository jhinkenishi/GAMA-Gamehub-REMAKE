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
    public class GameDescription : DBData
    {
        private int id;
        private int gameId;
        private int descriptionId;
        private GGDatabase database = GGDatabase.GetInstance();

        public GameDescription(int gameId, int descriptionId)
        {
            this.gameId = gameId;
            this.descriptionId = descriptionId;
        }


        public GameDescription(int id, int gameId, int descriptionId)
        {
            this.id = id;
            this.gameId = gameId;
            this.descriptionId = descriptionId;
        }

        public GameDescription()
        {
        }

        public int Id { get => id; set => id = value; }
        public int GameId { get => gameId; set => gameId = value; }
        public int DescriptionId { get => descriptionId; set => descriptionId = value; }

        public void Delete()
        {
            string sql = string.Format("DELETE FROM game_description WHERE id='{0}'", id);
            database.Query(sql);
        }
        public void DeleteByGame(Game game)
        {
            string sql = string.Format("DELETE FROM game_description WHERE game_id='{0}'", game.Id);
            database.Query(sql);
        }

        public void Insert()
        {
            string sql = string.Format("INSERT INTO game_description (game_id, description_id) VALUES ('{0}', '{1}')", gameId, descriptionId);
            database.Query(sql);
        }

        public void Select()
        {
        }
        public List<GameDescription> GetList()
        {
            List<GameDescription> list = new List<GameDescription>();
            string sql = "SELECT * FROM game_description";
            MySqlDataReader reader = database.QueryFirstRow(sql);
            while (reader.Read())
            {
                list.Add(new GameDescription(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2)));
            }
            reader.Close();
            return list;
        }

        public void Update()
        {
            string sql = string.Format("UPDATE game_description SET game_id = '{0}', description_id ='{1}' WHERE id='{2}'", gameId, descriptionId, id);
            database.Query(sql);
        }

        public List<GameDescription> GetListByGame(Game game)
        {
            List<GameDescription> list = new List<GameDescription>();
            string sql = string.Format("SELECT * FROM game_description WHERE game_id={0}", game.Id);
            MySqlDataReader reader = database.QueryFirstRow(sql);
            while (reader.Read())
            {
                list.Add(new GameDescription(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2)));
            }
            reader.Close();
            return list;
        }
        public List<GameDescription> GetListByGameId(int gameId)
        {
            List<GameDescription> list = new List<GameDescription>();
            string sql = string.Format("SELECT * FROM game_description WHERE game_id={0}", gameId);
            MySqlDataReader reader = database.QueryFirstRow(sql);
            while (reader.Read())
            {
                list.Add(new GameDescription(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2)));
            }
            reader.Close();
            return list;
        }
    }
}
