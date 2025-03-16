using System;
using System.Windows.Forms;

namespace Userhash
{
    public partial class AdminForm : Form
    {
        private UserManager userManager;

        public AdminForm(UserManager manager)
        {
            InitializeComponent();
            userManager = manager;
            LoadUsers();
        }

        private void LoadUsers()
        {
            lstUsers.Items.Clear();
            foreach (var user in userManager.Users)
            {
                lstUsers.Items.Add(user.Username);
            }
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (lstUsers.SelectedItem != null)
            {
                string selectedUser = lstUsers.SelectedItem.ToString();
                User user = userManager.FindUser(selectedUser);

                if (user != null)
                {
                    string newPassword = txtNewPassword.Text;
                    user.PasswordHash = User.HashPassword(newPassword);
                    userManager.SaveToXml("users.xml");
                    MessageBox.Show($"Heslo uživatele {user.Username} bylo změněno.", "Hotovo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Vyberte uživatele ze seznamu.", "Upozornění", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            string newUsername = txtNewUsername.Text;
            string newPassword = txtNewPassword.Text;

            if (string.IsNullOrEmpty(newUsername) || string.IsNullOrEmpty(newPassword))
            {
                MessageBox.Show("Zadejte uživatelské jméno a heslo.", "Upozornění", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (userManager.FindUser(newUsername) != null)
            {
                MessageBox.Show("Uživatel s tímto jménem již existuje.", "Upozornění", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            User newUser = new User(newUsername, newPassword);
            userManager.AddUser(newUser);
            userManager.SaveToXml("users.xml");

            LoadUsers();
            txtNewUsername.Text = "";
            txtNewPassword.Text = "";
        }

        private void btnBackToLogin_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }
    }
}