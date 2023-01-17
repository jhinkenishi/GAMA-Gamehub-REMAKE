using GAMA_Gamehub_REMAKE.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace GAMA_Gamehub_REMAKE.view
{
    public partial class UpdateGameView : UserControl
    {
        private string fileName;
        private const string ROOT_FOLDER = "resource";
        private const string SUB_FOLDER = "images";
        private string fileImagePath;
        public UpdateGameView()
        {
            InitializeComponent();
            RefreshDB();

        }

        private void ClickUpdate(object sender, EventArgs e)
        {
            Game selectedGame = (Game)cGames.SelectedItem;
            int id = selectedGame.Id;
            string name = txtTitle.Text;
            //string publisher = context.GetLogonUsername();
            string message = txtDesc.Text;
            double price = (double)numPrice.Value;
            string imageFilePath = ROOT_FOLDER + "\\\\" + SUB_FOLDER + "\\\\" + fileName;
            int genreId = new Genre().GetList()[cGenre.SelectedIndex].Id;

            selectedGame = new Game(id, genreId, name, 0, price, 0);
            GGImage image = new GGImage(imageFilePath);
            Description description = new Description(message);
            UpdateGame(selectedGame, image, description);
            RefreshDB();
        }

        public void UpdateGame(Game game, GGImage image, Description description) 
        {
            List<GameImage> images = new GameImage().GetListByGame(game);
            List<GameDescription> descriptions = new GameDescription().GetListByGame(game);

            if(images.Count > 0 && descriptions.Count > 0)
            {
                int imageId = images.Last().ImageId;
                int descriptionId = descriptions.Last().DescriptionId;
                image.Id = imageId;
                description.Id = descriptionId;

                game.Update();
                image.Update();
                description.Update();


            }else if(images.Count > 0 && descriptions.Count < 1)
            {
                int imageId = images.Last().ImageId;
                image.Id = imageId;

                game.Update();
                image.Update();
                description.Insert();
                AddGameDescription(new GameDescription(game.Id, description.GetList().Last().Id));
            }
            else if (images.Count < 1 && descriptions.Count > 0)
            {
                int descriptionId = descriptions.Last().DescriptionId;
                description.Id = descriptionId;

                game.Update();
                image.Insert();
                description.Update();
                AddGameImage(new GameImage(game.Id, image.GetList().Last().Id));
            }
            else
            {
                game.Update();

                image.Insert();
                description.Insert();
                AddGameImage(new GameImage(game.Id, image.GetList().Last().Id));
                AddGameDescription(new GameDescription(game.Id, description.GetList().Last().Id));
            }

            MessageBox.Show("Successful");
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
        public void AddGame(Game game, GGImage image, Description description)
        {
            game.Insert();
            image.Insert();
            description.Insert();
            AddGameImage(new GameImage(game.GetList().Last().Id, image.GetList().Last().Id));
            AddGameDescription(new GameDescription(game.GetList().Last().Id, description.GetList().Last().Id));

        }

        public void AddGameImage(GameImage gameImage)
        {
            gameImage.Insert();
        }

        public void AddGameDescription(GameDescription gameDescription)
        {
            gameDescription.Insert();
        }
        private System.Drawing.Image FindImage(Game game)
        {
            System.Drawing.Image tempImage = picGame.ErrorImage;
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
        public void RefreshDB()
        {
            cGames.Items.Clear();
            foreach (Game game in new Game().GetList())
            {
                cGames.Items.Add(game);
            }
            cGenre.Items.Clear();
            foreach (Genre genre in new Genre().GetList())
            {
                cGenre.Items.Add(genre);
            }

        }
        public string FindGenre(Game game)
        {
            int id = game.GenreId;
            string genreName = "";
            foreach(Genre genre in new Genre().GetListById(id))
            {
                genreName = genre.Name;
            }
            return genreName;
        }

        private void TextChangeSearch(object sender, EventArgs e)
        {
            string name = txtSearch.Text;
            cGames.Items.Clear();
            foreach (Game game in new Game().GetListByName(name))
            {
                cGames.Items.Add(game);
            }
        }

        private void SelectedGame(object sender, EventArgs e)
        {
            Game game = (Game)cGames.SelectedItem;
            txtTitle.Text = game.Name;
            numPrice.Value = (decimal)game.Price;
            picGame.Image = FindImage(game);
            txtDesc.Text = FindDescription(game);
            cGenre.Text = FindGenre(game);
        }

        private void ClickUploadImage(object sender, EventArgs e)
        {
            imageFilePicker.ShowDialog();
        }

        private void FileOk(object sender, CancelEventArgs e)
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
    }
}
