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
    public class User : DBData
    {
        private string name;
        private string email;
        private string username;
        private string password;
        private int loginStatus;
        private string userType;
        private GGDatabase database = GGDatabase.GetInstance();

        public User(string name, string email, string username, string password, int loginStatus, string userType)
        {
            this.name = name;
            this.email = email;
            this.username = username;
            this.password = password;
            this.loginStatus = loginStatus;
            this.userType = userType;
        }

        public User()
        {
        }

        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public int LoginStatus { get => loginStatus; set => loginStatus = value; }
        public string UserType { get => userType; set => userType = value; }

        public void Delete()
        {
            string sql = string.Format("DELETE FROM game_users WHERE username='{0}'", username);
            database.Query(sql);
        }

        public void Insert()
        {
            string sql = string.Format("INSERT INTO game_users (name, email, username, password, login_status, user_type) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}' )", name, email, username, password, loginStatus, userType);
            database.Query(sql);
        }

        public void Select()
        {
        }
        public List<User> GetList()
        {
            List<User> list = new List<User>();
            string sql = "SELECT * FROM game_users";
            MySqlDataReader reader = database.QueryFirstRow(sql);
            while (reader.Read())
            {
                list.Add(new User(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetString(5)));
            }
            reader.Close();
            return list;
        }
        public List<User> GetListByUsername(string username)
        {
            List<User> list = new List<User>();
            string sql = string.Format("SELECT * FROM game_users WHERE username LIKE '%{0}%'", username);
            MySqlDataReader reader = database.QueryFirstRow(sql);
            while (reader.Read())
            {
                list.Add(new User(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetString(5)));
            }
            reader.Close();
            return list;
        }

        public void Update()
        {
            string sql = string.Format("UPDATE game_users SET name = '{0}', email = '{1}', username = '{2}', password = '{3}', login_status = '{4}', user_type = '{5}' WHERE username='{6}'", name, email, username, password, loginStatus, userType, username);
            database.Query(sql);

        }

        public override string ToString()
        {
            return username;
        }
    }
}
