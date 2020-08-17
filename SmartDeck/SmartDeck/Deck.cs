using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace SmartDeck
{
    class Deck
    {
        public LinkedList<Card> Contents { get; private set; }
        public Card Top { get; private set; }
        public Card Bottom { get; private set; }
        public int TotalDeckSize { get; private set; }
        public int CurrentDeckSize { get; private set; }

        public Deck(int numberOfDecks)
        {
            
        }

        public void Add(int numberOfDecks)
        {

        }
    }
}
