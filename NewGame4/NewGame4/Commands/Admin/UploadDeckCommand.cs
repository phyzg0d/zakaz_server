using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using fastJSON;
using Microsoft.AspNetCore.Http;
using NewGame4.Commands.Base;
using NewGame4.Decks;
using NewGame4.Utilities;
using Newtonsoft.Json;

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
                var command = new SqlCommand("")
                {
                    Connection = context.BdConnection.Connection
                };
                
                command.CommandText = $"INSERT INTO cards(card_name, shirt, card_image, deck) VALUES('{card.Key}', '{shirt}', '{ card.Value}', '{deckName}')";
                command.ExecuteNonQuery();
                
                // command.CommandText = $"UPDATE cards SET card_name = @card_name, shirt = @shirt, card_image = @card_image, deck = @deck";
                // command.Parameters.Add("@card_name", SqlDbType.Text).Value = card.Key;
                // command.Parameters.Add("@shirt", SqlDbType.Text).Value = shirt;
                // command.Parameters.Add("@card_image", SqlDbType.Text).Value = card.Value;
                // command.Parameters.Add("@deck", SqlDbType.NVarChar).Value = cards.ToString();
                    
                using (StreamWriter sw = new StreamWriter($"Resources/Decks/{deckName}.json", false, System.Text.Encoding.Default))
                {
                    string json = JsonConvert.SerializeObject(_cards, Formatting.Indented);
                    sw.WriteLine(json);
                }

                try
                {
                    UserParams.Add("image",card.Value);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                
                deck.Cards.Add(new KeyValuePair<string, Card>(card.Key,new Card(card.Key,deckName,card.Value.ToString(),shirt)));
            }
            
            context.DeckModel.Add(deck);
            Send();
        }
    }
}