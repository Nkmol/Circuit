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

        public void LoadBoard()
        {
            var path = @"C:\Users\srm\Desktop\Circuit1_FullAdder.txt";
            var reader = new FileReader(path);

            boardParser = new BoardParser();

            try
            {
                foreach (var line in reader.ReadLine())
                {
                    boardParser.Parse(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void DrawBoard()
        {
            var board = new Board(boardParser.Nodes);
            boardView = new BoardView(board);
            boardView.Draw();
        }
    }
}