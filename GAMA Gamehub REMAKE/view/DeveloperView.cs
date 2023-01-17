using GAMA_Gamehub_REMAKE.model;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace GAMA_Gamehub_REMAKE.view
{
    public partial class DeveloperView : UserControl
    {
        private Context context;
        private string fileName;
        private const string ROOT_FOLDER = "resource";
        private const string SUB_FOLDER = "images";

        public DeveloperView(Context context)
        {
            this.context = context;
            this.context.SetState(this);
            InitializeComponent();

            List<Genre> genres = new Genre().GetList();
            foreach(Genre genre in genres)
            {
                cGenre.Items.Add(genre.Name);
            }
            cGenre.SelectedIndex = 0;

        }

        private void DeveloperView_Load(object sender, EventArgs e)
        {
        }

        private void ClickBack(object sender, EventArgs e)
        {
            this.context.SetState(new HomeView(context));
        }

        private void ImageOk(object sender, CancelEventArgs e)
        {
            string sourcePath = imageFilePicker.FileName;

            string targetFolder = Path.Combine(Environment.CurrentDirectory, ROOT_FOLDER, SUB_FOLDER);

            if (!Directory.Exists(targetFolder))
            {
                Directory.CreateDirectory(targetFolder);
            }
            MessageBox.Show(targetFolder);
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            fileName = $"image_{timestamp}.jpg";
            string targetPath = Path.Combine(targetFolder, fileName);

            try
            {
                File.Copy(sourcePath, targetPath, true);
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }

            System.Drawing.Image image = System.Drawing.Image.FromFile(sourcePath);
            picGame.Image = image;
        }

        private void ClickUploadImage(object sender, EventArgs e)
        {
            imageFilePicker.ShowDialog();
        }

        private void ClickPublish(object sender, EventArgs e)
        {
            string name = txtTitle.Text;
            //string publisher = context.GetLogonUsername();
            string message = txtDesc.Text;
            double price = (double)numPrice.Value;
            string imageFilePath = ROOT_FOLDER + "\\\\" + SUB_FOLDER + "\\\\" + fileName;
            int genreId = new Genre().GetList()[cGenre.SelectedIndex].Id;


            Game game = new Game(genreId, name, 0, price, 0);
            GGImage image = new GGImage( imageFilePath);
            Description description = new Description(message);

            if (Verify(game, message, imageFilePath) && context.Login && (context.User.UserType == "developer" || context.User.UserType == "admin" ) )
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
            
            if (name == ""  || description == "" || price == 0 || imageFilePath == "")
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
                }else if(imageFilePath == "")
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
        public void SetUpImage( GameImage gameImage)
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
