using GAMA_Gamehub_REMAKE.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GAMA_Gamehub_REMAKE.view
{
    public partial class InsertUserView : UserControl
    {

        public InsertUserView()
        {
            InitializeComponent();
            cUsertype.Items.Add("basic");
            cUsertype.Items.Add("developer");
            cUsertype.Items.Add("admin");
            cUsertype.SelectedIndex = 0;

        }
        public bool ValidateAll(string email, string username, string password)
        {
            if (ValidateEmail(email) && ValidatePassword(username) && ValidatePassword(password))
            {
                MessageBox.Show("Successfully");
                return true;
            }
            return false;
        }
        public bool ValidateEmail(string email)
        {
            string pattern = "^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$";
            if (Regex.IsMatch(email, pattern))
            {
                return true;
            }
            MessageBox.Show("Invalid Email");
            return false;
        }
        public bool ValidateUsername(string username)
        {
            string pattern = "^[a-zA-Z0-9]{4,}$";

            if (Regex.IsMatch(username, pattern))
            {
                return true;
            }
            MessageBox.Show("Invalid Username");
            return false;
        }
        public bool ValidatePassword(string password)
        {
            //string pattern = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$";

            //if (Regex.IsMatch(password, pattern))
            //{
            //    MessageBox.Show("Test");
            //    return true;
            //}
            //MessageBox.Show("Invalid Password");
            if (password == "" || password.Length < 8)
            {
                if (password == "")
                {
                    MessageBox.Show("Password is Empty");

                }
                else if (password.Length < 8)
                {
                    MessageBox.Show("Password is weak");
                }
                return false;
            }
            return true;
        }

        private void ClickRegister(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string email = txtEmail.Text;
            string username = txtUsernm.Text;
            string password = txtPass.Text;
            string confirmPassword = txtConPass.Text;
            int login = chkLogin.Checked ? 1 : 0;
            string type = cUsertype.SelectedItem.ToString();


            User user = new User(name, email, username, password, login, type);
            if (ValidateAll(email, username, password) && password == confirmPassword)
            {
                AddUser(user);
            }
            else if (password != confirmPassword)
            {
                MessageBox.Show("The password confirmation does not match");
            }
        }
        public void AddUser(User user)
        {
            user.Insert();
        }
    }
}
