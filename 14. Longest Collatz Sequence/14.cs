using System;
using System.Collections.Generic;

class Program
{
    static int GetCollatzSequenceTermCount(ulong firstElement)
    {
        Queue<ulong> collatzSequence = new Queue<ulong>();
        int count = 0;

        collatzSequence.Enqueue(firstElement);

        while (collatzSequence.Count != 0)
        {
            ulong currentElement = collatzSequence.Dequeue();

            if (currentElement == 1)
            {
                count++;

                break;
            }

            if (currentElement % 2 == 0)
            {
                collatzSequence.Enqueue(currentElement / 2);
            }
            else
            {
                collatzSequence.Enqueue(currentElement * 3 + 1);
            }

            count++;
        }

        return count;
    }

    static void Main()
    {
        int bestCount = 0;

        for (ulong i = 1; i < 1000000; i++)
        {
            int currentCount = GetCollatzSequenceTermCount(i);

            if (currentCount > bestCount)
            {
                bestCount = currentCount;
            }
        }

        Console.WriteLine(
            "The number of terms for the longest Collatz sequence starting at under one million is {0}.", bestCount);
    }
}