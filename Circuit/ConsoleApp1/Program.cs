namespace ConsoleApp1
{
    using System;
    using Circuit;

    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var controller = new BoardController();
                controller.LoadBoard(@"C:\Users\Sander\Desktop\Circuit1_FullAdder.txt");
                controller.StartSimulation();
                controller.DrawBoard();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }
    }
}