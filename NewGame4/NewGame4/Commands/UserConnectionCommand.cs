using System;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using NewGame4.Commands.Base;

namespace NewGame4.Commands
{
    public class UserConnectionCommand : ExecuteCommand
    {
        private string _id { get; }
        private string _email { get; }
        public UserConnectionCommand(IFormCollection data, HttpResponse response, HttpRequest request) : base(response, request)
        {
            NameCommand = nameof(UserConnectionCommand);
            _id = data["userId"];
            _email = data["session"];
        }

        public override void Execute(ServerContext context)
        {
            var user = context.UserModel.Get(_id);
            if (user.SecondName == _id)
            {
                Console.WriteLine("authorisation");
                UserParams.Add("authorisation", true);            
            }
            Send();
        }
    }
}