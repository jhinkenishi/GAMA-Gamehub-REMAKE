using GAMA_Gamehub_REMAKE.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GAMA_Gamehub_REMAKE.view
{
    public partial class DeleteGameView : UserControl
    {
        public DeleteGameView()
        {
            InitializeComponent();
            RefreshGameDB();
        }

        private void ClickDelete(object sender, EventArgs e)
        {
            Game game = (Game)cGames.SelectedItem;
            DeleteGame(game);
            RefreshGameDB();

        }
        public void DeleteGame(Game game)
        {
            new GamePublisher().DeleteByGame(game);
            new GameDescription().DeleteByGame(game);
            new GameImage().DeleteByGame(game);
            game.Delete();
            MessageBox.Show("Successful");
        }

   
        private void SelectedGame(object sender, EventArgs e)
        {

        }
        public void RefreshGameDB()
        {
            cGames.Items.Clear();
            foreach (Game game in new Game().GetList())
            {
                cGames.Items.Add(game);
            }
            cGames.SelectedIndex = 0;
        }
        private void TextChange(object sender, EventArgs e)
        {

        }

        private void TextChangeSearch(object sender, EventArgs e)
        {
            cGames.Items.Clear();
            string name = txtSearch.Text;
            foreach (Game game in new Game().GetListByName(name))
            {
                cGames.Items.Add(game);
            }
        }
    }
}
