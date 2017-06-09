namespace Circuit
{
    using System;
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
            _board = Board.Create(path);
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
    }
}