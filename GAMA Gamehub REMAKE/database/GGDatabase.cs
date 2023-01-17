using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GAMA_Gamehub_REMAKE.database
{
    public class GGDatabase
    {
        private static GGDatabase instance;
        private MySqlConnection connection;
        private MySqlCommand command;
        private MySqlDataReader reader;

        public void Query(string sql)
        {
            connection = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=gama_gamehub");
            command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                connection.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public MySqlDataReader QueryFirstRow(string sql)
        {
            connection = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=gama_gamehub");
            command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                //connection.Close();
                return reader;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;

            }
        }

        public static GGDatabase GetInstance()
        {
            if (instance is null)
            {
                instance = new GGDatabase();
            }
            return instance;
        }
    }
}
