using Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Circuit
{
    using Models;
    using Views;

    public class BoardController
    {
        private BoardParser boardParser =  null;
        private BoardView boardView = null;

        public BoardController()
        {
            
        }

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
            boardView = new BoardView(boardParser.Board);
            boardView.Draw();
        }
    }
}
