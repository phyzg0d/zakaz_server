using System;
using Microsoft.AspNetCore.Http;
using NewGame4.Commands.Base;
using JsonSerializer = ServiceStack.Text.JsonSerializer;

namespace NewGame4.Commands
{
    public class UserConnectionCommand : ExecuteCommand
    {
        private string _id { get; }
        private string _session { get; }
        public UserConnectionCommand(IFormCollection data, HttpResponse response, HttpRequest request) : base(response, request)
        {
            NameCommand = nameof(UserConnectionCommand);
            _id = data["userId"];
            _session = data["session"];
        }

        public override void Execute(ServerContext context)
        {
            if (context.UserModel.Contains(_id))
            {
                var deckModel = context.DeckModel.Get();
                var json = JsonSerializer.SerializeToString(deckModel);
                UserParams.Add("card_output", json);
                
                var user = context.UserModel.Get(_id);
                if (user.Session == _session)
                {
                    Console.WriteLine("authorisation");
                    UserParams.Add("authorisation", true);            
                }
            }
            else
            {
                UserParams["authorisation"] = false;
            }
            Send();
        }
    }
}