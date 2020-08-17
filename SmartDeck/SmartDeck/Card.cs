using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum Suits
{
    Diamonds,
    Clubs,
    Hearts,
    Spades
}

enum Ranks
{
    Ace = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,
    Nine = 9,
    Ten = 10,
    Jack = 11,
    Queen = 12,
    King = 13
}

enum CountingValues
{
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,
    Nine = 9,
    Ten, Jack, Queen, King = 10,
    Ace = 15,
    Joker = 20
}

namespace SmartDeck
{
    class Card
    {
        public string Suit { get; }
        public string Rank { get; }
        public int Value { get; }
        public Card Next { get; set; }
        public Card Previous { get; set; }

        public Card(Suits suit, Ranks rank)
        {
            Suit = suit.ToString();
            Rank = rank.ToString();
            Value = (int)rank;
            Next = null;
            Previous = null;
        }

        /// <summary>
        /// Concatenates and returns the rank and suit of the card.
        /// </summary>
        /// <returns>
        /// Returns the concatenated values as a string.
        /// </returns>
        public override string ToString()
        {
            return Enum.TryParse<Ranks>(Rank, out) + Suit[0];
        }
    }
}

/* Problems:
 * Low and High Ace - need to be able to distinguish between the two; is an enum really the best way?
 *      (currently solved by providing two different names, LowAce + HighAce)
 * When counting score in rummy, the card values change e.g. in sequences J=11, Q=12, K=13, A=14/1, but when
 *      counting, all face cards = 10.
 *      (currently solved by creating a separate enum for scoring, and assigning 10/J/Q/K to the same value.
 * Problem when switching back and forth between string and numerical values. Take a look at ToString() for context.
 */