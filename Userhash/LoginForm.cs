using System;
using System.Windows.Forms;

namespace Userhash
{
    public partial class LoginForm : Form
    {
        private UserManager userManager;

        public LoginForm()
        {
            InitializeComponent();
            userManager = new UserManager();
            userManager.LoadFromXml("users.xml");

            if (userManager.FindUser("admin") == null)
            {
                userManager.AddUser(new Admin("admin", "admin123"));
                userManager.SaveToXml("users.xml");
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (username == "admin")
            {
                AdminLoginForm adminLoginForm = new AdminLoginForm(userManager);
                adminLoginForm.Show();
                this.Hide();
            }
            else
            {
                User user = userManager.FindUser(username);

                if (user != null && user.VerifyPassword(password))
                {
                    UserForm userForm = new UserForm(user, userManager);
                    userForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Neplatné přihlašovací údaje.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}