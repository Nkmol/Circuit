namespace Circuit
{
    using System;
    using Helpers;
    using Models;
    using Views;

    public class BoardController
    {
        private BoardParser boardParser;
        private BoardView boardView;
        private Board _board;

        public void LoadBoard()
        {
            var path = @"C:\Users\Sander\Desktop\Circuit1_FullAdder.txt";
            var reader = new FileReader(path);

            boardParser = new BoardParser();
            BoardBuilder bb = new BoardBuilder();

            try
            {
                foreach (var line in reader.ReadLine())
                {
                    if (boardParser.StartProbLinking)
                    {
                        var parserLink = boardParser.ParseLinkLine(line);
                        if(parserLink != null) bb.LinkList(parserLink.Varname, parserLink.Values);
                    }
                    else
                    {
                        var component = boardParser.ParseVariableLine(line);
                        if (component != null) bb.AddComponent(component.Varname, component.Compname, component.Input);
                    }

                    // TODO Improve this call
                    _board = bb.Build();
                }
            }
            catch (Exception e)
            {
                // TODO Move to view
                Console.WriteLine(e);
            }
        }

        public void StartSimulation()
         {
            _board.Calculate();
        }

        public void DrawBoard()
        {
            boardView = new BoardView(_board);
            boardView.Draw();
        }
    }
}