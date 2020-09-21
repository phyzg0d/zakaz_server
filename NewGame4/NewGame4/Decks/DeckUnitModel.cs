using System.Collections.Generic;

namespace NewGame4.Decks
{
    public class DeckUnitModel : IDeckUnitModel
    {
        public string DeckName { get; set; }
        public IDictionary<string, Card> Cards { get; set; }

        public DeckUnitModel(string deckName,IDictionary<string,Card> cards)
        {
            DeckName = deckName;
            Cards = cards;
        }
    }
}