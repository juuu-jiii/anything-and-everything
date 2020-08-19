using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDeck
{
    class Program
    {
        static void Main(string[] args)
        {
            // User input loop
            Deck cardDeck = new Deck(1);
            string userInput = null;
            bool hasQuit = false;

            while (!hasQuit)
            {
                Console.WriteLine("What would you like to do?");
                userInput = Console.ReadLine();

                switch (userInput.Trim().ToLower())
                {
                    case ("add"):
                        {
                            Console.WriteLine("Input number of decks to add: ");

                            // Assume valid data is always entered for now.
                            int numberOfDecks = int.Parse(Console.ReadLine());
                            cardDeck.Add(numberOfDecks);
                            Console.WriteLine("{0} deck(s) successfully added.\n" +
                                "There are now {1} cards in the deck.", numberOfDecks, cardDeck.Contents.Count);
                            break;
                        }
                    case ("shuffle"):
                        {
                            int numberOfShuffles = cardDeck.Shuffle();
                            Console.WriteLine("Deck shuffled. {0} shuffles performed.\n" +
                                "There are now {1} cards in the deck.", numberOfShuffles, cardDeck.Contents.Count);
                            break;
                        }
                    case ("clear"):
                        {
                            cardDeck.ClearDeck();
                            Console.WriteLine("Deck cleared. There are now {0} cards in the deck.", cardDeck.Contents.Count);
                            break;
                        }
                    case ("print"):
                        {
                            Console.WriteLine("Deck contents:");
                            cardDeck.PrintDeck();
                            break;
                        }
                    case ("quit"):
                        {
                            hasQuit = true;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("This shouldn't happen.");
                            break;
                        }
                }
            }

            Console.WriteLine("Exiting application...");

            Console.ReadKey();
        }
    }
}
