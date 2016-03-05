using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    static readonly long limit = (long)Math.Sqrt(Math.Pow(10, 12));

    static List<long> primes = new List<long>();
    static HashSet<long> primesSet = new HashSet<long>();
    static HashSet<long> octoDivisible = new HashSet<long>();
    static HashSet<long> progressionSet = new HashSet<long>();

    static void GetPrimes()
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

            if (factors[2] == 2)
            {
                return null;
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

                if (factors[divisor] == 2)
                {
                    return null;
                }
            }
        }

        if (number > 2)
        {
            factors.Add(number, 1);
        }

        return factors;
    }

    static bool HasEightDivisors(long number)
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

        if (result == 8)
        {
            return true;
        }

        return false;
    }

    static void FillProgressionSet(long currentLimit, long number)
    {
        try
        {
            var bound = currentLimit / number;

            for (int i = 2; i < bound + 2; i++)
            {
                var result = number * i;

                progressionSet.Add(result);
            }
        }
        catch (OutOfMemoryException)
        {
            progressionSet.RemoveWhere(n => n < number);
        }        
    }

    static void Main()
    {
        GetPrimes();

        long fn = (long)Math.Pow(10, 7);
        Stopwatch sw = new Stopwatch();

        sw.Start();

        for (long currentNumber = 1; currentNumber <= fn; currentNumber++)
        {
            if (primesSet.Contains(currentNumber) || progressionSet.Contains(currentNumber))
            {
                continue;
            }

            if (HasEightDivisors(currentNumber))
            {
                octoDivisible.Add(currentNumber);
                FillProgressionSet(fn, currentNumber);
            }
        }

        sw.Stop();

        Console.WriteLine("Total count: " + octoDivisible.Count);
        Console.WriteLine("Time elapsed: " + sw.Elapsed);
    }
}