using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using NewGame4.Commands.Base;

namespace NewGame4.Commands.SignIn_SignOut
{
    public class UserSignInCommand : ExecuteCommand
    {
        private string _email { get; }
        private string _password { get; }

        public UserSignInCommand(IFormCollection data, HttpResponse response) : base(response)
        {
            NameCommand = nameof(UserSignInCommand);
            _email = data["email"];
            _password = data["password"];
            
            UserParams.Add("Email", string.Empty);
            UserParams.Add("Password", string.Empty);
        }
        
        public override void Execute(ServerContext context)
        {
            var command = new MySqlCommand("")
            {
                Connection = context.BdConnection.Connection
            };
            
            command.CommandText = $"SELECT * FROM users WHERE Email ='{_email}'";
            
            var signInEmailCheck = command.ExecuteReader();
            signInEmailCheck.Read();
            if ("Email" != _email)
            {
                Response.WriteAsync("Incorrect email");
                signInEmailCheck.Close();
                return;
            }
            signInEmailCheck.Close();

            command.CommandText = $"SELECT * FROM users WHERE Password ='{_password}'";
            var  signInPasswordCheck = command.ExecuteReader();
            signInPasswordCheck.Read();
            if ("Password" != _password)
            {
                Response.WriteAsync("Incorrect password");
                signInPasswordCheck.Close();
                return;
            }
            signInPasswordCheck.Close();

            Response.WriteAsync("Sign in");
            Send();
        }
    }
}
