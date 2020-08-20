using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using NewGame4.Commands.Base;

namespace NewGame4.Commands.GameProcess
{
    public class SendUserVideoCommand : ExecuteCommand
    {
        private byte[] _video { get; }
        private int _x;
        private int _y;
        public SendUserVideoCommand(IFormCollection data, HttpResponse response, HttpRequest request) : base(response, request)
        {
            NameCommand = nameof(SendUserVideoCommand);
            _video = Convert.FromBase64String(data["video"]);
            _x = Convert.ToInt32(data["x"]);
            _y = Convert.ToInt32(data["y"]);
        }

        public override void Execute(ServerContext context)
        {
            context.UserModel.CurrentSplash = _video;
            context.UserModel.X = _x;
            context.UserModel.Y = _y;
            UserParams.Add("video",Convert.ToBase64String(context.UserModel.CurrentSplash));
            UserParams.Add("x",context.UserModel.X);
            UserParams.Add("y",context.UserModel.Y);
            Send();
        }
    }
}