using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Program
{
    static bool[] GetPrimes(int limit)
    {
        bool[] primes = new bool[limit];
        primes[0] = false;
        primes[1] = false;

        for (int i = 2; i < limit; i++)
        {
            primes[i] = true;
        }

        for (long i = 2; i < limit; i++)
        {
            if (primes[i])
            {
                for (long j = i * 2; j < limit; j += i)
                    primes[j] = false;
            }
        }

        return primes;
    }

    static void Main()
    {
        int limit = 2000000;
        var primes = GetPrimes(limit);
        ulong sum = 0;

        for (int i = 0; i < primes.Length; i++)
        {
            if (primes[i])
            {
                sum += (ulong)i;
            }
        }

        Console.WriteLine("The sum is {0}.", sum);
    }
}