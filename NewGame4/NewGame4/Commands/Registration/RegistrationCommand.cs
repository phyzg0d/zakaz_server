using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using NewGame4.Commands.Base;
using NewGame4.Commands.SignIn_SignOut;

namespace NewGame4.Commands.Registration
{
    public class RegistrationCommand : ExecuteCommand
    {
        private string _name { get; }
        private string _secondName { get; }
        private string _password { get; }
        private string _email { get; }

        public RegistrationCommand(IFormCollection data, HttpResponse response) : base(response)
        {
            NameCommand = nameof(UserSignInCommand);
            _name = data["name"];
            _secondName = data["secondName"];
            _password = data["password"];
            _email = data["email"];

            UserParams.Add("Name", string.Empty);
            UserParams.Add("SecondName", string.Empty);
            UserParams.Add("Password", string.Empty);
            UserParams.Add("Email", string.Empty);
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
            if (emailCheck.HasRows)
            {
                UserParams["error"] = true;
                UserParams["error_text"] = "Email exist";
                Send();
                emailCheck.Close();
                return;
            }
            emailCheck.Close();

            command.CommandText = $"INSERT INTO users (Name, SecondName, Password, Email) VALUES( '{_name}', '{_secondName}', '{_password}', '{_email}')";
            command.ExecuteNonQuery();

            Send();
        }
    }
}