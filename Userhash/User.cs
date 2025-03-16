using System;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace Userhash
{
    [Serializable]
    [XmlInclude(typeof(Admin))]
    public class User
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }

        public User() { }

        public User(string username, string password)
        {
            Username = username;
            PasswordHash = HashPassword(password);
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public bool VerifyPassword(string password)
        {
            return PasswordHash == HashPassword(password);
        }
    }
}