using System.Collections.Generic;

namespace NewGame4.Decks
{
    public class DeckModel
    {
        private Dictionary<string, IDeckUnitModel> _decks = new Dictionary<string, IDeckUnitModel>();

        public void Add(IDeckUnitModel deck)
        {
            _decks.Add(deck.DeckName, deck);
        }

        public IDeckUnitModel Get(string deckName)
        {
            return _decks[deckName];
        }
    }
}