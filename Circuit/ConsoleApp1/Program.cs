namespace ConsoleApp1
{
    using System;
    using System.IO;
    using System.Windows.Forms;
    using Circuit;

    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            // TODO Option for restarting

            Console.WriteLine("Welcome to the Circuit simulator");
            Console.WriteLine("Please first select a valid circuit file for us to use.");
            Console.WriteLine();
            Console.WriteLine("Press a key to continue");
            Console.ReadKey();

            // Ask for file
            var file = "";
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Console.Clear();
                Console.WriteLine("Selected file: " + dialog.FileName); // file name
                Console.WriteLine();

                file = dialog.FileName;
            }
            else
            {
                Environment.Exit(0);
            }

            var controller = new BoardController();
            controller.LoadBoard(file);

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