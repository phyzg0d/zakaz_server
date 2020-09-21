using System.Collections.Generic;

namespace NewGame4.Decks
{
    public interface IDeckUnitModel
    {
        string DeckName { get; set; }
        IDictionary<string, Card> Cards { get; }
    }
}