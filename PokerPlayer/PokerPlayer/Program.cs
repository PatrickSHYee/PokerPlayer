﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerPlayer
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    PokerPlayer player = new PokerPlayer();
        //    List<Card> testHand = new List<Card>();
        //    Deck dealerDeck = new Deck();
        //    // lets shuffle the deck 100 times
        //    for (int s = 0; s < 100; s++)
        //    {
        //        dealerDeck.Shuffle();
        //    }

        //    for (int i = 0; i < 5; i++)
        //    {
        //        player.DrawHand(dealerDeck.Deal(5));
        //        player.ShowHand();
        //    }
        //    Console.ReadKey();
        //}

        static void Main(string[] args)
        {
            PokerPlayer player = new PokerPlayer();
            List<Card> testHand = new List<Card>();

            // straight test
            testHand.Add(new Card((int)Rank.Two, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Three, (int)Suit.Diamond));
            testHand.Add(new Card((int)Rank.Five, (int)Suit.Heart));
            testHand.Add(new Card((int)Rank.Four, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Six, (int)Suit.Diamond));
            player.DrawHand(testHand);
            player.ShowHand();
            Console.WriteLine();

            testHand.Clear();
            testHand.Add(new Card((int)Rank.Two, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Three, (int)Suit.Diamond));
            testHand.Add(new Card((int)Rank.King, (int)Suit.Heart));
            testHand.Add(new Card((int)Rank.Four, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Ace, (int)Suit.Diamond));
            player.DrawHand(testHand);
            player.ShowHand();
            Console.WriteLine();

            testHand.Clear();
            testHand.Add(new Card((int)Rank.Queen, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Jack, (int)Suit.Diamond));
            testHand.Add(new Card((int)Rank.King, (int)Suit.Heart));
            testHand.Add(new Card((int)Rank.Ace, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Ten, (int)Suit.Diamond));
            testHand.Clear();
            testHand.Add(new Card(Rank.Queen, Suit.Club));
            testHand.Add(new Card(Rank.Jack, Suit.Diamond));
            testHand.Add(new Card(Rank.King, Suit.Heart));
            testHand.Add(new Card(Rank.Ace, Suit.Club));
            testHand.Add(new Card(Rank.Ten, Suit.Diamond));
            player.DrawHand(testHand);
            player.ShowHand();
            Console.WriteLine();

            testHand.Clear();
            // four of a kind test
            testHand.Add(new Card((int)Rank.Ten, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Ten, (int)Suit.Diamond));
            testHand.Add(new Card((int)Rank.Ten, (int)Suit.Heart));
            testHand.Add(new Card((int)Rank.Ten, (int)Suit.Spade));
            testHand.Add(new Card((int)Rank.Queen, (int)Suit.Spade));
            player.DrawHand(testHand);
            player.ShowHand();
            Console.WriteLine();

            testHand.Clear();
            testHand.Add(new Card(Rank.Ten, Suit.Club));
            testHand.Add(new Card(Rank.Ten, Suit.Diamond));
            testHand.Add(new Card(Rank.Ten, Suit.Club));
            testHand.Add(new Card(Rank.Ten, Suit.Spade));
            testHand.Add(new Card(Rank.Queen, Suit.Spade));
            player.DrawHand(testHand); ;
            player.ShowHand();
            Console.ReadKey();
        }
    }

    /// <summary>
    /// The pokerplayer gets a cards from dealer and determines what he has.
    /// </summary>
    class PokerPlayer
    {
        /// <summary>
        /// Enum for the winning hand
        /// </summary>
        public enum HandType
        {
            HighCard, OnePair, TwoPair, ThreeOfAKind, Straight, Flush, FullHouse, FourOfAKind, StraightFlush, RoyalFlush
        }

        // The current hand of the poker player
        private List<Card> _listOfCards;
        public List<Card> ListOfCards
        {
            get { return _listOfCards; }
            set { _listOfCards = value; }
        }

        // Rank of hand that player holds
        private HandType _handRank;
        public HandType HandRank { set { _handRank = value; } get { return _handRank; } }
        // The highest card that the player has
        private Card _highestCard;
        public Card HighestCard { get { return _highestCard; } set { _highestCard = value; } }
        private string _myHand;
        public string MyHand { get { return _myHand; } set { _myHand = value; } }

        /// <summary>
        /// nothing is created, but for the player
        /// </summary>
        public PokerPlayer()
        {
            HandRank = HandType.HighCard;
        }

        /// <summary>
        /// The player gets a hand from the dealer and does his check
        /// </summary>
        /// <param name="CurrentHand"></param>
        public void DrawHand(List<Card> CurrentHand)
        {
            // Default the high card
            ListOfCards = CurrentHand;
            HighestCard = ListOfCards.OrderByDescending(x => x.Ranked).First();
            MyHand = "High Card";
            HandRank = HandType.HighCard;

            // the Poker player does there checks with my hand of what it is
            if (HasPair())
            {
                HandRank = HandType.OnePair;
                MyHand = "One Pair";
            }
            if (HasTwoPair())
            {
                HandRank = HandType.TwoPair;
                MyHand = "Two Pair";
            }
            if (HasThreeOfAKind())
            {
                HandRank = HandType.ThreeOfAKind;
                MyHand = "Three of a kind";
            }
            if (HasFlush())
            {
                HandRank = HandType.Flush;
                MyHand = "Flushed out";
            }
            if (HasStraight())
            {
                HandRank = HandType.Straight;
                MyHand = "straight";
            }
            if (HasFullHouse())
            {
                HandRank = HandType.FullHouse;
                MyHand = "full house";
            }
            if (HasFourOfAKind())
            {
                HandRank = HandType.FourOfAKind;
                MyHand = "four of a kind";
            }
            if (HasStraightFlush())
            {
                MyHand = "straight flush";
                HandRank = HandType.StraightFlush;
            }
            if (HasRoyalFlush())
            {
                MyHand = "ROYAL FLUSH";
                HandRank = HandType.RoyalFlush;
            }
        }

        /// <summary>
        /// Displays the poker player's hand and what it is.
        /// </summary>
        public void ShowHand()
        {
            ListOfCards.ForEach(x => x.ShowCard());
            Console.WriteLine("\n{0}", MyHand);
        }

        /// <summary>
        /// Finds a pair
        /// </summary>
        /// <returns>Has a pair</returns>
        public bool HasPair()
        {
            return ListOfCards.GroupBy(x => x.Ranked).Count(x => x.Count() == 2) == 1;
        }

        /// <summary>
        /// Finds the two pair in a hand
        /// </summary>
        /// <returns>True if the 2 pairs</returns>
        public bool HasTwoPair()
        {
            return ListOfCards.GroupBy(x => x.Ranked).Count(x => x.Count() >= 2) == 2;
        }
        
        /// <summary>
        /// Checks for a three of a kind
        /// </summary>
        /// <returns>boolean return</returns>
        public bool HasThreeOfAKind()
        {
            if (ListOfCards.GroupBy(x => x.Ranked).Any(x => x.Count() == 3))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if the hand is a straight. Even checks if A, 2, 3, 4, 5 is a straight
        /// </summary>
        /// <returns>True if the has a straight</returns>
        public bool HasStraight()
        {
            bool itsGood = true;
            List<Card> tempList = ListOfCards.OrderBy(x => x.Ranked).ToList();
            int count = tempList.Count;

            // checks if is A, 2, 3, 4, 5 straight.
            // remember the ace is a high cardeturn itsGood;
            if (tempList.Last().Ranked == Rank.Ace && tempList.First().Ranked == Rank.Two)
            {
                itsGood = true;
                count--; // make sure that the last element does gets check as well.
            }

            for (int i = 0; i < count - 1; i++)
            {
                if (tempList[i + 1].Ranked - 1 != tempList[i].Ranked)
                {
                    itsGood = false;
                }
            }
            return itsGood;
        }

        /// <summary>
        /// Checks for a flush
        /// </summary>
        /// <returns>boolean value for a flush</returns>
        public bool HasFlush()
        {
            if (ListOfCards.Select(x => x.TheSuit).Distinct().Count() == 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check if three of a kind and has a pair
        /// </summary>
        /// <returns>return true if it three of a kind</returns>
        public bool HasFullHouse()
        {
            if (HasThreeOfAKind() && HasPair())
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if there is four of a kind and that it's not the same suit
        /// </summary>
        /// <returns></returns>
        public bool HasFourOfAKind()
        {
            if (ListOfCards.GroupBy(x => x.Ranked).Any(x => x.Count() == 4) && ListOfCards.Select(x => x.TheSuit).Distinct().Count() >= 4)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// If HasStraight and HasFlush is both true to make the check.
        /// </summary>
        /// <returns>it's a big winner</returns>
        public bool HasStraightFlush()
        {
            if (HasStraight() && HasFlush())
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// checks if the hand is a straightFlush and that the first card is a ten.
        /// </summary>
        /// <returns>It's money</returns>
        public bool HasRoyalFlush()
        {
            if (HasStraightFlush() && ListOfCards.OrderBy(x => x.Ranked).First().Ranked == Rank.Ten)
            {
                return true;
            }
            return false;
        }
    }
    //Guides to pasting your Deck and Card class

    //  *****Deck Class Start*****
    /// <summary>
    /// The making of a deck of cards
    /// </summary>
    class Deck
    {
        private List<Card> _deckOfCards;
        public List<Card> DeckOfCards { get { return _deckOfCards; } set { _deckOfCards = value; } }
        private List<Card> _discardedCards = new List<Card>();
        public List<Card> DiscardedCards { get; set; }

        /// <summary>
        /// A normal deck of cards has 52 card of each suit with ranks
        /// </summary>
        public Deck()
        {
            DeckOfCards = new List<Card>();
            DiscardedCards = new List<Card>();
            for (int r = 2; r <= 14; r++)
            {
                for (int s = 1; s <= 4; s++)
                {
                    DeckOfCards.Add(new Card((Rank)r, (Suit)s));
                }
            }
        }

        /// <summary>
        /// It creates a number of decks in the deck.
        /// </summary>
        /// <param name="howMany">How many decks it needs to be created</param>
        public Deck(int howMany)
        {
            DeckOfCards = new List<Card>();
            DiscardedCards = new List<Card>();
            for (int i = 0; i < howMany; i++)
            {
                for (int r = 2; r <= 14; r++)
                {
                    for (int s = 1; s <= 4; s++)
                    {
                        DeckOfCards.Add(new Card((Rank)r, (Suit)s));
                    }
                }
            }
        }

        /// <summary>
        /// it pulls out how many cards from the deck
        /// </summary>
        /// <param name="numOfCards">the number of cards to be pulled</param>
        /// <returns>A list of cards</returns>
        public List<Card> Deal(int numOfCards)
        {
            List<Card> BigHand = new List<Card>();
            for (int c = 0; c < numOfCards; c++)
            {
                BigHand.Add(DeckOfCards[0]);
                DeckOfCards.RemoveAt(0);
            }
            return BigHand;
        }

        /// <summary>
        /// it discards the list of cards
        /// </summary>
        /// <param name="hand"></param>
        public void Discard(List<Card> hand)
        {
            while (hand.Count > 0)
            {
                DiscardedCards.Add(hand[0]);
                hand.RemoveAt(0);
            }
        }

        /// <summary>
        /// Shuffle the deck
        /// </summary>
        public void Shuffle()
        {
            List<Card> newDeckOfCards = new List<Card>();
            Random shuffler = new Random();
            // put the cards back to the DeckOfCards from the Discard piles
            if (DiscardedCards.Count != 0)
            {
                while (DiscardedCards.Count > 0)
                {
                    DeckOfCards.Add(DiscardedCards[0]);
                    DiscardedCards.RemoveAt(0);
                }
            }
            while (DeckOfCards.Count > 0)
            {
                Card getCard = DeckOfCards[shuffler.Next(1, DeckOfCards.Count) - 1];
                newDeckOfCards.Add(getCard);
                DeckOfCards.Remove(getCard);
            }
            DeckOfCards = newDeckOfCards;
        }
    }


    //  *****Deck Class End*******

    //  *****Card Class Start*****

    // What makes a card?
    //     A card is comprised of it’s suit and its rank.  Both of which are enumerations.
    //     These enumerations should be "Suit" and "Rank"
    public enum Suit
    {
        Heart = 1,
        Diamond, Club, Spade
    }

    /// <summary>
    /// the rank for the card
    /// </summary>
    public enum Rank
    {
        Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace
    }
    // What makes a card?
    //     A card is comprised of it’s suit and its rank.  Both of which are enumerations.
    //     These enumerations should be "Suit" and "Rank"
    class Card
    {
        public Suit TheSuit { get; set; }
        public Rank Ranked { get; set; }

        public Card(Rank rank, Suit suit)
        {
            this.TheSuit = suit;
            this.Ranked = rank;
        }

        public Card(int rank, int suit)
        {
            this.TheSuit = (Suit)suit;
            this.Ranked = (Rank)rank;
        }

        /// <summary>
        /// Shows the card
        /// </summary>
        public void ShowCard()
        {
            Console.WriteLine("{0} of {1}", this.Ranked, this.TheSuit);
        }
    }
    //  *****Card Class End*******class Deck
}