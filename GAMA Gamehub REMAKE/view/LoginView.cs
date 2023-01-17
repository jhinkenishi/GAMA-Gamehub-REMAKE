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
    public partial class LoginView : UserControl
    {
        private Context context;
        public LoginView(Context context)
        {
            this.context = context;
            this.context.SetState(this);
            InitializeComponent();
        }

        private void ClickBack(object sender, EventArgs e)
        {
            this.context.SetState(new HomeView(context));
        }

        private void ClickNoAccount(object sender, EventArgs e)
        {
            this.context.SetState(new RegisterView(context));
        }

        private void ClickLogin(object sender, EventArgs e)
        {
            string username = txtUsernm.Text;
            string password = txtPass.Text;
            List<User> users = new User().GetListByUsername(username);
            bool login = false;
            foreach(User user in users)
            {
                if(user.Username.Equals(username) && user.Password.Equals(password))
                {
                    context.User = user;
                    context.Login = true;
                    login = true;
                    context.SetState(new HomeView(context));
                    MessageBox.Show("Successful");
                }
            }
            if (!login)
            {
                MessageBox.Show("Login Failed");
            }
            
        }
    }
}
