using System.Collections.Generic;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Http;
using NewGame4.Commands.Base;
using NewGame4.Commands.SignIn_SignOut;

namespace NewGame4.Commands.GameProcess
{
    public class CreateNewGameCommand : ExecuteCommand
    {
        private string _password { get; }
        private string _email { get; }

        public CreateNewGameCommand(IFormCollection data, HttpResponse response) : base(response)
        {
            NameCommand = nameof(UserSignInCommand);
            _password = data["password"];
            _email = data["email"];
            
            UserParams.Add("Password", string.Empty);
            UserParams.Add("Email", string.Empty);
        }      
        public override void Execute(ServerContext context)
        {
            Send();
        }

    }
}