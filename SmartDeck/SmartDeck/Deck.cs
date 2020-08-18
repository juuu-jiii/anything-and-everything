using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace SmartDeck
{
    class Deck
    {
        // Create Card, store in Node<T>, store in IndexedLinkedList<T>

        public IndexedLinkedList<Node<Card>> Contents { get; private set; }
        public int TotalDeckSize { get; private set; }
        private Random rng;

        // Constructor that initialises Contents with the specified number of card decks.
        public Deck(int numberOfDecks)
        {
            Contents = new IndexedLinkedList<Node<Card>>();
            TotalDeckSize = 0;
            rng = new Random();

            Add(numberOfDecks);
        }

        /// <summary>
        /// Adds a specified number of decks to the current, main deck.
        /// </summary>
        /// <param name="numberOfDecks">
        /// The number of decks to add.
        /// </param>
        public void Add(int numberOfDecks)
        {
            for (int i = 0; i < numberOfDecks; i++)
            {
                JoinDeck(CreateDeck());
            }
        }

        /// <summary>
        /// Helper method that joins the main deck and additional specified subdeck together.
        /// </summary>
        /// <param name="subDeck">
        /// The subdeck to be added to the main deck.
        /// </param>
        private void JoinDeck(IndexedLinkedList<Node<Card>> subDeck)
        {
            Contents.Tail.Next = subDeck.Head;
            subDeck.Head.Previous = Contents.Tail;
            Contents.Tail = subDeck.Tail;

            Contents.Count += subDeck.Count;
            TotalDeckSize += subDeck.Count;
        }

        /// <summary>
        /// Helper method that creates and returns a single deck of cards.
        /// </summary>
        /// <returns>
        /// Returns the deck of cards as an IndexedLinkedList<Node<Card>>.
        /// </returns>
        private IndexedLinkedList<Node<Card>> CreateDeck()
        {
            IndexedLinkedList<Node<Card>> singleDeck = new IndexedLinkedList<Node<Card>>();

            for (int i = 0; i < (int)Ranks.Ace - 1; i++)
            {
                for (int j = 0; j < (int)Suits.Spades + 1; j++)
                {
                    singleDeck.AddLast(new Node<Card>(new Card((Suits)j, (Ranks)i)));
                }
            }

            return singleDeck;
        }

        public void Shuffle()
        {
            // Need to ensure index is a whole number
            // Try using the formula: splitting index = decksize * rng.Next(3, 8) / 10 
            // Note the use of integer division to ensure that invalid indices are not accessed.
        }
    }
}
