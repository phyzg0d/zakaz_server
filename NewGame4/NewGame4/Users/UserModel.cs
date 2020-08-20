using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace NewGame4.Users
{
    public class UserModel
    {
        private Dictionary<string, IUserUnitModel> _users = new Dictionary<string, IUserUnitModel>();
        public Dictionary<string, string> emails = new Dictionary<string, string>();
        public byte[] CurrentSplash;
        public void Serialize(ServerContext context)
        {
            foreach (var user in _users.Values)
            {
                var command = new SqlCommand("")
                {
                    Connection = context.BdConnection.Connection
                };
                
                if (user.IsNew)
                {
                    user.IsNew = false;
                    command.CommandText = $"INSERT INTO users(name, user_id, is_authorisation, second_name, email, password, session) VALUES('{user.Name}', '{user.UserId}', '{user.IsAuthorisation}', '{user.SecondName}', '{user.Email}', '{user.Password}', '{user.Session}')";
                    command.ExecuteNonQuery();
                }
                else
                {
                    command.CommandText = $"UPDATE users SET name = '{user.Name}', is_authorisation = '{user.IsAuthorisation}', second_name = '{user.SecondName}', email = '{user.Email}', password = '{user.Password}', session = '{user.Session}' WHERE user_id = '{user.UserId}'";
                    command.ExecuteNonQuery();
                    
                }
            }
        }

        public void Deserialize(ServerContext context)
        {
            var command = new SqlCommand("")
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
                    UserId = userReader.GetString("user_id"),
                    Name = userReader.GetString("name"),
                    SecondName = userReader.GetString("second_name"),
                    Email = userReader.GetString("email"),
                    Password = userReader.GetString("password"),
                    Session = userReader.GetString("session")
                };
                _users.Add(user.UserId, user);
                emails.Add(user.Email, user.UserId);
            }
            userReader.Close();
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
            _users.Add(user.UserId, user);
            emails.Add(user.Email, user.UserId);
        }
    }
}