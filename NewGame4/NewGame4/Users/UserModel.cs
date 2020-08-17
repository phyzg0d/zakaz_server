using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace NewGame4.Users
{
    public class UserModel
    {
        private Dictionary<string, IUserUnitModel> _users = new Dictionary<string, IUserUnitModel>();
        public List<string> Emails = new List<string>();
        public void Serialize(ServerContext context)
        {
            foreach (var user in _users.Values)
            {
                var command = new MySqlCommand("")
                {
                    Connection = context.BdConnection.Connection
                };
                
                if (user.IsNew)
                {
                    user.IsNew = false;
                    command.CommandText = "USE users";
                    command.CommandText = $"INSERT INTO users(Name, SecondName, Email, Password) VALUES('{user.Name}', '{user.SecondName}', '{user.Email}', '{user.Password}')";
                    command.ExecuteNonQuery();
                }
                else
                {
                    // command.CommandText = $"UPDATE users SET (Name, SecondName, Password, Email, Session) VALUES( '{user.Name}', '{user.SecondName}', '{user.Password}', '{user.Email}', '{user.Session}')";
                    // command.ExecuteNonQuery();
                }
            }
        }

        public void Deserialize(ServerContext context)
        {
            var command = new MySqlCommand("")
            {
                Connection = context.BdConnection.Connection
            };
            
            command.CommandText = $"SELECT * FROM users";
            var userReader = command.ExecuteReader();
            while (userReader.Read())
            {
                var user = new UserUnitModel()
                {
                    Id = userReader.GetString("id"),
                    Name = userReader.GetString("Name"),
                    SecondName = userReader.GetString("SecondName"),
                    Email = userReader.GetString("Email"),
                    Password = userReader.GetString("Password"),
                    Session = userReader.GetString("Session"),
                };
                _users.Add(user.Id, user);
                Emails.Add(user.Email);
            }
        }

        public IUserUnitModel Get(string id)
        {
            return _users[id];
        }

        public bool Contains(string id)
        {
            return _users.ContainsKey(id);
        }

        public void Add(string id, IUserUnitModel user)
        {
            _users.Add(id, user);
            Emails.Add(user.Email);
        }
    }
}