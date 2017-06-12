using System;
using System.Collections.Generic;
using System.Security.Policy;

namespace Models
{
    public class BoardBuilder
    {

        private readonly DirectGraph<Component> _nodes;
        private readonly ComponentFactory _componentFactory = ComponentFactory.Instance;

        public BoardBuilder()
        {
            _nodes =  new DirectGraph<Component>();
        }

        public Board Build()
        {
            return new Board(_nodes);
        }

        public void LinkList(string compName, IList<string> links)
        {
            foreach (var link in links)
            {
                Link(link, compName);
            }
        }

        public void Link(string to, string from)
        {
            var component = _nodes[to];

			_nodes[from].LinkNext(component);
        }

        public void AddComponent(string varName, string componentName, string input = "LOW")
        {
            input = input ?? "LOW"; // Fix nullable input

            var component = _componentFactory.Create(componentName);

            component.Name = varName;
            component.Value = (Bit)Enum.Parse(typeof(Bit), input, true);

            _nodes.Add(varName, component);
        }

        public void AddBoard(string varName, string path)
        {
            var board = Board.Create(path);
            board.Name = varName;
            _nodes.Add(varName, board);   
        }
    }
}
