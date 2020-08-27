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
        // IndexedLL<T> holds T-type objects, stored in Node<T> objects.

        public IndexedLinkedList<Card> Contents { get; private set; }
        public int TotalDeckSize { get; private set; }
        private Random rng;

        // Constructor that initialises Contents with the specified number of card decks.
        public Deck(int numberOfDecks)
        {
            Contents = new IndexedLinkedList<Card>();
            TotalDeckSize = 0;
            rng = new Random();

            Add(numberOfDecks);
        }

        /// <summary>
        /// Helper method that joins the main deck and additional specified subdeck together.
        /// </summary>
        /// <param name="subDeck">
        /// The subdeck to be added to the main deck.
        /// </param>
        private void JoinDeck(IndexedLinkedList<Card> subDeck)
        {
            // Check to prevent exceptions.
            if (TotalDeckSize == 0)
            {
                Contents.Head = subDeck.Head;
                Contents.Tail = subDeck.Tail;

                Contents.Count += subDeck.Count;
                TotalDeckSize += subDeck.Count;
            }
            else
            {
                Contents.Tail.Next = subDeck.Head;
                subDeck.Head.Previous = Contents.Tail;
                Contents.Tail = subDeck.Tail;

                Contents.Count += subDeck.Count;
                TotalDeckSize += subDeck.Count;
            }
        }

        /// <summary>
        /// Helper method that creates and returns a single deck of cards.
        /// </summary>
        /// <returns>
        /// Returns the deck of cards as an IndexedLinkedList<Node<Card>>.
        /// </returns>
        private IndexedLinkedList<Card> CreateDeck()
        {
            IndexedLinkedList<Card> singleDeck = new IndexedLinkedList<Card>();

            for (int i = 2; i < (int)Ranks.Ace + 1; i++)
            {
                for (int j = 0; j < (int)Suits.Spades + 1; j++)
                {
                    singleDeck.AddLast(new Card((Suits)j, (Ranks)i));
                }
            }

            return singleDeck;
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
        /// Grabs a portion of the deck spanning a point somewhere in between to 
        ///     the end, reinserting it randomly. This will be called repeatedly to
        ///     shuffle the deck's contents.
        /// </summary>
        public void Move()
        {
            // Need to ensure index is a whole number
            // Try using the formula: splitting index = decksize * rng.Next(3, 6) / 10 
            // Note the use of integer division to ensure that invalid indices are not accessed.
            int splitAtIndex = Contents.Count * rng.Next(3, 6) / 10;

            Node<Card> splitAt = Contents[splitAtIndex];

            // Insertion point is between first and last card of the remaining deck.
            Node<Card> joinAt = Contents[rng.Next(0, splitAtIndex - 2)];

            // Rearrange the nodes' pointers here.

            // Check to see if insertion point is at the head.
            if (joinAt.Previous == null)
            {
                splitAt.Previous.Next = null;
                Contents.Tail.Next = joinAt.Next;
                Contents.Tail.Next.Previous = Contents.Tail;

                joinAt.Next = splitAt;
                Contents.Tail = splitAt.Previous;

                splitAt.Previous = joinAt;

                Contents.Head = splitAt;
            }
            else
            {
                splitAt.Previous.Next = null;
                Contents.Tail.Next = joinAt.Next;
                Contents.Tail.Next.Previous = Contents.Tail;

                joinAt.Next = splitAt;
                Contents.Tail = splitAt.Previous;

                splitAt.Previous = joinAt;
            }
        }

        public int Shuffle()
        {
            int numberOfShuffles = rng.Next(50, 101);

            // for loop, random # times
            for (int i = 0; i < numberOfShuffles; i++)
            {
                Move();
            }

            return numberOfShuffles;
        }

        /// <summary>
        /// Loops through and prints the current contents of the Indexed Linked List.
        /// </summary>
        public void PrintDeck()
        {
            for (int i = 0; i < Contents.Count; i++)
            {
                Console.WriteLine(Contents[i].Data.ToString());
            }
        }

        /// <summary>
        /// Clears the deck.
        /// </summary>
        public void ClearDeck()
        {
            Contents.Clear();
        }
    }
}
