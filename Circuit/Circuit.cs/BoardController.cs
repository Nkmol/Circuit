using System.Collections.Generic;
using System.Linq;
using Datatypes.DirectedGraph;
using Helpers;
using Helpers.DGML;
using Models;
using Views;

namespace Circuit
{
    public class BoardController
    {
        private Board _board;
        private BoardView _boardView;

        public List<Probe> Outputs => _board.Probes.Select(x => (Probe) x).ToList();

        // Checks
        public bool IsBoardConnected => _board.IsConnected;

        public List<Edge<Component>> Loops => _board.Components.BackEdges.ToList();

        public static string DiagramExtension => DgmlWriter.Extension;

        public void LoadBoard(string path)
        {
            _board = CreateBoard(path);
        }

        private Board CreateBoard(string path)
        {
            var reader = new FileReader(path);

            var boardParser = new BoardParser();
            var bb = new BoardBuilder();

            foreach (var line in reader.ReadLine())
            {
                var parsedLine = boardParser.Parse(line);
                if (parsedLine == null)
                    continue;

                if (boardParser.StartProbLinking)
                {
                    var compname = parsedLine[0];
                    bb.LinkList(compname, parsedLine.Skip(1).ToList());
                }
                else
                {
                    string varname = parsedLine[0], compname = parsedLine[1], input = parsedLine[2];
                    if (compname.ToLower() == "board")
                        bb.AddBoard(varname, CreateBoard(input));
                    else
                        bb.AddComponent(varname, compname, input);
                }
            }

            return bb.Build();
        }

        public IEnumerable<Cycle<Component>> StartSimulationYieldCycles()
        {
            foreach (var cycle in _board.Cycle())
            {
                foreach (var node in cycle)
                    node.Calculate();
                yield return cycle;
            }
        }

        public void StartSimulation()
        {
            _board.Calculate();
        }

        public void DrawBoard()
        {
            _boardView = new BoardView(_board);
            _boardView.Draw();
        }

        public void CreateDiagram(string path)
        {
            var parser = new BoardDgmlParser();

            // Get all links
            var edges = new List<Edge<Component>>();
            foreach (var cycle in _board.Cycle())
                edges.AddRange(parser.ParseCyclesToEdge(cycle));

            parser.Parse(new BoardDgmlStrategy(), edges);

            var dgmlWriter = new DgmlWriter();
            dgmlWriter.Nodes = parser.Nodes;
            dgmlWriter.Links = parser.Links;

            dgmlWriter.Serialize(path);
        }
    }
}