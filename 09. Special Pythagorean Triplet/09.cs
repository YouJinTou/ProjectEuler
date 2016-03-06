using System;

class Program
{
    static void GenerateTriplets()
    {
        for (int a = 1; a < 998; a++)
        {
            for (int b = 2; b < 998; b++)
            {
                if (a >= b)
                {
                    continue;
                }

                for (int c = 3; c < 998; c++)
                {
                    if (b >= c)
                    {
                        continue;
                    }

                    var abSquared = Math.Pow(a, 2) + Math.Pow(b, 2);
                    var cSquared = Math.Pow(c, 2);

                    if (cSquared > abSquared)
                    {
                        break;
                    }
                    else if (abSquared == cSquared)
                    {
                        if (a + b + c == 1000)
                        {
                            var result = a * b * c;

                            Console.WriteLine("The product is {0}.", result);

                            return;
                        }
                    }
                }
            }
        }
    }

    static void Main()
    {
        GenerateTriplets();
    }
}
