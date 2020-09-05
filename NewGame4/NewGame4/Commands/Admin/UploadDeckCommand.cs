using System;
using System.Collections.Generic;
using fastJSON;
using Microsoft.AspNetCore.Http;
using NewGame4.Commands.Base;
using NewGame4.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NewGame4.Commands.Admin
{
    public class UploadDeckCommand : ExecuteCommand
    {
        private string _name { get;}
        private string _shirt { get;}
        private Dictionary<string, object> _cards { get;}
        
        public UploadDeckCommand(IFormCollection data, HttpResponse response, HttpRequest request) : base(response, request)
        {
            NameCommand = nameof(UploadDeckCommand);
            _cards = (Dictionary<string, object>) JSON.Parse(data["json"]);
        }

        public override void Execute(ServerContext context)
        {
            var name = _cards.GetString("name");
            var shirt = _cards.GetString("shirt");
            var cards = _cards.GetNode("cards");
            
            foreach (var card in cards)
            {
                Console.WriteLine(card.Key);
            }
            
            Send();
        }
    }
}