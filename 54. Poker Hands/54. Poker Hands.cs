using System;
using System.Collections.Generic;
using System.IO;

public class Card
{
    public Card(char rank, char suit)
    {
        this.Rank = rank;
        this.Suit = suit;
    }

    public char Rank { get; set; }
    public char Suit { get; set; }
}

class PokerTable
{
    public enum Rankings
    {
        RoyalFlush = 10,
        StraightFlush = 9,
        FourOfAKind = 8,
        FullHouse = 7,
        Flush = 6,
        Straight = 5,
        ThreeOfAKind = 4,
        TwoPair = 3,
        OnePair = 2,
        HighCard = 1
    }

    public static int playerOneWins;

    public static void ReadInput()
    {
        string fileName = "input.txt";
        string path = Path.Combine(Environment.CurrentDirectory, fileName);

        using (StreamReader sr = new StreamReader(path))
        {
            string currentLine = "";

            while ((currentLine = sr.ReadLine()) != null)
            {
                ParseHands(currentLine);
            }
        }
    }

    public static void ParseHands(string currentHand)
    {
        string[] cards = currentHand.Split(' ');
        Card[] playerOneHand = new Card[5];
        Card[] playerTwoHand = new Card[5];

        for (int currentCard = 0; currentCard < cards.Length; currentCard++)
        {
            char rank = cards[currentCard][0];
            char suit = cards[currentCard][1];
            Card card = new Card(rank, suit);

            if (currentCard < 5)
            {
                playerOneHand[currentCard] = card;
            }
            else
            {
                int playerTwoCurrentCard = currentCard - 5;

                playerTwoHand[playerTwoCurrentCard] = card;
            }
        }

        var hand = new Card[2][];

        hand[0] = playerOneHand;
        hand[1] = playerTwoHand;

        DetermineWinner(hand);
    }

    public static void DetermineWinner(Card[][] hands)
    {
        Enum playerOneHandStrength = EvaluateHandStrength(hands[0]);
        Enum playerTwoHandStrength = EvaluateHandStrength(hands[1]);

        if (playerOneHandStrength.CompareTo(playerTwoHandStrength) == 1)
        {
            playerOneWins++;
        }
    }

    public static Enum EvaluateHandStrength(Card[] hand)
    {
        if (IsRoyalFlush(hand))
        {
            return Rankings.RoyalFlush;
        }
        else if (IsStraightFlush(hand))
        {
            return Rankings.StraightFlush;
        }
        else if (IsFourOfAKind(hand))
        {
            return Rankings.FourOfAKind;
        }
        else if (IsFullHouse(hand))
        {
            return Rankings.FullHouse;
        }
        else if (IsFlush(hand))
        {
            return Rankings.Flush;
        }
        else if (IsStraight(hand))
        {
            return Rankings.Straight;
        }
        else if (IsThreeOfAKind(hand))
        {
            return Rankings.ThreeOfAKind;
        }
        else if (IsTwoPair(hand))
        {
            return Rankings.TwoPair;
        }
        else if (IsOnePair(hand))
        {
            return Rankings.OnePair;
        }

        return Rankings.HighCard;
    }

    private static bool IsOnePair(Card[] hand)
    {
        throw new NotImplementedException();
    }

    private static bool IsTwoPair(Card[] hand)
    {
        throw new NotImplementedException();
    }

    private static bool IsThreeOfAKind(Card[] hand)
    {
        throw new NotImplementedException();
    }

    private static bool IsStraight(Card[] hand)
    {
        throw new NotImplementedException();
    }

    private static bool IsFlush(Card[] hand)
    {
        throw new NotImplementedException();
    }

    private static bool IsFullHouse(Card[] hand)
    {
        throw new NotImplementedException();
    }

    private static bool IsFourOfAKind(Card[] hand)
    {
        throw new NotImplementedException();
    }

    public static bool IsStraightFlush(Card[] hand)
    {
        /throw new NotImplementedException();
    }

    public static bool IsRoyalFlush(Card[] hand)
    {
        var royalStraight = new HashSet<char>() { 'T', 'J', 'Q', 'K', 'A' };
        char firstSuit = hand[0].Suit;

        for (int i = 0; i < hand.Length; i++)
        {
            char currentSuit = hand[i].Suit;

            if (currentSuit != firstSuit || !royalStraight.Contains(hand[i].Rank))
            {
                return false;
            }

            royalStraight.Remove(hand[i].Rank);
        }

        return true;
    }    

    static void Main()
    {
        playerOneWins = 0;

        ReadInput();
    }
}
