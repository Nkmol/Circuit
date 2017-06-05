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
        private BoardParser _boardParser =  null;
        private BoardView _boardView = null;
        private Board _board = null;

        public BoardController()
        {
            
        }

        public void LoadBoard()
        {
            var path = @"C:\Users\srm\Desktop\Circuit1_FullAdder.txt";
            var reader = new FileReader(path);

            _boardParser = new BoardParser();

            try
            {
                foreach (var line in reader.ReadLine())
                {
                    _boardParser.Parse(line);
                }

            }
            catch (Exception e)
            {
                // TODO Move to view
                Console.WriteLine(e);
            }

            _board = new Board(_boardParser.Nodes);
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
            _boardView = new BoardView(_board);
            _boardView.Draw();
        }
    }
}
