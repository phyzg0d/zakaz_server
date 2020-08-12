using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using NewGame4.Commands.Base;
using NewGame4.Commands.GameElements;
using NewGame4.Commands.GameProcess;
using NewGame4.Commands.GamingFields;
using NewGame4.Commands.Registration;
using NewGame4.Commands.SignIn_SignOut;
using NewGame4.Commands.Subscription;
using Ubiety.Dns.Core;

namespace NewGame4.Utilities
{
    public class Factory
    {
        public Dictionary<string, Func<IFormCollection, HttpResponse, IExecuteCommand>> CommandFactory;
        private ServerContext _context;

        public Factory(ServerContext context)
        {
            _context = context;
            CommandFactory = new Dictionary<string, Func<IFormCollection, HttpResponse, IExecuteCommand>>
            {
                {nameof(UserSignInCommand), (form, response) => new UserSignInCommand(form, response)},
                {nameof(SignOutCommand), (form, response) => new SignOutCommand(form, response)},
                {nameof(LeaderBuySubscriptionCommand), (form, response) => new LeaderBuySubscriptionCommand(form, response)},
                {nameof(RegistrationCommand), (form, response) => new RegistrationCommand(form, response)},
                {nameof(GamingFieldSectionNowPlayingCommand), (form, response) => new GamingFieldSectionNowPlayingCommand(form, response)},
                {nameof(LeaderDistributeTreasuryCommand), (form, response) => new LeaderDistributeTreasuryCommand(form, response)},
                {nameof(CreateNewGameCommand), (form, response) => new CreateNewGameCommand(form, response)},
                {nameof(BagFillingCommand), (form, response) => new BagFillingCommand(form, response)},
                {nameof(FormingGiftOfTheUniverseCommand), (form, response) => new FormingGiftOfTheUniverseCommand(form, response)},
                {nameof(FormingGamingSectorCommand), (form, response) => new FormingGamingSectorCommand(form, response)},
                {nameof(FormingCoffersCommand), (form, response) => new FormingCoffersCommand(form, response)},
                {nameof(DecomposedElementsCommand), (form, response) => new DecomposedElementsCommand(form, response)},
            };
        }
    }
}