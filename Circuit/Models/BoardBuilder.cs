using System;
using System.Collections.Generic;

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
                Link(link, compName);

            return this;
        }

        public BoardBuilder Link(string to, string from)
        {
            var componentTo = _boardToBuild.Components.TryGetValue(to, out Component result1) ? result1 : null;
            ;
            var componentFrom = _boardToBuild.Components.TryGetValue(from, out Component result2) ? result2 : null;

            if (componentFrom != null && componentTo != null)
                _boardToBuild.Components[from].LinkNext(componentTo);

            return this;
        }

        public BoardBuilder AddComponent(string varName, string componentName, string input = "LOW")
        {
            input = input ?? "LOW"; // Fix nullable input
            input = input.ToUpper();

            var component = _componentFactory.Create(componentName);

            if (component == null)
                return this;

            component.Name = varName;
            if (Enum.TryParse(input, out Bit result))
                component.Value = result;

            _boardToBuild.Components[varName] = component;

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