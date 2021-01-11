using System;
using System.Collections.Generic;

namespace BlackJackCS
{
    // Created by Jerid Fenderson
    // Suncoast Developers Guild
    // Cohort-20
    // Created January 8th, 2021
    // Last modified January 11th, 2021
    class Program
    {
        class Player
        // Class that stores all individual player information needed to properly run game 
        {
            public List<Card> CardsInHand { get; set; }
            public int PlayerNumber { get; set; }
            public string PlayerName { get; set; }
            public int Wins { get; set; }
            public int TotalPoints { get; set; }
            public int PointsBetThisRound { get; set; }
            public bool IsComputer = false;
            public bool DoubledDown = false;
            public bool Surrendered = false;
            public int TotalCardsInHandValue()
            {
                var cardValueCounter = 0;
                foreach (var card in CardsInHand)
                {
                    if (card.CardValue == 0)
                    {
                        card.CardValue = card.SetCardValue(IsComputer);
                    }
                    cardValueCounter += card.CardValue;
                }
                return cardValueCounter;
            }
            public void PlaceBets()
            {
                Console.Write("In increments of 20, how many chips would you like to bet this round? ");
                var individualBetAsString = Console.ReadLine();
                while (PointsBetThisRound == 0)
                {
                    if (TotalPoints >= 20)
                    {
                        switch (individualBetAsString)
                        {
                            case "20":
                                if (TotalPoints == 20)
                                {
                                    Console.WriteLine($"\n{PlayerName}...you better win this round...");
                                    Console.WriteLine("...or you're going to have no chips left...\n");
                                    PointsBetThisRound = 20;
                                    break;
                                }
                                else
                                {
                                    PointsBetThisRound = 20;
                                    break;
                                }
                            case "40":
                                if (TotalPoints < 40)
                                {
                                    Console.Write("\nYou don't have the chips my friend. Please bet lower: ");
                                    individualBetAsString = Console.ReadLine();
                                    break;
                                }
                                else
                                {
                                    PointsBetThisRound = 40;
                                    break;
                                }
                            case "60":
                                if (TotalPoints < 60)
                                {
                                    Console.Write("\nYou don't have the chips my friend. Please bet lower: ");
                                    individualBetAsString = Console.ReadLine();
                                    break;
                                }
                                else
                                {
                                    PointsBetThisRound = 60;
                                    break;
                                }
                            case "80":
                                if (TotalPoints < 80)
                                {
                                    Console.Write("\nYou don't have the chips my friend. Please bet lower: ");
                                    individualBetAsString = Console.ReadLine();
                                    break;
                                }
                                else
                                {
                                    PointsBetThisRound = 80;
                                    break;
                                }
                            case "100":
                                if (TotalPoints < 100)
                                {
                                    Console.Write("\nYou don't have the chips my friend. Please bet lower: ");
                                    individualBetAsString = Console.ReadLine();
                                    break;
                                }
                                else
                                {
                                    PointsBetThisRound = 100;
                                    break;
                                }
                            default:
                                Console.Write("\nPlease bet either '20', '40', '60', '80' or '100' chips: ");
                                individualBetAsString = Console.ReadLine();
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\n{PlayerName}...you better win this round...");
                        Console.WriteLine("...or you're going to have no chips left...\n");
                        Console.WriteLine("...you only have 10 chips left to begin with...\n");
                        Console.WriteLine("...those 10 chips were bet for you...\n");
                        PointsBetThisRound = 10;
                    }

                }
            }
            public void AddChips()
            {
                TotalPoints = TotalPoints + PointsBetThisRound;
                PointsBetThisRound = 0;
            }
            public void RemoveChips()
            {
                TotalPoints = TotalPoints - PointsBetThisRound;
                PointsBetThisRound = 0;
            }
            public void Surrender()
            {
                TotalPoints = TotalPoints - PointsBetThisRound / 2;
                PointsBetThisRound = 0;
            }
        }
        static List<Player> Roster(List<Player> playersToBeAdded, int numberOfPlayers, int startingPoints)
        // Method that builds a list of players using the Player class to create each player individually
        {
            for (var i = 1; i <= numberOfPlayers; i++)
            {
                Console.Write($"What is Player {i}'s name? ");
                var newPlayer = new Player()
                {
                    PlayerNumber = i,
                    Wins = 0,
                    TotalPoints = startingPoints,
                    PlayerName = Console.ReadLine(),
                };
                playersToBeAdded.Add(newPlayer);
                Console.WriteLine($"Got it. Welcome {newPlayer.PlayerName}!\n");
            }
            return playersToBeAdded;
        }
        class Card
        // Class that stores each card individually and assigns an appropriate number value to the card face
        {
            public string Rank { get; set; }
            public string Suit { get; set; }
            public int CardValue = 0;
            public int SetCardValue(bool isComputer)
            {
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
                        return int.Parse(Rank);
                    case "10":
                    case "Jack":
                    case "Queen":
                    case "King":
                        return 10;
                    case "Ace":
                        return OneOrEleven(isComputer);
                    default:
                        return 0;
                }
                static int OneOrEleven(bool isComputer)
                {
                    if (isComputer)
                    {
                        return 11;
                    }
                    else
                    {
                        int cardValue = 0;
                        Console.Write("You have an Ace, is it worth '1' or '11'? ");
                        var cardValueAsString = Console.ReadLine();
                        while (cardValueAsString != "1" && cardValueAsString != "11")
                        {
                            Console.Write("Please enter either '1' or '11' only: ");
                            cardValueAsString = Console.ReadLine();
                        }
                        cardValue = int.Parse(cardValueAsString);
                        return cardValue;
                    }
                }
            }
        }
        static List<Card> Build(List<Card> deckToBeBuilt)
        // Method that builds deck, using the Card class to create each card and places each card in a list
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
        static void Welcome()
        // Method that greets player(s) upon start of program
        {
            Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("Welcome to Blackjack!");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~\n");
        }
        static void Goodbye()
        // Method that says goodbye to players(s) upon the end of program
        {
            Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("Thanks for Blackjack!");
            Console.WriteLine("Created by Jerid Fenderson");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~\n");
        }
        static void Help()
        // Method that displays a help menu with commands upon user request
        {
            Console.WriteLine("\nTo play a round of Blackjack, enter 'play'");
            Console.WriteLine("To see current game stats, including player wins, enter 'stats'");
            Console.WriteLine("To learn the basic rules of Blackjack enter 'rules'");
            Console.WriteLine("To leave the table/exit the program, enter 'exit'\n");
        }
        static void Rules(List<Player> listOfPlayers)
        // Method that displays rules of Blackjack upon user request
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
        static List<Player> PlayBlackjack(List<Player> listOfPlayers, bool keepingScore)
        // Method that plays a round of Blackjack with all the current players in this game
        {
            var deck = new List<Card>();
            deck = new List<Card>(Build(deck));
            deck = new List<Card>(Shuffle(deck));
            Console.WriteLine("Cards have been shuffled");

            var computer = new Player();
            computer.IsComputer = true;
            computer.CardsInHand = new List<Card>() { deck[0], deck[1] };
            deck = new List<Card>(RemoveTwoCards(deck));
            Console.WriteLine("Dealer's cards have been dealt");
            Console.WriteLine($"Dealer flipped a {computer.CardsInHand[0].Rank}{computer.CardsInHand[0].Suit}\n");

            foreach (var player in listOfPlayers)
            {
                player.CardsInHand = new List<Card>() { deck[0], deck[1] };
                deck = new List<Card>(RemoveTwoCards(deck));
                Console.WriteLine($"It's your turn {player.PlayerName}");
                if (keepingScore)
                {
                    if (player.TotalPoints != 0)
                    {
                        player.PlaceBets();
                    }
                    else
                    {
                        Console.WriteLine("\nYou have no chips to bet...");
                        Console.WriteLine("...you filthy animal...\n");
                    }
                }
                foreach (var card in player.CardsInHand)
                {
                    Console.WriteLine($"You've been dealt a {card.Rank}{card.Suit}");
                }
                Console.WriteLine($"Right now, you're at {player.TotalCardsInHandValue()}");

                var playerChoice = "";
                while (player.TotalCardsInHandValue() < 21 && playerChoice != "stand")
                {
                    Console.Write($"\nWhat would you like to do {player.PlayerName}? ");
                    playerChoice = Console.ReadLine().ToLower();
                    Console.WriteLine("");
                    var currentCardInHand = 2;
                    switch (playerChoice)
                    {
                        case "hit":
                            player.CardsInHand.Add(deck[0]);
                            deck.Remove(deck[0]);
                            Console.WriteLine($"You've been dealt a {player.CardsInHand[currentCardInHand].Rank}{player.CardsInHand[currentCardInHand].Suit}");
                            Console.WriteLine($"Right now, you're at {player.TotalCardsInHandValue()}");
                            currentCardInHand++;
                            break;
                        case "stand":
                            Console.WriteLine("Moving onto next hand...\n");
                            break;
                        case "surrender":
                            if (keepingScore)
                            {
                                player.Surrendered = true;
                                player.Surrender();
                                playerChoice = "stand";
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Please enter either 'hit' or 'stand', you're not keeping score");
                                Console.Write("It's not case sensitive, but it is spelling sensitive: ");
                                playerChoice = Console.ReadLine();
                                break;
                            }
                        case "double down":
                            if (keepingScore)
                            {

                            }
                            else
                            {
                                Console.WriteLine("Please enter either 'hit' or 'stand', you're not keeping score");
                                Console.Write("It's not case sensitive, but it is spelling sensitive: ");
                                playerChoice = Console.ReadLine();
                            }
                            break;
                        default:
                            if (keepingScore)
                            {
                                Console.WriteLine("Please enter 'hit', 'stand', 'surrender' or 'double down'");
                                Console.Write("It's not case sensitive, but it is spelling sensitive: ");
                                playerChoice = Console.ReadLine();
                            }
                            else
                            {
                                Console.WriteLine("Please enter either 'hit' or 'stand'");
                                Console.Write("It's not case sensitive, but it is spelling sensitive: ");
                                playerChoice = Console.ReadLine();
                            }
                            break;
                    }
                }
                if (player.Surrendered)
                {
                    Console.WriteLine($"You've successfully surrendered this round, {player.PlayerName}\n");
                }
                else
                {
                    if (player.TotalCardsInHandValue() > 21)
                    {
                        Console.WriteLine($"\nYou busted, {player.PlayerName}! You lost this round\n");
                        if (keepingScore)
                            player.RemoveChips();
                    }
                    else if (player.TotalCardsInHandValue() == 21)
                    {
                        Console.WriteLine($"\nYou have 21, {player.PlayerName}! You're not hitting again!\n");
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
            }
            if (computer.TotalCardsInHandValue() > 21)
            {
                if (keepingScore)
                {
                    Console.WriteLine("\nThe Dealer busted! Everyone who didn't bust or surrender won this round\n");
                }
                else
                {
                    Console.WriteLine("\nThe Dealer busted! Everyone who didn't bust won this round\n");
                }
                foreach (var player in listOfPlayers)
                {
                    if (player.TotalCardsInHandValue() <= 21 && !player.Surrendered)
                    {
                        player.Wins++;
                        if (keepingScore)
                            player.AddChips();
                    }
                    else
                    {
                        player.Surrendered = false;
                    }
                }
            }
            else
            {
                foreach (var player in listOfPlayers)
                {
                    if (player.Surrendered)
                    {
                        Console.WriteLine($"{player.PlayerName} surrendered. They lose half of their bid\n");
                    }
                    else
                    {
                        if (computer.TotalCardsInHandValue() > player.TotalCardsInHandValue())
                        {
                            Console.WriteLine($"{player.PlayerName} lost to the Dealer this round\n");
                            if (keepingScore)
                                player.RemoveChips();
                        }
                        else if (computer.TotalCardsInHandValue() < player.TotalCardsInHandValue() && player.TotalCardsInHandValue() <= 21)
                        {
                            Console.WriteLine($"{player.PlayerName} beat the Dealer this round!\n");
                            if (keepingScore)
                                player.AddChips();
                            player.Wins++;
                        }
                        else if (computer.TotalCardsInHandValue() == player.TotalCardsInHandValue())
                        {
                            Console.WriteLine($"{player.PlayerName} tied with the Dealer. The Dealer wins by default\n");
                            if (keepingScore)
                                player.RemoveChips();
                        }
                    }
                }
            }
            if (keepingScore)
            {
                var hasPointsLeft = 0;
                foreach (var player in listOfPlayers)
                {
                    if (player.TotalPoints > 0)
                        hasPointsLeft++;
                }
                if (hasPointsLeft > 2 || (listOfPlayers.Count == 1 && listOfPlayers[0].TotalPoints > 40))
                {
                    Console.WriteLine("The table is still going strong!");
                }
                else if ((hasPointsLeft == 2 && listOfPlayers.Count != 2) || (listOfPlayers.Count == 1 && listOfPlayers[0].TotalPoints < 40))
                {
                    Console.WriteLine("The table is a bit shakey! Someone's going to lose soon, maybe!");
                }
                else if ((hasPointsLeft == 1 && listOfPlayers.Count <= 2) || (listOfPlayers.Count == 1 && listOfPlayers[0].TotalPoints == 0))
                {
                    foreach (var player in listOfPlayers)
                    {
                        if (player.TotalPoints != 0)
                            Console.WriteLine($"{player.PlayerName} won the game!");
                    }
                    Console.WriteLine("\n\nGet out of here!");
                    Console.WriteLine("You don't bet points, lose all your points, and then keep playing!");
                    Console.WriteLine("No seriously, what do you think you're still doing here?");
                    Console.WriteLine("If you want a sandbox, don't keep score next time!\n\n");
                    Goodbye();
                    System.Environment.Exit(0);
                }
            }
            return listOfPlayers;
        }
        static void SeeStats(List<Player> listOfPlayers, int roundNumber, bool scoreKeeper)
        // Method that displays stats of all players within Blackjack for the whole game
        {
            if (roundNumber == 1)
            {
                Console.WriteLine($"You've played Blackjack {roundNumber} time\n");
            }
            else
            {
                Console.WriteLine($"You've played Blackjack {roundNumber} times\n");
            }
            if (scoreKeeper)
            {
                foreach (var player in listOfPlayers)
                {
                    if (player.Wins == 1)
                    {
                        Console.WriteLine($"{player.PlayerName} won {player.Wins} time and has {player.TotalPoints} chips");
                    }
                    else
                    {
                        Console.WriteLine($"{player.PlayerName} won {player.Wins} times and has {player.TotalPoints} chips");
                    }
                }
            }
            else
            {
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
            var points = 0;
            var scoreKeeper = false;
            var playersList = new List<Player>();
            Console.Write("How many players are there? ");
            var numOfPlayersAsString = Console.ReadLine();
            Console.WriteLine("");
            int numOfPlayers = 0;
            while (numOfPlayers == 0)
            {
                switch (numOfPlayersAsString)
                {
                    case "1":
                        numOfPlayers = 1;
                        break;
                    case "2":
                        numOfPlayers = 2;
                        break;
                    case "3":
                        numOfPlayers = 3;
                        break;
                    case "4":
                        numOfPlayers = 4;
                        break;
                    case "5":
                        numOfPlayers = 5;
                        break;
                    case "6":
                        numOfPlayers = 6;
                        break;
                    case "7":
                        numOfPlayers = 7;
                        break;
                    default:
                        Console.Write("\nMin to max player size is 1 to 7: ");
                        numOfPlayersAsString = Console.ReadLine();
                        break;
                }
            }
            Console.Write("\nDo you want a betting system that keeps a points based score? ");
            var toKeepScore = Console.ReadLine();
            while (toKeepScore != "yes" && toKeepScore != "no")
            {
                Console.Write("Please say 'yes' or 'no': ");
                toKeepScore = Console.ReadLine();
            }
            if (toKeepScore == "yes")
            {
                scoreKeeper = true;
                points = 100;
                Console.WriteLine("Keeping score, you all will start out with 100 chips\n");
            }
            else
            {
                Console.WriteLine("Ok, got it. No active score will be kept this game\n");
            }
            var listOfPlayers = new List<Player>();
            listOfPlayers = new List<Player>(Roster(listOfPlayers, numOfPlayers, points));

            var menuSelection = PlayAgain().ToLower();
            while (menuSelection != "exit")
            {
                switch (menuSelection)
                {
                    case "play":
                        listOfPlayers = PlayBlackjack(listOfPlayers, scoreKeeper);
                        roundCounter++;
                        menuSelection = PlayAgain().ToLower();
                        break;
                    case "stats":
                        SeeStats(listOfPlayers, roundCounter, scoreKeeper);
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
                        Console.WriteLine("It's not case sensitive, but it is spelling sensitive\n");
                        menuSelection = PlayAgain().ToLower();
                        break;
                }
            }
            Goodbye();
        }
    }
}

