using System;
using System.Collections.Generic;

class Program
{
    static List<long> triangularNumbers = new List<long>();
    static List<long> primes = new List<long>();
    static HashSet<long> primesSet = new HashSet<long>();

    static void GetTriangularNumbers()
    {
        var previousTriangularNumber = 0;

        for (int i = 1; i <= 1000000; i++)
        {
            var currentTriangularNumber = previousTriangularNumber + i;

            triangularNumbers.Add(currentTriangularNumber);

            previousTriangularNumber = currentTriangularNumber;
        }
    }

    static void GetPrimes(int limit)
    {
        bool[] primesTemp = new bool[limit + 1];
        primesTemp[0] = false;
        primesTemp[1] = false;

        for (int i = 2; i <= limit; i++)
        {
            primesTemp[i] = true;
        }

        for (long i = 2; i <= limit; i++)
        {
            if (primesTemp[i])
            {
                for (long j = i * 2; j <= limit; j += i)
                    primesTemp[j] = false;
            }
        }

        for (int i = 0; i < limit; i++)
        {
            if (primesTemp[i])
            {
                primes.Add(i);
                primesSet.Add(i);
            }
        }
    }

    static Dictionary<long, int> GetFactors(long number)
    {
        Dictionary<long, int> factors = new Dictionary<long, int>();

        if (number % 2 == 0)
        {
            factors.Add(2, 0);

            while (number % 2 == 0)
            {
                factors[2]++;

                number /= 2;
            }
        }

        long bound = (long)Math.Sqrt(number);

        for (int i = 1; i < bound; i++)
        {
            var divisor = primes[i];

            if (divisor > bound)
            {
                break;
            }

            if (number % divisor == 0)
            {
                factors.Add(divisor, 0);

                while (number % divisor == 0)
                {
                    factors[divisor]++;

                    number /= divisor;
                }
            }
        }

        if (number > 2)
        {
            factors.Add(number, 1);
        }

        return factors;
    }

    static bool HasMoreThanFiveHundredDivisors(long number)
    {
        var factors = GetFactors(number);

        if (factors == null)
        {
            return false;
        }

        var result = 1;

        foreach (var factor in factors)
        {
            result *= (factor.Value + 1);
        }

        if (result > 500)
        {
            return true;
        }

        return false;
    }

    static void Main()
    {
        GetTriangularNumbers();
        GetPrimes(2000000);

        foreach (var triangularNumber in triangularNumbers)
        {
            if (HasMoreThanFiveHundredDivisors(triangularNumber))
            {
                Console.WriteLine("The first such number is {0}.", triangularNumber);

                break;
            }
        }
    }
}