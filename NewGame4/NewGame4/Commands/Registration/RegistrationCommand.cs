using System;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using NewGame4.Commands.Base;

namespace NewGame4.Commands.Registration
{
    public class RegistrationCommand : ExecuteCommand
    {
        private string _id { get; }
        private string _name { get; }
        private string _secondName { get; }
        private string _password { get; }
        private string _email { get; }
        private string _session { get; }

        public RegistrationCommand(IFormCollection data, HttpResponse response, HttpRequest request) : base(response, request)
        {
            NameCommand = nameof(RegistrationCommand);
            _id = data["id"];
            _name = data["name"];
            _secondName = data["secondName"];
            _password = data["password"];
            _email = data["email"];
            _session = data["session"];

            UserParams.Add("Id", string.Empty);
            // UserParams.Add("Name", string.Empty);
            // UserParams.Add("SecondName", string.Empty);
            // UserParams.Add("Password", string.Empty);
            // UserParams.Add("Email", string.Empty);
        }

        public override void Execute(ServerContext context)
        {
            
            // if (Request.Cookies.ContainsKey("Name"))
            // {
            //     Console.WriteLine(Request.Cookies["Name"]);
            // }
            // else
            // {
            //     Response.Cookies.Append("Name", _name);
            // }
            
            var command = new MySqlCommand("")
            {
                Connection = context.BdConnection.Connection
            };
            
            command.CommandText = $"SELECT * FROM users WHERE Email ='{_email}'";
            var emailCheck = command.ExecuteReader();
            emailCheck.Read();
            if (emailCheck.HasRows)
            {
                UserParams["error"] = true;
                UserParams["error_text"] = "Email exist";
                // Response.Cookies.Append("Name", _name);
                Send();
                emailCheck.Close();
                return;
            }
            emailCheck.Close();
            // var cookieOptions = new CookieOptions()
            // {
            //     HttpOnly = true,
            //     SameSite = SameSiteMode.Lax,
            //     Domain = "localhost:49752"
            // };
            // Response.Cookies.Append("Name", _name, cookieOptions);
            
            command.CommandText = $"INSERT INTO users (Name, SecondName, Password, Email, Session) VALUES( '{_name}', '{_secondName}', '{_password}', '{_email}', '{_session}')";
            command.ExecuteNonQuery();
            
            Send();
        }
    }
}