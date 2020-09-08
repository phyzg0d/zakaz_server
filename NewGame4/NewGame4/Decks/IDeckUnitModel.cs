using System.Collections.Generic;

namespace NewGame4.Decks
{
    public interface IDeckUnitModel
    {
        string Name { get; }
        string Shirt { get; }
        IDictionary<string, object> Cards { get; }
    }
}