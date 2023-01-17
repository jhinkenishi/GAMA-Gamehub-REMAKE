using GAMA_Gamehub_REMAKE.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace GAMA_Gamehub_REMAKE.view
{
    public partial class HomeView : UserControl
    {
        private Context context;
   

        public HomeView(Context context)
        {
            this.context = context;
            InitializeComponent();
            foreach(Game game in new Game().GetList())
            {
                listGames.Items.Add(game);
            }
            listGames.SelectedIndex = 0;
            if(context.Login == true && context.User != null)
            {
                btnLogin.Text = context.User.Username;
            }
            else
            {
                btnLogin.Text = "Login";
            }
            
        }

        private void HomeView_Load(object sender, EventArgs e)
        {
            if (context.Login == true && context.User != null)
            {
                btnLogin.Text = context.User.Username;
            }
            else
            {
                btnLogin.Text = "Login";
            }
        }
         

            
        private void ClickAdmin(object sender, EventArgs e)
        {
            try
            {
                if (this.context.User.UserType == "admin" && this.context.Login)
                {
                    this.context.SetState(new AdminView(context));
                }
                else
                {
                    MessageBox.Show("Admin Only!");
                }
            }
            catch(NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }

           

        }

        private void ClickDev(object sender, EventArgs e)
        {
            try
            {
                if ((this.context.User.UserType == "admin" || this.context.User.UserType == "developer") && this.context.Login)
                {
                    this.context.SetState(new DeveloperView(context));
                }
                else
                {
                    MessageBox.Show("Dev Only!");
                }
            }
            catch(NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }

                
        }

        private void ClickLogin(object sender, EventArgs e)
        {
            if(context.Login == true && context.User != null)
            {
                this.context.SetState(new AccountView(context));
            }
            else
            {
                this.context.SetState(new LoginView(context));
                Update();
                Validate();
            }
            
        }

        private void ClickAddToCart(object sender, EventArgs e)
        {
            if(context.Login == true)
            {
                AddGameToCart(context.User, context.Game);
                this.context.SetState(new CartView(context));
            }else
            {
                this.context.SetState(new LoginView(context));
            }
            
        }

        private void ClickCart(object sender, EventArgs e)
        {
            if (context.Login == true)
            {
                this.context.SetState(new CartView(context));
            }
            else
            {
                this.context.SetState(new LoginView(context));
            }
        }

        private void SelectedGame(object sender, EventArgs e)
        {
            context.Game = (Game)listGames.SelectedItem;
            lblTitle.Text = context.Game.Name;
            lblPrice.Text = "$" + context.Game.Price;
            pGame.Image = FindImage(context.Game);
            txtDesc.Text = FindDescription(context.Game);

        }

        private System.Drawing.Image FindImage(Game game)
        {
            System.Drawing.Image tempImage = pGame.ErrorImage;
            foreach (GameImage gi in new GameImage().GetList())
            {
                if (gi.GameId.Equals(game.Id))
                {
                    int gimageId = gi.ImageId;

                    foreach (GGImage gimage in new GGImage().GetList())
                    {
                        if (gimage.Id.Equals(gimageId))
                        {

                            try
                            {
                                string baseDirectory = Environment.CurrentDirectory;
                                string imagePath = Path.Combine(baseDirectory, gimage.ImagePath);
                                tempImage = System.Drawing.Image.FromFile(imagePath);


                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);

                            }
                        }

                    }
                }
            }

            return tempImage;
        }
        public void AddGameToCart(User user, Game game)
        {
            AddGameCart(new GameCart(user.Username, game.Id));
        }
        public void AddGameCart(GameCart gameCart)
        {
            gameCart.Insert();
        }

        public string FindDescription(Game game)
        {
            string description = "No Description";
            foreach (GameDescription gd in new GameDescription().GetList())
            {
                if (gd.GameId.Equals(game.Id))
                {
                    int descriptionId = gd.DescriptionId;

                    foreach (Description desc in new Description().GetList())
                    {
                        if (desc.Id.Equals(descriptionId))
                        {
                            description = desc.Message;

                        }

                    }
                }
            }
            return description;
        }

        private void TextChangeSearch(object sender, EventArgs e)
        {
            string name = txtSearch.Text;
            listGames.Items.Clear();
            foreach(Game game in new Game().GetListByName(name))
            {
                listGames.Items.Add(game);
            }

        }
    }
}
