# BlackJackCS

# PEDAC

## Problem

Need to create a single player Black Jack game against the computer using a standard 52-card deck

- The winner is determined by who has the highest card values, without exceeding 21

- The player can hit or bust as many times as they'd like until they bust
- If the player bust, the computer automatically wins

- The computer will hit until their card value total is >= 17 or they bust
- If the computer busts, the player wins
- If the player and computer ties, the computer wins
- If the player's total card value is less than the computer's total card value, the computer wins

The code must have the option to run again so long as player wants to play again

## Example

- Player has 17, dealer has 19, dealer wins
- Player has 21, dealer has 21, dealer wins
- Player has 21, dealer busts, player wins
- Player has 19, dealer has 18, player wins

## Data

- Need deck of cards (property)
- Need point value for deck of cards (property)
- Need to be able to shuffle deck of cards (methods)
- Need to keep track of Player's cards (property)
- Need to keep track of Dealer's cards (property)
- Need to be able to hit (method)
- Need to be able to stay (method)
- Need to be able to determine winner (method)

## Algorithm

Generate card value pairs

- assign a number value of 11 to Ace
- assign a number value of 10 to all other face cards
- assign a number value equal to the card number for all other cards

Generate a deck of 52 card value pairs

- assign a card value pair for each card type in a 52-card deck

Shuffle card value pairs

- use a shuffling algorithm to randomize the newly generated deck

Deal the house two card from the deck

- issue the first two cards within the shuffled deck to the dealer

Deal the player the next two cards from the deck

- issue the next two cards to the player

Show the player their cards

- write out the cards issued from the deck to the player in a readable way

Allow the player to either hit (take the next card from deck) or stay (hold score and move onto computer's turn)

- if the player hits, grab the next card from the deck and show it to player
- after player hits or stays, evaluate player's card scores and determine if player busted (the value is above 21). If so, the dealer wins. Give option for new game
- if the player didn't bust, allow the player to either hit or stay again
  -if the player stays, calculate player's total card value and move on to the dealer's (computer) turn

Allow the computer to either hit (take the next card from deck) if their total card value is under 17 or stay if their total card value is equal to or greater than 17

- if the dealer hits, grab the next card from the deck
- after the computer hits or stays, evaluate the computer's card scores and determine if the computer busted (the value is above 21). If so, the player wins. Give option for new game
- if computer didn't bust, determine if the computer should hit or stay again based on the computer's total card score being above or below 17
- if computer stays, calculate computer's total card value

Determine winner if nobody busts

- compare player's total card value with dealer's total card value
- whoever has the highest total card value wins
- give the option for a new game

Give player option for new game

- if player would like to play again, rebuild deck and run script again
- else, terminate
