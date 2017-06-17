using System;
using System.Collections.Generic;
using System.Security.Policy;

namespace Models
{
    public class BoardBuilder : IBuilder<Board>
    {
        private readonly ComponentFactory _componentFactory = ComponentFactory.Instance;
        private Board _boardToBuild;

        public BoardBuilder()
        {
            _boardToBuild = new Board();
        }

        public Board Build()
        {
            var buildedBoard = _boardToBuild;
            _boardToBuild = new Board();

            return buildedBoard;
        }

        public BoardBuilder LinkList(string compName, IList<string> links)
        {
            foreach (var link in links)
            {
                Link(link, compName);
            }

            return this;
        }

        public BoardBuilder Link(string to, string from)
        {
            var component = _boardToBuild.Components[to];

            _boardToBuild.Components[from].LinkNext(component);

            return this;
        }

        public BoardBuilder AddComponent(string varName, string componentName, string input = "LOW")
        {
            input = input ?? "LOW"; // Fix nullable input

            var component = _componentFactory.Create(componentName);

            component.Name = varName;
            component.Value = (Bit)Enum.Parse(typeof(Bit), input, true);

            _boardToBuild.Components.Add(varName, component);

            return this;
        }

        public BoardBuilder AddBoard(string varName, Board board)
        {
            board.Name = varName;
            _boardToBuild.Components.Add(varName, board);

            return this;
        }
    }
}
