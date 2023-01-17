using GAMA_Gamehub_REMAKE.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GAMA_Gamehub_REMAKE.view
{
    public partial class InsertGameView : UserControl
    {
        private Context context;
        private string fileName;
        private const string ROOT_FOLDER = "resource";
        private const string SUB_FOLDER = "images";

        public Context Context { get => context; set => context = value; }

        public InsertGameView()
        {InitializeComponent();

        }

        private void ClickPublish(object sender, EventArgs e)
        {
            string name = txtTitle.Text;
            //string publisher = context.GetLogonUsername();
            string message = txtDesc.Text;
            double price = (double)numPrice.Value;
            string imageFilePath = ROOT_FOLDER + "\\\\" + SUB_FOLDER + "\\\\" + fileName;
            int genreId = new Genre().GetList()[cGenre.SelectedIndex].Id;
            Context.User = new User("admin", "admin", "admin", "admin", 1, "admin");
            Context.Login = true;

            Game game = new Game(genreId, name, 0, price, 0);
            GGImage image = new GGImage(imageFilePath);
            Description description = new Description(message);

            if (Verify(game, message, imageFilePath) && Context.Login && (Context.User.UserType == "developer" || Context.User.UserType == "admin"))
            {
                AddGame(game, image, description);
                SetUpImage(new GameImage(game.SelectLast().Id, image.SelectLast().Id));
                SetUpDescription(new GameDescription(game.SelectLast().Id, description.SelectLast().Id));
                MessageBox.Show("Successfully");
            }
            else
            {
                MessageBox.Show("Invalid Fields");
            }

        }
        public bool Verify(Game game, string description, string imageFilePath)
        {
            string name = game.Name;
            double price = game.Price;

            if (name == "" || description == "" || price == 0 || imageFilePath == "")
            {
                if (name == "")
                {
                    MessageBox.Show("No Name Specified");
                }
                else if (description == "")
                {
                    MessageBox.Show("No Description");
                }
                else if (price == 0)
                {
                    MessageBox.Show("Price is not set!");
                }
                else if (imageFilePath == "")
                {
                    MessageBox.Show("Image is not set!");
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        public void AddGame(Game game, GGImage image, Description description)
        {
            game.Insert();
            image.Insert();
            description.Insert();
        }
        public void SetUpImage(GameImage gameImage)
        {
            AddGameImage(gameImage);
        }
        public void SetUpDescription(GameDescription gameDescription)
        {
            AddGameDescription(gameDescription);
        }

        public void AddGameImage(GameImage gameImage)
        {
            gameImage.Insert();
        }

        public void AddGameDescription(GameDescription gameDescription)
        {
            gameDescription.Insert();
        }

    }
}
