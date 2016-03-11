using System;
using System.Diagnostics;

class Cell
{
    public Cell(int row, int col)
    {
        this.Row = row;
        this.Col = col;
        this.IsVisited = false;
        this.IsExit = false;
    }

    public int Row { get; set; }
    public int Col { get; set; }
    public bool IsVisited { get; set; }
    public bool IsExit { get; set; }
}

class Program
{
    private const int Side = 21;
    private static Cell[,] matrix = new Cell[Side, Side];
    private static ulong pathCount = 0;

    private static void InitializeMatrix()
    {
        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                matrix[row, col] = new Cell(row, col);
            }
        }

        matrix[Side - 1, Side - 1].IsExit = true;
    }

    private static void TraverseDFS(int row, int col)
    {
        if (row >= matrix.GetLength(0) || col >= matrix.GetLength(1))
        {
            return;
        }

        if (matrix[row, col].IsExit)
        {
            pathCount++;

            return;
        }

        if (matrix[row, col].IsVisited)
        {
            return;
        }

        matrix[row, col].IsVisited = true;

        TraverseDFS(row + 1, col);
        TraverseDFS(row, col + 1);

        matrix[row, col].IsVisited = false;
    }

    static void Main()
    {
        InitializeMatrix();

        Stopwatch sw = new Stopwatch();

        sw.Start();

        TraverseDFS(0, 0);

        sw.Stop();

        Console.WriteLine("Total paths: {0}", pathCount);
        Console.WriteLine("Time elapsed: {0}", sw.Elapsed);
    }
}