using System;
using System.Collections.Generic;

class Program
{
    private static readonly string[] primeSet = { "3", "7", "109", "673" };

    private static long fifthPrime = 0;
    private static List<long> primesList = new List<long>() { 2 };
    private static HashSet<long> primesHashset;

    static void Main()
    {
        GeneratePrimes();

        primesHashset = new HashSet<long>(primesList);

        foreach (var prime in primesList)
        {
            if (ConcatenationPrimalityExists(prime))
            {
                fifthPrime = prime;
                break;
            }
        }

        Console.WriteLine(fifthPrime);
    }

    static void GeneratePrimes()
    {
        for (int i = 3; i < 100000000; i += 2)
        {
            if (IsPrime(i))
            {
                primesList.Add(i);
            }
        }
    }

    static bool IsPrime(int n)
    {
        var upperBound = (int)Math.Sqrt(n);

        for (int i = 0; i < upperBound; i++)
        {
            if (n % primesList[i] == 0)
            {
                return false;
            }
        }

        return true;
    }

    static bool ConcatenationPrimalityExists(long primeToCheck)
    {
        string primeToCheckAsString = primeToCheck.ToString();

        foreach (var prime in primeSet)
        {
            long result = long.Parse(prime + primeToCheckAsString);
            long inverseResult = long.Parse(primeToCheckAsString + prime);

            if (!primesHashset.Contains(result) || !primesHashset.Contains(inverseResult))
            {
                return false;
            }
        }

        return true;
    }
}