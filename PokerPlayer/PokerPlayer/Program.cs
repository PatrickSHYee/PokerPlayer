using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerPlayer
{
    class Program
    {
        public static Deck dealerDeck;

        static void Main(string[] args)
        {
            PokerPlayer player = new PokerPlayer();
            List<Card> testHand = new List<Card>();
            dealerDeck = new Deck();
            // lets shuffle the deck 10 times
            for (int s = 0; s < 10; s++)
            {
                dealerDeck.Shuffle();
            }

            player.DrawHand(dealerDeck.Deal(5));
            player.ShowHand();
            Console.ReadKey();
        }
    }

    /// <summary>
    /// the suits for the cards to have
    /// </summary>
    public enum Suit
    {
        Heart, Diamond, Club, Spade
    }

    /// <summary>
    /// The ranking for the cards
    /// </summary>
    public enum Rank
    {
        Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace
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
        public PokerPlayer() {
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
            ListOfCards.ForEach(x=>Console.Write("{0}, ", x.GetCard()));
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
        /// <returns></returns>
        public bool HasTwoPair()
        {
            return ListOfCards.GroupBy(x=>x.Ranked).Count(x=>x.Count() >= 2 ) == 2;
        }
        public bool HasThreeOfAKind()
        {
            if (ListOfCards.GroupBy(x=>x.Ranked).Any(x=>x.Count() == 3))
            {
                return true;
            }
            return false;
        }
        public bool HasStraight()
        {
            bool itsGood = true;
            List<Card> tempList = ListOfCards.OrderBy(x=>x.Ranked).ToList();

            // checks if is A, 2, 3, 4, 5 straight.
            // remember the ace is a high cardeturn itsGood;
            if (ListOfCards.OrderBy(x => x.Ranked).Last().Ranked == Rank.Ace && tempList[0].Ranked == Rank.Two && !itsGood)
            {
                itsGood = true;
            }
            
            for (int i = 0; i < tempList.Count - 1 ; i++)
            {
                if (tempList[i + 1].Ranked-1 != tempList[i].Ranked)
                {
                    itsGood = false;
                }
            }
            return itsGood;
        }
        public bool HasFlush()
        {
            if (ListOfCards.Select(x => x.TheSuit).Distinct().Count() == 1)
            {
                return true;
            }
            return false;
        }
        public bool HasFullHouse()
        {
            if (HasThreeOfAKind() && HasPair())
            {
                return true;
            }
            return false;
        }
        public bool HasFourOfAKind()
        {
            if (ListOfCards.GroupBy(x => x.Ranked).Any(x => x.Count() == 4) && ListOfCards.Select(x => x.TheSuit).Distinct().Count() >= 4)
            {
                return true;
            }
            return false ;
        }
        public bool HasStraightFlush()
        {
            if (HasStraight() && HasFlush())
            {
                return true;
            }
            return false;
        }
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
    class Deck
    {
        private List<Card> _deckOfCards;
        public List<Card> DeckOfCards { get { return _deckOfCards; } set { _deckOfCards = value; } }
        private List<Card> _discardedCards = new List<Card>();
        public List<Card> DiscardedCards { get; set; }
        // a deck has 52 cards of each of suits with ranks
        public Deck()
        {
            DeckOfCards = new List<Card>();
            DiscardedCards = new List<Card>(); ;
            for (int r = 2; r <= 14; r++)
            {
                for (int s = 1; s <= 4; s++)
                {
                    DeckOfCards.Add(new Card((Rank)r, (Suit)s));
                }
            }
        }

        public Deck(int howMany)
        {
            for (int i = 0; i < howMany; i++)
            {
                new Deck();
            }
        }

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

        public void Discard(List<Card> hand)
        {
            while (hand.Count > 0)
            {
                DiscardedCards.Add(hand[0]);
                hand.RemoveAt(0);
            }
        }

        public void Shuffle()
        {
            List<Card> newDeckOfCards = new List<Card>();
            Random shuffler = new Random();
            // put the cards back to the DeckOfCards from the Discard piles
            if (DiscardedCards.Count == 0)
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
    class Card
    {
        private Suit _theSuit;
        public Suit TheSuit { get { return _theSuit; } set { _theSuit = value; } }
        private Rank _ranked;
        public Rank Ranked { get { return _ranked; } set { _ranked = value; } }

        public Card(Rank rank, Suit suit)
        {
            this.TheSuit = suit;
            this.Ranked = rank;
        }

        public Card(int rank, int suit)
        {
            this.TheSuit = (Suit)Enum.ToObject(typeof(Suit), suit);
            this.Ranked = (Rank)Enum.ToObject(typeof(Rank), rank);
        }

        public string GetCard()
        {
            return this.Ranked + " of " + this.TheSuit;
        }

    }
    //  *****Card Class End*******class Deck
}
