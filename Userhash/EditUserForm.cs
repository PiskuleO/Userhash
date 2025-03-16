using System;
using System.Windows.Forms;

namespace Userhash
{
    public partial class EditUserForm : Form
    {
        private User user;
        private UserManager userManager;

        public EditUserForm(User user, UserManager manager)
        {
            InitializeComponent();
            this.user = user;
            this.userManager = manager;
            txtUsername.Text = user.Username;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string newPassword = txtNewPassword.Text;
            if (!string.IsNullOrEmpty(newPassword))
            {
                user.PasswordHash = User.HashPassword(newPassword);
                userManager.SaveToXml("users.xml");
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtNewPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}