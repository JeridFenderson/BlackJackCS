using System;
using System.Collections.Generic;

namespace BlackJackCS
{
    class Program
    {
        class Card
        //Class that builds each card individually and assigns an appropriate number value to the card face
        {
            public string Rank { get; set; }
            public string Suit { get; set; }

            public int Value()
            {
                var cardValue = 0;

                switch (Rank)
                {
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                        cardValue = int.Parse(Rank);
                        break;
                    case "10":
                    case "Jack":
                    case "Queen":
                    case "King":
                        cardValue = 10;
                        break;
                    case "Ace":
                        cardValue = 11;
                        break;
                }
                return cardValue;

            }

        }
        static List<Card> Build(List<Card> deckToBeBuilt)
        {
            var listOfSuits = new List<string>()
                 {
                    " of Spades", " of Clubs", " of Hearts", " of Diamonds"
                  };
            var listOfRanks = new List<string>()
                 {
                    "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King"
                  };
            foreach (var suit in listOfSuits)
            {
                foreach (var rank in listOfRanks)
                {
                    var newCard = new Card()
                    {
                        Rank = rank,
                        Suit = suit,
                    };
                    deckToBeBuilt.Add(newCard);
                }
            }
            return deckToBeBuilt;
        }
        static List<Card> Shuffle(List<Card> deckToBeShuffled)
        // Method that shuffles deck on command
        {
            var lastUnmovedCardPosition = deckToBeShuffled.Count;
            var randomNumber = 0;
            var randomCardBasedOnRandomNumber = deckToBeShuffled[randomNumber];

            while (lastUnmovedCardPosition-- > 0)
            {
                randomNumber = new Random().Next(0, lastUnmovedCardPosition);

                randomCardBasedOnRandomNumber = deckToBeShuffled[randomNumber];
                deckToBeShuffled[randomNumber] = deckToBeShuffled[lastUnmovedCardPosition];
                deckToBeShuffled[lastUnmovedCardPosition] = randomCardBasedOnRandomNumber;
            }
            return deckToBeShuffled;
        }

        static string PlayAgain(string playQuestion, int roundNumber)
        // Method that takes in a string question and a round number integer and outputs a string and readLine variable
        {
            if (roundNumber == 1)
            {
                Console.WriteLine($"You've played Black Jack {roundNumber} time.");
            }
            else
            {
                Console.WriteLine($"You've played Black Jack {roundNumber} times.");
            }
            Console.WriteLine(playQuestion);
            Console.Write("Enter anything other than 'yes' to stop: ");
            var noOrSomething = Console.ReadLine();
            return noOrSomething;
        }
        static void Main(string[] args)
        {
            var roundCounter = 0;
            var prompt = "Would you like to play Black Jack?";

            while (PlayAgain(prompt, roundCounter) == "yes")
            {
                var deck = new List<Card>();
                deck = new List<Card>(Build(deck));
                foreach (var card in deck)
                {
                    Console.WriteLine(card.Rank + card.Suit + " Value of " + card.Value());

                }

                deck = new List<Card>(Shuffle(deck));
                foreach (var card in deck)
                {
                    Console.WriteLine(card.Rank + card.Suit + " Value of " + card.Value());
                }

                roundCounter++;
                if (roundCounter > 0)
                    prompt = "Would you like to play again?";
            }
        }
    }
}

