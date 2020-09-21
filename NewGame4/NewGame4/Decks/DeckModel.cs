using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace NewGame4.Decks
{
    public class DeckModel
    {
        private Dictionary<string, IDeckUnitModel> _decks = new Dictionary<string, IDeckUnitModel>();

        public void Add(IDeckUnitModel deck)
        {
            _decks.Add(deck.DeckName, deck);
        }

        public void Serialize(ServerContext context)
        {
        }

        public void Deserialize(ServerContext context)
        {
            var command = new SqlCommand("")
            {
                Connection = context.BdConnection.Connection
            };

            command.CommandText = $"SELECT * FROM cards";
            var deckReader = command.ExecuteReader();
            while (deckReader.Read())
            {
                if (!_decks.ContainsKey(deckReader.GetString("deck")))
                {
                    var cards = new Dictionary<string, Card>();
                    cards.Add(deckReader.GetString("card_name"), new Card(deckReader.GetString("card_name"),
                        deckReader.GetString("deck"),
                        deckReader.GetString("card_image"), deckReader.GetString("shirt")));
                    _decks.Add(deckReader.GetString("deck"), new DeckUnitModel(deckReader.GetString("deck"), cards));
                }
                else
                {
                    _decks[deckReader.GetString("deck")].Cards.Add(new KeyValuePair<string, Card>(
                        deckReader.GetString("card_name"), new Card(deckReader.GetString("card_name"),
                            deckReader.GetString("deck"), deckReader.GetString("card_image"),
                            deckReader.GetString("shirt"))));
                }
            }

            deckReader.Close();
        }

        public Dictionary<string, IDeckUnitModel> Get()
        {
            return _decks;
        }
    }
}