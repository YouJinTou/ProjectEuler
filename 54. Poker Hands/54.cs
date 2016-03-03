using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Card
{
    public Card(char rank, char suit)
    {
        this.Rank = rank;
        this.Suit = suit;
    }

    public char Rank { get; set; }
    public char Suit { get; set; }
}

class HandStrength
{
    public HandStrength()
    {
    }

    public HandStrength(Enum handRanking, int kickerStrength)
    {
        this.HandRanking = handRanking;
        this.KickerStrength = kickerStrength;
    }

    public Enum HandRanking { get; set; }
    public int KickerStrength { get; set; }
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

        DetermineWinner(playerOneHand, playerTwoHand);
    }

    public static void DetermineWinner(Card[] playerOneHand, Card[] playerTwoHand)
    {
        HandStrength playerOneHandStrength = EvaluateHandStrength(playerOneHand);
        HandStrength playerTwoHandStrength = EvaluateHandStrength(playerTwoHand);
        
        if (playerOneHandStrength.HandRanking.CompareTo(playerTwoHandStrength.HandRanking) > 0)
        {
            playerOneWins++;
        }
        else if (playerOneHandStrength.HandRanking.CompareTo(playerTwoHandStrength.HandRanking) == 0)
        {
            if (playerOneHandStrength.KickerStrength > playerTwoHandStrength.KickerStrength)
            {
                playerOneWins++;
            }
        }
    }

    public static HandStrength EvaluateHandStrength(Card[] hand)
    {
        var handStrength = new HandStrength();
        var handEvaluator = GetHandEvaluator(hand);

        switch (handEvaluator.Count)
        {
            case 2:
                if (handEvaluator.Any(e => e.Value == 1))
                {
                    handStrength.HandRanking = Rankings.FourOfAKind;
                    handStrength.KickerStrength = GetKickerStrength(handEvaluator, 1);

                    return handStrength;
                }
                else
                {
                    handStrength.HandRanking = Rankings.FullHouse;
                    handStrength.KickerStrength = GetKickerStrength(handEvaluator, 3);

                    return handStrength;
                }
            case 3:
                if (handEvaluator.Any(e => e.Value == 2))
                {
                    handStrength.HandRanking = Rankings.TwoPair;
                    handStrength.KickerStrength = GetKickerStrength(handEvaluator, 2);

                    return handStrength;
                }
                else
                {
                    handStrength.HandRanking = Rankings.ThreeOfAKind;
                    handStrength.KickerStrength = GetKickerStrength(handEvaluator, 3);

                    return handStrength;
                }
            case 4:
                handStrength.HandRanking = Rankings.OnePair;
                handStrength.KickerStrength = GetKickerStrength(handEvaluator, 2);

                return handStrength;
            case 5:
                {
                    bool isFlush = CardsAreOfSameSuit(hand);
                    bool isStraight = CardsFormAStraight(hand);

                    if (isFlush && isStraight)
                    {
                        if (hand.Any(c => c.Rank == 'T') && hand.Any(c => c.Rank == 'A'))
                        {
                            handStrength.HandRanking = Rankings.RoyalFlush;
                            handStrength.KickerStrength = GetKickerStrength(handEvaluator, 0);

                            return handStrength;
                        }
                        else
                        {
                            handStrength.HandRanking = Rankings.StraightFlush;
                            handStrength.KickerStrength = GetKickerStrength(handEvaluator, 1);

                            return handStrength;
                        }
                    }
                    else if (isFlush)
                    {
                        handStrength.HandRanking = Rankings.Flush;
                        handStrength.KickerStrength = GetKickerStrength(handEvaluator, 1);

                        return handStrength;
                    }
                    else if (isStraight)
                    {
                        handStrength.HandRanking = Rankings.Straight;
                        handStrength.KickerStrength = GetKickerStrength(handEvaluator, 1);

                        return handStrength;
                    }

                    break;
                }
        }

        handStrength.HandRanking = Rankings.HighCard;
        handStrength.KickerStrength = GetKickerStrength(handEvaluator, 1);

        return handStrength;
    }

    public static Dictionary<char, int> GetHandEvaluator(Card[] hand)
    {
        var handEvaluator = new Dictionary<char, int>();

        foreach (var card in hand)
        {
            if (!handEvaluator.ContainsKey(card.Rank))
            {
                handEvaluator[card.Rank] = 1;
            }
            else
            {
                handEvaluator[card.Rank]++;
            }
        }

        return handEvaluator;
    }

    public static bool CardsAreOfSameSuit(Card[] hand)
    {
        var firstCardSuit = hand[0].Suit;

        for (int i = 1; i < hand.Length; i++)
        {
            if (firstCardSuit != hand[i].Suit)
            {
                return false;
            }
        }

        return true;
    }

    public static bool CardsFormAStraight(Card[] hand)
    {
        char[][] possibleStraights = new char[][]
        {
            new char[] { 'A', '2', '3', '4', '5' },
            new char[] { '2', '3', '4', '5', '6' },
            new char[] { '3', '4', '5', '6', '7' },
            new char[] { '4', '5', '6', '7', '8' },
            new char[] { '5', '6', '7', '8', '9' },
            new char[] { '6', '7', '8', '9', 'T' },
            new char[] { '7', '8', '9', 'T', 'J' },
            new char[] { '8', '9', 'T', 'J', 'Q' },
            new char[] { '9', 'T', 'J', 'Q', 'K' },
            new char[] { 'T', 'J', 'Q', 'K', 'A' }
        };
        bool isAStraight = true;

        foreach (var straight in possibleStraights)
        {
            isAStraight = true;

            foreach (var cardRank in straight)
            {
                if (!hand.Any(c => c.Rank == cardRank))
                {
                    isAStraight = false;
                    break;
                }
            }
        }

        return isAStraight;
    }

    public static int GetKickerStrength(Dictionary<char, int> handEvaluator, int check)
    {
        int bestResult = 0;
        int currentBest = 0;

        foreach (var entry in handEvaluator)
        {
            if (entry.Value != check)
            {
                continue;
            }

            switch (entry.Key)
            {
                case 'T':
                    currentBest = 58;
                    break;
                case 'J':
                    currentBest = 59;
                    break;
                case 'Q':
                    currentBest = 60;
                    break;
                case 'K':
                    currentBest = 61;
                    break;
                case 'A':
                    currentBest = 62;
                    break;
                default:
                    currentBest = entry.Key;
                    break;
            }

            if (bestResult < currentBest)
            {
                bestResult = currentBest;
            }
        }

        return bestResult;
    }

    static void Main()
    {
        playerOneWins = 0;

        ReadInput();

        Console.WriteLine("Player one's total wins: {0}", playerOneWins);
    }
}
