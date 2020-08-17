using System;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using NewGame4.Commands.Base;
using NewGame4.Users;

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
            if (context.UserModel.Emails.Contains(_email))
            {
                UserParams["error"] = true;
                UserParams["error_text"] = "Email exist";
            }
            else
            {
                var user = new UserUnitModel()
                {
                    Name = _name,
                    SecondName = _secondName,
                    Email = _email,
                    Password = _password,
                    Session = _session,
                    IsNew = true
                };
                context.UserModel.Add(_email, user);
            }
            
            Send();
        }
    }
}