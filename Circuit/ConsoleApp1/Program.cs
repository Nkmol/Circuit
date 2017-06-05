namespace ConsoleApp1
{
    using System;
    using Circuit;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var controller = new BoardController();
            controller.LoadBoard();
            controller.StartSimulation();
            controller.DrawBoard();
            Console.ReadKey();
        }
    }
}