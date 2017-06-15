namespace ConsoleApp1
{
    using System;
    using Circuit;
    using Datatypes;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var controller = new BoardController();
            controller.LoadBoard(@"C:\Users\srm\Desktop\Circuit1_FullAdder.txt");
            foreach (var cycle in controller.StartSimulationYieldCycles())
            {
                Console.WriteLine($"--- {cycle.Name} of starting point {cycle.Start.Name} [{cycle.Number}] ---");
                foreach (var node in cycle)
                {
                    Console.WriteLine($"   {node.Name} = {node.Value}");
                }
                Console.WriteLine();
            }

//            controller.StartSimulation();
            Console.WriteLine("--- Board Summary ---");
            controller.DrawBoard();

            Console.ReadKey();
        }
    }
}