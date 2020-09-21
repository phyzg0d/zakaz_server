using System;
using System.Collections.Generic;
using System.IO;
using fastJSON;
using Microsoft.AspNetCore.Http;
using NewGame4.Commands.Base;
using NewGame4.Decks;
using NewGame4.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using JsonSerializer = Json.Net.JsonSerializer;

namespace NewGame4.Commands.Admin
{
    public class UploadDeckCommand : ExecuteCommand
    {
        private Dictionary<string, object> _cards { get; }

        public UploadDeckCommand(IFormCollection data, HttpResponse response, HttpRequest request) : base(response,
            request)
        {
            NameCommand = nameof(UploadDeckCommand);
            _cards = (Dictionary<string, object>) JSON.Parse(data["json"]);
        }

        public override void Execute(ServerContext context)
        {
            var deckName = _cards.GetString("name");
            var shirt = _cards.GetString("shirt");
            var cards = _cards.GetNode("cards");

            FileHelper.CreateDirectory($"{deckName}");
            FileHelper.CreateDirectory("Resources/Decks");

            var deck = new DeckUnitModel(deckName, new Dictionary<string, Card>());

            foreach (var card in cards)
            {
                using (StreamWriter sw = new StreamWriter($"Resources/Decks/{deckName}.json", false,
                    System.Text.Encoding.Default))
                {
                    string jsonToFile = JsonConvert.SerializeObject(_cards, Formatting.Indented);
                    sw.WriteLine(jsonToFile);
                }

                UserParams.Add("image", card.Value);
                deck.Cards.Add(new KeyValuePair<string, Card>(card.Key,
                    new Card(card.Key, deckName, card.Value.ToString(), shirt)));

                context.DeckModel.Add(deck);

                const string readPath = "Resources/Decks/GameRulesDecks.json";

                using (StreamReader streamReader = new StreamReader(readPath))
                {
                    string jsonString = streamReader.ReadToEnd();
                    var buffer = (Dictionary<string, object>) JSON.Parse(jsonString);
                    var neededDecks = buffer.GetStingArray("name");

                    foreach (var nameDeck in neededDecks)
                    {
                        if (neededDecks.Contains(deckName))
                        {
                            var neededDeck = context.DeckModel.Get(nameDeck);
                            if (neededDeck.DeckName == deckName)
                            {
                                Dictionary<string, string> neededCard = new Dictionary<string, string>()
                                {
                                    {$"{neededDeck.DeckName}", $"{card}"}
                                };

                                var json = JsonConvert.SerializeObject(neededCard, Formatting.Indented);
                                UserParams.Add("requestCards", json);
                            }    
                        }
                    }
                }
            }

            Send();
        }
    }
}