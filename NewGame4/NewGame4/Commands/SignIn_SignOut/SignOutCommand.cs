using System.Collections.Generic;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Http;
using NewGame4.Commands.Base;

namespace NewGame4.Commands.SignIn_SignOut
{
    public class SignOutCommand : ExecuteCommand
    {
        private bool _authorisation;
        
        private string _name { get; }

        public SignOutCommand(IFormCollection data, HttpResponse response, HttpRequest request) : base(response, request)
        {
            NameCommand = nameof(UserSignInCommand);
            _name = data["name"];
            
            UserParams.Add("Name", string.Empty);
        }

        public override void Execute(ServerContext context)
        {
            Response.StatusCode = 200;
            if (_authorisation)
            {
               
            }
            else
            {
                UserParams["Error"] = "Authorisation Error";
            }
            Send();
        }

    }
}