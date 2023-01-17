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
    public partial class AccountView : UserControl
    {
        private Context context;
        public AccountView(Context context)
        {
            this.context = context;
            InitializeComponent();
        }

        private void ClickLogout(object sender, EventArgs e)
        {
            context.User = null;
            context.Login = false;
            context.SetState(new HomeView(context));
        }

        private void ClickBack(object sender, EventArgs e)
        {
            this.context.SetState(new HomeView(context));
        }
    }
}
