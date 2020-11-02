using System;
using System.Windows.Forms;

namespace DataUtilitiesApp.UserInterfaces
{
    public partial class frmMainMenu : Form
    {
        public frmMainMenu()
        {
            InitializeComponent();
        }

        private void btnModifyRulebook_Click(object sender, EventArgs e)
        {
            var rulebook = new frmRulebooks();
            rulebook.Show();
        }
    }
}
