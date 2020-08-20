using System;
using Microsoft.AspNetCore.Http;
using NewGame4.Commands.Base;
using NewGame4.Utilities;

namespace NewGame4.Commands.GameProcess
{
    public class CreateNewGameCommand : ExecuteCommand
    {
        private string _countOfPlayers { get; }
        public CreateNewGameCommand(IFormCollection data, HttpResponse response, HttpRequest request) : base(response, request)
        {
            NameCommand = nameof(CreateNewGameCommand);
            _countOfPlayers = data["countOfPlayers"];
        }      
        public override void Execute(ServerContext context)
        {
            Console.WriteLine(UserParams.GetString("countOfPlayers"));
            Console.WriteLine(NameCommand);
            Send();
        }
    }
}