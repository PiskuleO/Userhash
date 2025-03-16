using System;
using System.Windows.Forms;

namespace Userhash
{
    public partial class UserForm : Form
    {
        private User currentUser;
        private UserManager userManager;

        public UserForm(User user, UserManager manager)
        {
            InitializeComponent();
            currentUser = user;
            userManager = manager;
            lblUsername.Text = $"Přihlášen jako: {currentUser.Username}";
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            string newPassword = txtNewPassword.Text;
            currentUser.PasswordHash = User.HashPassword(newPassword);
            userManager.SaveToXml("users.xml");
            MessageBox.Show("Heslo bylo změněno.", "Hotovo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBackToLogin_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }
    }
}