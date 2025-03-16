using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Userhash
{
    public class UserManager
    {
        public List<User> Users { get; set; } = new List<User>();

        public void SaveToXml(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
            using (TextWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, Users);
            }
        }

        public void LoadFromXml(string filePath)
        {
            if (!File.Exists(filePath) || new FileInfo(filePath).Length == 0)
            {
                Users = new List<User>();
                return;
            }

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
                using (TextReader reader = new StreamReader(filePath))
                {
                    Users = (List<User>)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading XML: {ex.Message}");
                Users = new List<User>();
            }
        }

        public User FindUser(string username)
        {
            return Users.Find(u => u.Username == username);
        }

        public void AddUser(User user)
        {
            Users.Add(user);
        }
    }
}