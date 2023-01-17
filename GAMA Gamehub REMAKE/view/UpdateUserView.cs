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
    public partial class UpdateUserView : UserControl
    {
        public UpdateUserView()
        {
            InitializeComponent();
            cUsertype.Items.Add("basic");
            cUsertype.Items.Add("developer");
            cUsertype.Items.Add("admin");
            cUsertype.SelectedIndex = 0;
            foreach (User user in new User().GetList())
            {
                cUser.Items.Add(user);
            }
            cUser.SelectedIndex = 0;
        }

        private void ClickUpdate(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string email = txtEmail.Text;
            string username = txtUsernm.Text;
            string password = txtPass.Text;
            int login = chkLogin.Checked ? 1 : 0;
            string type = cUsertype.SelectedItem.ToString();

            UpdateUser(new User(name, email, username, password, login, type));

        }

        private void SelectedUser(object sender, EventArgs e)
        {
            User selectedUser = (User)cUser.SelectedItem;
            string name = selectedUser.Name;
            string email = selectedUser.Email;
            string username = selectedUser.Username;
            string password = selectedUser.Password;
            int login = selectedUser.LoginStatus;
            string type = selectedUser.UserType;

            txtName.Text = name;
            txtEmail.Text = email;
            txtUsernm.Text = username;
            txtPass.Text = password;
            chkLogin.Checked = login == 1 ? true : false;
            cUsertype.Text = type;

        }

        public void UpdateUser(User user)
        {
            user.Update();
            MessageBox.Show("Successful");
        }
    }
}
