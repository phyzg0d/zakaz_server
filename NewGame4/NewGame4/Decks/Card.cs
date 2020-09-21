namespace NewGame4.Decks
{
    public class Card
    {
        public string Id;
        public string Deck;
        public string Image;
        public string Shirt;

        public Card()
        {
        }

        public Card(string id,string deck,string image,string shirt)
        {
            Id = id;
            Deck = deck;
            Image = image;
            Shirt = shirt;
        }
    }
}