using System;

class PrimeCounter
{
    public static bool IsPrime(int n)
    {
        int bound = (int)Math.Sqrt(n);

        for (int i = 3; i <= bound; i += 2)
        {
            if (n % i == 0)
            {
                return false;
            }
        }

        return true;
    }

    static void Main()
    {
        int primeCount = 1;
        int currentNumber = 3;

        while (primeCount < 10001)
        {
            if (IsPrime(currentNumber))
            {
                primeCount++;
            }

            currentNumber += 2;
        }

        Console.WriteLine("The 10 001st prime is {0}", currentNumber - 2);
    }
}