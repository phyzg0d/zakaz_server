using Microsoft.AspNetCore.Http;
using NewGame4.Commands.Base;
using NewGame4.Commands.SignIn_SignOut;

namespace NewGame4.Commands.GameProcess
{
    public class CreateNewGameCommand : ExecuteCommand
    {
        public CreateNewGameCommand(IFormCollection data, HttpResponse response) : base(response)
        {
            NameCommand = nameof(UserSignInCommand);
           
            
        }      
        public override void Execute(ServerContext context)
        {
            Send();
        }
    }
}