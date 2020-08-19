using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace NewGame4.Sessions
{
    public class SessionModel
    {
        private Dictionary<string, ISessionUnitModel> _users = new Dictionary<string, ISessionUnitModel>();
        public List<string> sessions = new List<string>();
        public void Serialize(ServerContext context)
        {
            foreach (var user in _users.Values)
            {
                var command = new SqlCommand("")
                {
                    Connection = context.BdConnection.Connection
                };
                
                if (user.is_new)
                {
                    user.is_new = false;
                    command.CommandText = $"INSERT INTO users(session) VALUES('{user.session}')";
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
            var command = new SqlCommand("")
            {
                Connection = context.BdConnection.Connection
            };
            
            command.CommandText = $"SELECT * FROM users";
            var sessionReader = command.ExecuteReader();
            while (sessionReader.Read())
            {
                var user = new SessionUnitModel()
                {
                    session = sessionReader.GetString("session"),
                };
                _users.Add(user.session, user);
                sessions.Add(user.session);
            }
            sessionReader.Close();
        }

        public ISessionUnitModel Get(string id)
        {
            return _users[id];
        }

        public bool Contains(string id)
        {
            return _users.ContainsKey(id);
        }

        public void Add(string id, ISessionUnitModel user)
        {
            _users.Add(user.second_name, user);
            sessions.Add(user.session);
        }
    }
}