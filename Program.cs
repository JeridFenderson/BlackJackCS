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
            Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("Welcome to Black Jack");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~\n");
        }
        static void Help()
        {
            Console.WriteLine("\nTo play a round of Blackjack, enter 'play'");
            Console.WriteLine("To see current game stats, including player wins, enter 'stats'");
            Console.WriteLine("To learn the basic rules of Blackjack enter 'rules'");
            Console.WriteLine("To leave the table/exit the program, enter 'exit'\n");
        }
        static void Rules(List<Player> listOfPlayers)
        {
            Console.WriteLine("\nBlackjack is a card game where the goal is to either get 21 or beat the Dealer's score without going over 21");
            Console.WriteLine("Each card has a value equal to itself, except face cards and the Ace. All face are equal to 10 while the Ace equals 11");
            Console.WriteLine("\nWhile you're under 21, you'll have the option to 'hit' or 'stand'. Hitting will give you another card from the deck");
            Console.WriteLine("If your total card value exceeds 21 after you 'hit', you busted. You lose the round regardless of what happens with the Dealer");
            Console.WriteLine("If you 'stand' before you're over 21, the next player then gets the same option to 'hit' or 'stand' as well");
            Console.WriteLine("\nOnce when all players have had their turn, of the players who didn't bust, the Dealer then takes his turn just as you all did");
            Console.WriteLine("If the Dealer busts, all players who didn't bust this round automatically win the round");
            Console.WriteLine("If the Dealer doesn't bust, each player's total card value against the Dealer's total card value individually");
            Console.WriteLine("If the your card value is greater than, but not equal to the Dealer's card value, you win!");
            Console.WriteLine("\nEach player's total number of wins is kept track of individually for the duration of the game");
            Console.WriteLine("You can see the stats after each round by entering 'stats' when asked 'What would you like to do?'");
            Console.Write("\nHappy playing ");
            if (listOfPlayers.Count == 1)
            {
                Console.WriteLine($"{listOfPlayers[0].PlayerName}!\n");
            }
            else if (listOfPlayers.Count == 2)
            {
                Console.WriteLine($"{listOfPlayers[0].PlayerName} and {listOfPlayers[1].PlayerName}!\n");
            }
            else
            {
                for (var players = 0; players <= listOfPlayers.Count - 2; players++)
                {
                    Console.Write($"{listOfPlayers[players].PlayerName}, ");
                }
                Console.WriteLine($"and {listOfPlayers[listOfPlayers.Count - 1].PlayerName}!\n");
            }
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
        static List<Player> PlayBlackjack(List<Player> listOfPlayers)
        {
            var deck = new List<Card>();
            deck = new List<Card>(Build(deck));
            deck = new List<Card>(Shuffle(deck));
            Console.WriteLine("Cards have been shuffled");

            var computer = new Player();
            computer.CardsInHand = new List<Card>() { deck[0], deck[1] };
            deck = new List<Card>(RemoveTwoCards(deck));
            Console.WriteLine("Dealer's cards have been dealt");
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
                        Console.WriteLine("Moving onto next hand...\n");
                    }
                    else if (playerChoice.ToLower() != "hit" || playerChoice.ToLower() != "stand")
                    {
                        Console.WriteLine("Please either enter 'hit' or 'stand'");
                        Console.WriteLine("It's not case sensitive, but it is spelling sensitive");
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
                Console.WriteLine("\nThe Dealer busted! Everyone who didn't bust won this round\n");
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
                        Console.WriteLine($"{player.PlayerName} lost to the Dealer this round\n");
                    }
                    else if (computer.TotalCardsInHandValue() < player.TotalCardsInHandValue() && player.TotalCardsInHandValue() <= 21)
                    {
                        Console.WriteLine($"{player.PlayerName} beat the Dealer this round!\n");
                        player.Wins++;
                    }
                    else if (computer.TotalCardsInHandValue() == player.TotalCardsInHandValue())
                    {
                        Console.WriteLine($"{player.PlayerName} tied the with Dealer. The Dealer wins by default\n");
                    }
                }
            }
            return listOfPlayers;
        }
        static void SeeStats(List<Player> listOfPlayers, int roundNumber)
        {
            if (roundNumber == 1)
            {
                Console.WriteLine($"You've played Blackjack {roundNumber} time\n");
            }
            else
            {
                Console.WriteLine($"You've played Blackjack {roundNumber} times\n");
            }

            foreach (var player in listOfPlayers)
            {
                if (player.Wins == 1)
                {
                    Console.WriteLine($"{player.PlayerName} won {player.Wins} time");
                }
                else
                {
                    Console.WriteLine($"{player.PlayerName} won {player.Wins} times");
                }
            }
            Console.WriteLine("");
        }
        static string PlayAgain()
        // Method that takes in a string question and a round number integer and outputs a string and readLine variable
        {
            Console.Write("What would you like to do? ");
            var option = Console.ReadLine();
            Console.WriteLine("");
            return option;
        }
        static void Main(string[] args)
        {
            Welcome();
            var roundCounter = 0;
            var playersList = new List<Player>();
            Console.Write("How many players are there? ");
            var numOfPlayers = int.Parse(Console.ReadLine());
            Console.WriteLine("");
            while (numOfPlayers <= 0 || numOfPlayers > 7)
            {
                Console.Write("Min to max player size is 1 to 7: ");
                numOfPlayers = int.Parse(Console.ReadLine());
                Console.WriteLine("");
            }
            var listOfPlayers = new List<Player>();
            listOfPlayers = new List<Player>(Roster(listOfPlayers, numOfPlayers));

            var menuSelection = PlayAgain().ToLower();
            while (menuSelection != "exit")
            {
                switch (menuSelection)
                {
                    case "play":
                        listOfPlayers = PlayBlackjack(listOfPlayers);
                        roundCounter++;
                        menuSelection = PlayAgain().ToLower();
                        break;
                    case "stats":
                        SeeStats(listOfPlayers, roundCounter);
                        menuSelection = PlayAgain().ToLower();
                        break;
                    case "help":
                        Help();
                        menuSelection = PlayAgain().ToLower();
                        break;
                    case "rules":
                        Rules(listOfPlayers);
                        menuSelection = PlayAgain().ToLower();
                        break;
                    default:
                        Console.WriteLine("That is not an option. Please enter 'help' to see a list of avaliable options");
                        Console.WriteLine("It's not case sensitive, but it is spelling sensitive");
                        menuSelection = PlayAgain().ToLower();
                        break;
                }
            }
        }
    }
}

