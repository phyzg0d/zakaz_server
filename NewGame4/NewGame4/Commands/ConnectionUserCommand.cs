using System;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using NewGame4.Commands.Base;

namespace NewGame4.Commands
{
    public class ConnectionUserCommand : ExecuteCommand
    {
        private string _id { get; }
        private string _email { get; }
        public ConnectionUserCommand(IFormCollection data, HttpResponse response, HttpRequest request) : base(response, request)
        {
            NameCommand = nameof(ConnectionUserCommand);
            _id = data["id"];
            _email = data["email"];
        }

        public override void Execute(ServerContext context)
        {
            var command = new MySqlCommand("")
            {
                Connection = context.BdConnection.Connection
            };
            
            command.CommandText = $"SELECT * FROM users WHERE Email ='{_email}'";
            var emailCheck = command.ExecuteReader();
            emailCheck.Read();
            var session = emailCheck.GetString("Session");
            
            if (string.IsNullOrEmpty(session)) return;
            if (string.IsNullOrEmpty(_id)) return;
            if (session == _id)
            {
                Console.WriteLine("authorisation");
                UserParams.Add("authorisation", true);            
            }
        }
    }
}