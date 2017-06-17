using System.Collections.Generic;
using System.Linq;
using Datatypes.DirectedGraph;
using Helpers;
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

        public static string DiagramExtension => DGMLWriter.Extension;

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
                if (boardParser.StartProbLinking)
                {
                    var parserLink = boardParser.ParseLinkLine(line);
                    if (parserLink != null) bb.LinkList(parserLink.Varname, parserLink.Values);
                }
                else
                {
                    var component = boardParser.ParseVariableLine(line);
                    if (component != null)
                        if (component.Compname.ToLower() == "board")
                            bb.AddBoard(component.Varname, CreateBoard(component.Input));
                        else
                            bb.AddComponent(component.Varname, component.Compname, component.Input);
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
            // Get all links
            var links = new List<Edge<Component>>();
            foreach (var cycle in _board.Cycle())
            {
                Component prev = null;
                foreach (var node in cycle)
                {
                    if (prev != null)
                        links.Add(new Edge<Component>(prev, node));

                    prev = node;
                }
            }

            var uniqueLinks = links.Distinct();
            var uniqueComp = links.Select(x => x.From).GroupBy(x => x.Name, (key, group) => group.First());

            var dgmlWriter = new DGMLWriter();
            uniqueLinks.Select(x => new DGMLWriter.Link(x.From.Name, x.To.Name, "")).ToList()
                .ForEach(x => dgmlWriter.AddLink(x));
            uniqueComp.Select(x => new DGMLWriter.Node(x.Name, $"{x.GetType().Name}[{x.Value}]")).ToList()
                .ForEach(x => dgmlWriter.AddNode(x));

            dgmlWriter.Serialize(path);
        }
    }
}