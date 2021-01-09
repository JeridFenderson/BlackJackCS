using System;
using System.Collections.Generic;

namespace BlackJackCS
{
    class Program
    {
        class Player
        {
            public List<Card> CardsInHand { get; set; }
            public int TotalCardsInHandValue()
            {
                var cardValueCounter = 0;
                foreach (var card in CardsInHand)
                {
                    cardValueCounter += card.Value();
                }
                return cardValueCounter;
            }
            public int PlayerNumber { get; set; }
            public string PlayerName { get; set; }
            public int Wins { get; set; }
        }
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
        static void Welcome()
        {
            Console.WriteLine("\n\n");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("Welcome to Black Jack");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("\n");
        }
        static List<Player> Roster(List<Player> playersToBeAdded, int numberOfPlayers)
        {
            for (var i = 1; i <= numberOfPlayers; i++)
            {
                Console.Write($"What is Player {i}'s name? ");
                var newPlayer = new Player()
                {
                    PlayerNumber = i,
                    Wins = 0,
                    PlayerName = Console.ReadLine(),
                };
                playersToBeAdded.Add(newPlayer);
                Console.WriteLine($"Got it. Welcome {newPlayer.PlayerName}!\n");
            }
            return playersToBeAdded;
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

        static List<Card> RemoveTwoCards(List<Card> deckBeforeRemoved)
        // Method that Removes two cards from deck on command
        {
            deckBeforeRemoved.Remove(deckBeforeRemoved[0]);
            deckBeforeRemoved.Remove(deckBeforeRemoved[0]);
            return deckBeforeRemoved;
        }

        static string PlayAgain(string playQuestion, int roundNumber)
        // Method that takes in a string question and a round number integer and outputs a string and readLine variable
        {
            if (roundNumber == 0)
            {

            }
            else if (roundNumber == 1)
            {
                Console.WriteLine($"You've played Black Jack {roundNumber} time.\n");
            }
            else
            {
                Console.WriteLine($"You've played Black Jack {roundNumber} times.\n");
            }
            Console.WriteLine(playQuestion);
            Console.Write("Enter anything other than 'yes' to stop: ");
            var yes = Console.ReadLine();
            Console.WriteLine("\n");
            return yes;
        }
        static void Main(string[] args)
        {
            Welcome();
            var prompt = "What would you like to do?";
            var roundCounter = 0;
            var computer = new Player();
            var playersList = new List<Player>();
            Console.Write("How many players are there? ");
            var numOfPlayers = int.Parse(Console.ReadLine());
            while (numOfPlayers < 0 || numOfPlayers > 10)
            {
                Console.WriteLine("Min to max player size is 1 to 10: ");
                numOfPlayers = int.Parse(Console.ReadLine());
            }
            var listOfPlayers = new List<Player>();
            listOfPlayers = new List<Player>(Roster(listOfPlayers, numOfPlayers));

            while (PlayAgain(prompt, roundCounter).ToLower() == "play")
            {
                var deck = new List<Card>();
                deck = new List<Card>(Build(deck));
                deck = new List<Card>(Shuffle(deck));
                Console.WriteLine("Cards have been shuffled.");

                computer.CardsInHand = new List<Card>() { deck[0], deck[1] };
                deck = new List<Card>(RemoveTwoCards(deck));
                Console.WriteLine("Dealer's cards have been dealt.");
                Console.WriteLine($"Dealer flipped a {computer.CardsInHand[0].Rank}{computer.CardsInHand[0].Suit}\n");

                foreach (var player in listOfPlayers)
                {
                    player.CardsInHand = new List<Card>() { deck[0], deck[1] };
                    deck = new List<Card>(RemoveTwoCards(deck));
                    Console.WriteLine($"It's your turn {player.PlayerName}");
                    foreach (var card in player.CardsInHand)
                    {
                        Console.WriteLine($"You've been dealt a {card.Rank}{card.Suit}");
                    }
                    Console.WriteLine($"Right now, you're at {player.TotalCardsInHandValue()}");

                    var playerChoice = "";
                    while (player.TotalCardsInHandValue() < 21 && playerChoice.ToLower() != "stand")
                    {


                        Console.Write($"\nWould you like to 'hit' or 'stand' {player.PlayerName}? ");
                        playerChoice = Console.ReadLine();
                        Console.WriteLine("");
                        var currentCardInHand = 2;
                        if (playerChoice.ToLower() == "hit")
                        {
                            player.CardsInHand.Add(deck[0]);
                            deck.Remove(deck[0]);
                            Console.WriteLine($"You've been dealt a {player.CardsInHand[currentCardInHand].Rank}{player.CardsInHand[currentCardInHand].Suit}");
                            Console.WriteLine($"Right now, you're at {player.TotalCardsInHandValue()}");
                            currentCardInHand++;
                        }
                        else if (playerChoice.ToLower() == "stand")
                        {
                            Console.WriteLine("Moving onto next hand...");
                        }
                        else if (playerChoice.ToLower() != "hit" || playerChoice.ToLower() != "stand")
                        {
                            Console.WriteLine("Please either enter 'hit' or 'stand'.");
                            Console.WriteLine("It's not case sensitive, but it is spelling sensitive.");
                        }
                    }

                    if (player.TotalCardsInHandValue() > 21)
                    {
                        Console.WriteLine($"\nYou busted, {player.PlayerName}! You lost this round\n");
                    }
                    else if (player.TotalCardsInHandValue() == 21)
                    {
                        Console.WriteLine($"\nYou have 21, {player.PlayerName}! You're not hitting again!");
                    }
                    else
                    {
                        while (computer.TotalCardsInHandValue() < 17)
                        {
                            computer.CardsInHand.Add(deck[0]);
                            deck.Remove(deck[0]);
                        }
                    }
                }
                if (computer.TotalCardsInHandValue() > 21)
                {
                    Console.WriteLine("\nThe dealer busted! Everyone who didn't bust won this round\n");
                    foreach (var player in listOfPlayers)
                    {
                        if (player.TotalCardsInHandValue() <= 21)
                        {
                            player.Wins++;
                        }
                    }
                }
                else
                {
                    foreach (var player in listOfPlayers)
                    {
                        if (computer.TotalCardsInHandValue() > player.TotalCardsInHandValue())
                        {
                            Console.WriteLine($"\n{player.PlayerName} lost to Dealer this round.\n");
                        }
                        else if (computer.TotalCardsInHandValue() < player.TotalCardsInHandValue() && player.TotalCardsInHandValue() <= 21)
                        {
                            Console.WriteLine($"\n{player.PlayerName} beat Dealer this round!\n");
                            player.Wins++;
                        }
                        else if (computer.TotalCardsInHandValue() == player.TotalCardsInHandValue())
                        {
                            Console.WriteLine($"\n{player.PlayerName} tied with Dealer. Dealer wins by default.\n");
                        }
                    }
                }
                roundCounter++;
            }
        }
    }
}

