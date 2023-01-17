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
    public partial class CartView : UserControl
    {
        private Context context;
        public CartView(Context context)
        {
            this.context = context;
            InitializeComponent();
        }

        private void ClickBack(object sender, EventArgs e)
        {
            this.context.SetState(new HomeView(context));
        }

        private void CartLoad(object sender, EventArgs e)
        {
            string username = context.User.Username;
            ShowGameList(username);
        }

        public void ShowGameList(string username)
        {
            double price = 0;
            listGames.Items.Clear();
            txtCalculation.Text = "";
            foreach(GameCart cart in new GameCart().GetList(username))
            {
                int gameId = cart.GameId;
                foreach(Game game in new Game().GetListById(gameId))
                {
                    listGames.Items.Add(game);
                    price += game.Price;
                    txtCalculation.Text += "+ $" + game.Price +"\n";
                }
            }
            lblTotaLPrice.Text = "Total: $" + price;
        }
    }
}
