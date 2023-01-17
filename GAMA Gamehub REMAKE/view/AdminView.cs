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
    public partial class AdminView : UserControl
    {
        private Context context;
        public AdminView(Context context)
        {
            this.context = context;
            this.context.SetState(this);
            InitializeComponent();
        }
        private void AdminView_Load(object sender, EventArgs e)
        {

        }

        private void ClickBack(object sender, EventArgs e)
        {
            this.context.SetState(new HomeView(context));
        }

        private void ClickInsertUser(object sender, EventArgs e)
        {
            this.viewLayout.Controls.Clear();
            this.viewLayout.Controls.Add(new InsertUserView());
  

        }

        private void ClickInsertGame(object sender, EventArgs e)
        {
            InsertGameView insertGameView = new InsertGameView();
            insertGameView.Context = context;
            this.viewLayout.Controls.Clear();
            this.viewLayout.Controls.Add(insertGameView);

        }

        private void ClickUpdateUser(object sender, EventArgs e)
        {
            this.viewLayout.Controls.Clear();
            this.viewLayout.Controls.Add(new UpdateUserView());

        }

        private void ClickUpdateGame(object sender, EventArgs e)
        {
            this.viewLayout.Controls.Clear();
            this.viewLayout.Controls.Add(new UpdateGameView());

        }

        private void ClickDeleteUser(object sender, EventArgs e)
        {
            this.viewLayout.Controls.Clear();
            this.viewLayout.Controls.Add(new DeleteUserView());

        }

        private void ClickDeleteGame(object sender, EventArgs e)
        {
            this.viewLayout.Controls.Clear();
            this.viewLayout.Controls.Add(new DeleteGameView());

        }

    }
}
