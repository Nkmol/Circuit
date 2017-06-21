using System;
using System.Linq;
using System.Windows.Forms;
using Circuit;
using Datatypes.DirectedGraph;

namespace ConsoleApp1
{
    internal class Program
    {
        private static BoardController _controller;

        [STAThread]
        private static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Circuit simulator");
            Console.WriteLine("Please first select a valid circuit file for us to use.");
            Console.WriteLine();
            Console.WriteLine("Press a key to continue");
            Console.ReadKey(true);

            var file = GetFile();
            DrawBoard(file);

            while (true)
            {
                ShowOptions();
                var btn = Console.ReadKey(true);
                Console.Clear();

                switch (btn.Key)
                {
                    case ConsoleKey.R:
                        DrawBoard(file);
                        break;
                    case ConsoleKey.F:
                        file = GetFile();
                        DrawBoard(file);
                        break;
                    case ConsoleKey.D:
                        CreateDiagram();
                        break;
                    default:
                        break;
                }
            }
        }

        private static void ShowOptions()
        {
            Console.WriteLine();
            Console.WriteLine("The following options are available");
            Console.WriteLine("  r = To restart the board simulation.");
            Console.WriteLine("  f = To choose another board file.");
            Console.WriteLine("  d = To save a diagram to the chosen location.");
        }

        private static void CreateDiagram()
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "Directed Graph Markup Language (*.dgml)|*.dgml";
            var path = string.Empty;

            if (dialog.ShowDialog() == DialogResult.OK)
                path = dialog.FileName;

            _controller.CreateDiagram(path);

            Console.WriteLine("The file has been saved at: " + path);
        }

        private static string GetFile()
        {
            // Ask for file
            var file = "";
            var dialog = new OpenFileDialog();
            dialog.Filter = "Text (*.txt)|*.txt";
            if (dialog.ShowDialog() == DialogResult.OK)
                file = dialog.FileName;
            else
                Environment.Exit(0);

            return file;
        }

        private static void DrawBoard(string file)
        {
            Console.WriteLine("Selected file: " + file); // file name
            Console.WriteLine();

            _controller = new BoardController();
            _controller.LoadBoard(file);

            if (!_controller.IsBoardConnected)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This board is not minimally connected");
                Console.ReadKey(true);
                Environment.Exit(0);
            }

            DrawCycles();

            // Only know after cycling thru
            if (_controller.Loops.Count > 0)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This board contains invalid connections, resulting in a loop.");
                Console.WriteLine("The following connections have been ignored: ");
                foreach (var connection in _controller.Loops)
                {
                    Console.WriteLine($"Contains a loop from {connection.From.Name} to {connection.To.Name}");
                }
                Console.WriteLine();
                Console.ResetColor();
            }

            Console.WriteLine("--- Board Summary ---");
            _controller.DrawBoard();
            Console.WriteLine();

            Console.WriteLine("--- Output ----");
            var bitSum = "";
            foreach (var output in _controller.Outputs)
            {
                Console.WriteLine($"{output.Name} = {output.Value}");
                bitSum += ((int) output.Value).ToString();
            }
            Console.WriteLine($"Result: {bitSum}");
        }

        private static void DrawCycles()
        {
            //            controller.StartSimulation();
            foreach (var cycle in _controller.StartSimulationYieldCycles())
            {
                if (!(cycle.Last() is ILeaf))
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine($"The following cycle was not connected to a Probe");
                }

                Console.WriteLine($"--- {cycle.Name} of starting point {cycle.Start.Name} [{cycle.Number}] ---");
                foreach (var node in cycle)
                    Console.WriteLine($"   {node.Name} = {node.Value}");
                Console.WriteLine();
                Console.ResetColor();
            }
        }
    }
}