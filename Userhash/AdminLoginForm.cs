using System;
using System.Windows.Forms;

namespace Userhash
{
    public partial class AdminLoginForm : Form
    {
        private UserManager userManager;

        public AdminLoginForm(UserManager manager)
        {
            InitializeComponent();
            userManager = manager;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string password = txtAdminPassword.Text;
            User admin = userManager.FindUser("admin");

            if (admin != null && admin.VerifyPassword(password))
            {
                AdminForm adminForm = new AdminForm(userManager);
                adminForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Neplatné heslo.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}