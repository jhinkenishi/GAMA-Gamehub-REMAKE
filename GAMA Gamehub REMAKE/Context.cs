using GAMA_Gamehub_REMAKE.model;
using GAMA_Gamehub_REMAKE.view;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GAMA_Gamehub_REMAKE
{
    public partial class Context : Form
    {
        private Control state;
        private User user;
        private bool login;
        private Game game;

        public Game Game { get => game; set => game = value; }


        public User User { get => user; set => user = value; }
        public bool Login { get => login; set => login = value; }

        public Context()
        {
            SetState(new HomeView(this));
            InitializeComponent();
        }

        public void SetState(Control state)
        {
            this.state = state;
            this.Controls.Clear();
            state.Dock = DockStyle.Fill;
            this.Controls.Add(state);
        }

        

        public Control GetState()
        {
            return this.state;
        }
        private void Context_Load(object sender, EventArgs e)
        {

        }
    }
}
