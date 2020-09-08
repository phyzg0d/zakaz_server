using System.Collections.Generic;

namespace NewGame4.Decks
{
    public class DeckUnitModel : IDeckUnitModel
    {
        public string Name { get; set; }
        public string Shirt { get; set; }
        public IDictionary<string, object> Cards { get; set; }
    }
}