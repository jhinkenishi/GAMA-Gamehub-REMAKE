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
    public partial class DeleteUserView : UserControl
    {
        public DeleteUserView()
        {
            InitializeComponent();
            RefreshUserDB();
        }

        private void ClickDelete(object sender, EventArgs e)
        {
            User user = (User)cUser.SelectedItem;
            DeleteUser(user);
            RefreshUserDB();
        }

        public void DeleteUser(User user)
        {
            user.Delete();
            MessageBox.Show("Successful");
        }

        private void SelectedUser(object sender, EventArgs e)
        {
            
        }
        public void RefreshUserDB()
        {
            cUser.Items.Clear();
            foreach (User user in new User().GetList())
            {
                cUser.Items.Add(user);
            }
            cUser.SelectedIndex = 0;
        }
        private void TextChange(object sender, EventArgs e)
        {
            cUser.Items.Clear();
            string username = txtSearch.Text;
            foreach (User user in new User().GetListByUsername(username))
            {
                cUser.Items.Add(user);
            }
        }
    }
}
