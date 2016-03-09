using System;
using System.IO;
using System.Numerics;

class Program
{
    static BigInteger SumNumbers()
    {
        BigInteger result = 0;
        var fileName = "100 numbers.txt";

        using (var sr = new StreamReader(fileName))
        {
            string line = sr.ReadLine();

            while (line != null)
            {
                result += BigInteger.Parse(line);

                line = sr.ReadLine();
            }
        }

        return result;
    }

    static void Main()
    {
        var sum = SumNumbers().ToString();

        for (int i = 0; i < 10; i++)
        {
            Console.Write(sum[i]);
        }

        Console.WriteLine();
    }
}