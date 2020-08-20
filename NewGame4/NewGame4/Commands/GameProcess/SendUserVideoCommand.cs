using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using NewGame4.Commands.Base;

namespace NewGame4.Commands.GameProcess
{
    public class SendUserVideoCommand : ExecuteCommand
    {
        private byte[] _video { get; }
        public SendUserVideoCommand(IFormCollection data, HttpResponse response, HttpRequest request) : base(response, request)
        {
            NameCommand = nameof(SendUserVideoCommand);
            _video = Convert.FromBase64String(data["video"]);
            
            UserParams.Add("video", string.Empty);
        }

        public override void Execute(ServerContext context)
        {
            context.UserModel.CurrentSplash = _video;
        }
    }
}