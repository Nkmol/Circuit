namespace ConsoleApp1
{
    using System;
    using Circuit;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var controller = new BoardController();
            controller.LoadBoard(@"C:\Users\srm\Desktop\Circuit1_FullAdder.txt");

            if (!controller.IsBoardConnected)
            {
                Console.WriteLine("This board is not minimally connected");
                Console.ReadKey();
                Environment.Exit(0);
            }

            //            controller.StartSimulation();
            foreach (var cycle in controller.StartSimulationYieldCycles())
            {
                Console.WriteLine($"--- {cycle.Name} of starting point {cycle.Start.Name} [{cycle.Number}] ---");
                foreach (var node in cycle)
                {
                    Console.WriteLine($"   {node.Name} = {node.Value}");
                }
                Console.WriteLine();
            }

            // Only know after cycling thru about all relations
            if (controller.Loops.Count > 0)
            {
                Console.WriteLine("This board contains invalid connections, resulting in a loop.");
                Console.WriteLine("The following connections have been ignored: ");
                foreach (var connection in controller.Loops)
                {
                    Console.WriteLine($"Contains a loop from {connection.From.Name} to {connection.To.Name}");
                }
            }

            Console.WriteLine("--- Board Summary ---");
            controller.DrawBoard();
            Console.WriteLine();

            Console.WriteLine("--- Output ----");
            foreach (var output in controller.Outputs)
            {
                Console.WriteLine($"{output.Name} = {output.Value}");
            }

            Console.ReadKey();
        }
    }
}