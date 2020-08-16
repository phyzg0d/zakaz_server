using System.Collections.Generic;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Http;
using NewGame4.Commands.Base;
using NewGame4.Commands.SignIn_SignOut;

namespace NewGame4.Commands.GameProcess
{
    public class LeaderDistributeTreasuryCommand : ExecuteCommand
    {        
        private string _name { get; }
        private string _secondName { get; }
        private string _gear { get; }
        private string _dice { get; }
        private string _btnThrowDice { get; }
        private string _btnShareCoffers { get; }
        private string _btnVideoSettings { get; }
        private string _btnShowTableGamer { get; }
        private string _flag { get; }
        
        public LeaderDistributeTreasuryCommand(IFormCollection data, HttpResponse response, HttpRequest request) : base(response, request)
        {
            NameCommand = nameof(UserSignInCommand);
            _name = data["name"];
            _secondName = data["secondName"];
            _gear = data["gear"];
            _dice = data["dice"];
            _btnThrowDice = data["btnThrowDice"];
            _btnShareCoffers = data["btnShareCoffers"];
            _btnVideoSettings = data["btnVideoSettings"];
            _btnShowTableGamer = data["btnShowTableGamer"];
            _flag = data["flag"];
            
            UserParams.Add("Name", string.Empty);
            UserParams.Add("SecondName", string.Empty);
            UserParams.Add("Gear", string.Empty);
            UserParams.Add("Dice", string.Empty);
            UserParams.Add("BtnThrowDice", string.Empty);
            UserParams.Add("BtnShareCoffers", string.Empty);
            UserParams.Add("BtnVideoSettings", string.Empty);
            UserParams.Add("BtnShowTableGamer", string.Empty);
            UserParams.Add("Flag", string.Empty);
            
        }      
        public override void Execute(ServerContext context)
        {
            Send();
        }

    }
}