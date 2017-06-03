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
            controller.DrawBoard();
            Console.ReadKey();
        }
    }
}