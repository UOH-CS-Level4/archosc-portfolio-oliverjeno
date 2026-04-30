
using System;

class Program
{
    static void Main()
    {
        Console.Write("Number of processes: ");
        int processes = int.Parse(Console.ReadLine());

        Console.Write("Number of resource types: ");
        int resources = int.Parse(Console.ReadLine());

        int[,] allocation = new int[processes, resources];
        int[,] max = new int[processes, resources];
        int[,] need = new int[processes, resources];
        int[] available = new int[resources];
        bool[] finished = new bool[processes];
        int[] safeSequence = new int[processes];

        
        Console.WriteLine("Enter allocation matrix (with a space between):");
        for (int i = 0; i < processes; i++)
        {
            Console.Write("P" + (i + 1) + ": ");
            string[] input = Console.ReadLine().Split(' ');

            for (int j = 0; j < resources; j++)
            {
                allocation[i, j] = int.Parse(input[j]);
            }
        }

       
        Console.WriteLine("Enter maximum demand matrix (with a space between):");
        for (int i = 0; i < processes; i++)
        {
            Console.Write("P" + (i + 1) + ": ");
            string[] input = Console.ReadLine().Split(' ');

            for (int j = 0; j < resources; j++)
            {
                max[i, j] = int.Parse(input[j]);
                need[i, j] = max[i, j] - allocation[i, j];
            }
        }

       
        Console.WriteLine("Enter available resources (with a space between):");
        string[] availInput = Console.ReadLine().Split(' ');
        for (int j = 0; j < resources; j++)
        {
            available[j] = int.Parse(availInput[j]);
        }

        int count = 0;

        while (count < processes)
        {
            bool foundProcess = false;

            for (int i = 0; i < processes; i++)
            {
                if (!finished[i])
                {
                    bool canRun = true;

                    for (int j = 0; j < resources; j++)
                    {
                        if (need[i, j] > available[j])
                        {
                            canRun = false;
                            break;
                        }
                    }

                    if (canRun)
                    {
                        for (int j = 0; j < resources; j++)
                        {
                            available[j] += allocation[i, j];
                        }

                        safeSequence[count] = i;
                        finished[i] = true;
                        count++;
                        foundProcess = true;
                    }
                }
            }

            if (!foundProcess)
            {
                Console.WriteLine("System is not in a safe state.");
                return;
            }
        }

        Console.Write("Safe Sequence: ");
        for (int i = 0; i < processes; i++)
        {
            Console.Write("P" + (safeSequence[i] + 1));
            if (i < processes - 1)
                Console.Write(" -> ");
        }

        Console.WriteLine();
        Console.WriteLine("System is in a safe state ");
        Console.Read(); 
    }
}