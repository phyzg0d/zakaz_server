using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace NewGame4.Commands.Base
{
    public abstract class ExecuteCommand : IExecuteCommand
    {
        public string NameCommand { get; set; }
        protected Dictionary<string, string> UserParams = new Dictionary<string, string>();
        protected HttpResponse Response { get; }


        protected ExecuteCommand(HttpResponse response)
        {
            Response = response;
            UserParams.Add("Error", string.Empty);
            Dictionary<string, StringValue> headers = new Dictionary<string, StringValue>();
            Response.OnStarting(() =>
            {
                Response.Headers.Add("Access-Control-Allow-Credentials", "true");
                Response.Headers.Add("Access-Control-Allow-Headers", "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time");
                Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
                Response.Headers.Add("Access-Control-Allow-Origin", "*");
                return Task.FromResult(0);
            });
            
            Response.StatusCode = 200;
        }
        
        protected async void Send()
        {
            Console.WriteLine(NameCommand);
            var sendObject = JsonConvert.SerializeObject(UserParams);
            await Response.WriteAsync(sendObject);
        }
        
        public abstract void Execute(ServerContext context);
    }
}