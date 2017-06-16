namespace ConsoleApp1
{
    using System;
    using System.IO;
    using System.Windows.Forms;
    using Circuit;

    internal class Program
    {
        private static BoardController _controller;

        [STAThread]
        private static void Main(string[] args)
        {
            // TODO Option for restarting

            Console.WriteLine("Welcome to the Circuit simulator");
            Console.WriteLine("Please first select a valid circuit file for us to use.");
            Console.WriteLine();
            Console.WriteLine("Press a key to continue");
            Console.ReadKey();

            var file = GetFile();
            DrawBoard(file);
            var btn = Console.ReadKey(true);

            while (true)
            {
                switch (btn.Key)
                {
                    case ConsoleKey.R:
                        DrawBoard(file);
                        break;
                    case ConsoleKey.F:
                        GetFile();
                        DrawBoard(file);
                        break;
                    case ConsoleKey.D:
                        CreateDiagram();
                        break;
                    default:
                        Environment.Exit(0);
                        break;
                }

                btn = Console.ReadKey();
            }
        }

        private static void CreateDiagram()
        {
            Console.Clear();
            var path = @"C:\Users\srm\Downloads";
            _controller.CreateDiagram(path);

            Console.WriteLine("The file has been saved at: " + path);
        }

        private static string GetFile()
        {
            // Ask for file
            var file = "";
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                file = dialog.FileName;
            }
            else
            {
                Environment.Exit(0);
            }

            return file;
        }

        private static void DrawBoard(string file)
        {
            Console.Clear();
            Console.WriteLine("Selected file: " + file); // file name
            Console.WriteLine();

            _controller = new BoardController();
            _controller.LoadBoard(file);

            if (!_controller.IsBoardConnected)
            {
                Console.WriteLine("This board is not minimally connected");
                Console.ReadKey();
                Environment.Exit(0);
            }

            //            controller.StartSimulation();
            foreach (var cycle in _controller.StartSimulationYieldCycles())
            {
                Console.WriteLine($"--- {cycle.Name} of starting point {cycle.Start.Name} [{cycle.Number}] ---");
                foreach (var node in cycle)
                {
                    Console.WriteLine($"   {node.Name} = {node.Value}");
                }
                Console.WriteLine();
            }

            // Only know after cycling thru about all relations
            if (_controller.Loops.Count > 0)
            {
                Console.WriteLine("This board contains invalid connections, resulting in a loop.");
                Console.WriteLine("The following connections have been ignored: ");
                foreach (var connection in _controller.Loops)
                {
                    Console.WriteLine($"Contains a loop from {connection.From.Name} to {connection.To.Name}");
                }
            }

            Console.WriteLine("--- Board Summary ---");
            _controller.DrawBoard();
            Console.WriteLine();

            Console.WriteLine("--- Output ----");
            foreach (var output in _controller.Outputs)
            {
                Console.WriteLine($"{output.Name} = {output.Value}");
            }
        }
    }
}