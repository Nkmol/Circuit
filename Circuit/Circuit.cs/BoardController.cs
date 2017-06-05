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
                // TODO Move to view
                Console.WriteLine(e);
            }

            // TODO Improve this call
            _board = boardParser.BoardBuilder.Board;
        }

        public void StartSimulation()
         {
             try
             {
                 _board.Start();
             }
             catch (Exception e)
             {
                 // TODO Move to view
                 Console.WriteLine(e.Message);
             }
          }

        public void DrawBoard()
        {
            boardView = new BoardView(_board);
            boardView.Draw();
        }
    }
}