namespace Circuit
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Datatypes.DirectedGraph;
    using Helpers;
    using Models;
    using Views;

    public class BoardController
    {
        private BoardParser _boardParser;
        private BoardView _boardView;
        private Board _board;

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
                if (boardParser.StartProbLinking)
                {
                    var parserLink = boardParser.ParseLinkLine(line);
                    if (parserLink != null) bb.LinkList(parserLink.Varname, parserLink.Values);
                }
                else
                {
                    var component = boardParser.ParseVariableLine(line);
                    if (component != null)
                    {
                        if (component.Compname.ToLower() == "board")
                        {
                            bb.AddBoard(component.Varname, CreateBoard(component.Input));
                        }
                        else
                        {
                            bb.AddComponent(component.Varname, component.Compname, component.Input);
                        }
                    }
                }
            }

            return bb.Build();
        }

        public IEnumerable<Cycle<Component>> StartSimulationYieldCycles()
        {
            foreach (var cycle in _board.Cycle())
            {
                foreach (var node in cycle)
                {
                    node.Calculate();
                }
                yield return cycle;
            }

            _board.Calculate();
        }

        public void StartSimulation()
        {
            _board.Cycle();
            _board.Calculate();
        }

        public void DrawBoard()
        {
            _boardView = new BoardView(_board);
            _boardView.Draw();
        }
    }
}