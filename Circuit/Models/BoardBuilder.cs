using System;
namespace Models
{
    public class BoardBuilder
    {

        private readonly DirectGraph<Component> _nodes;
        private readonly ComponentFactory _componentFactory = new ComponentFactory();

        public Board Board => new Board(_nodes);

        public BoardBuilder()
        {
            _nodes =  new DirectGraph<Component>();
        }

        public void Link(string compName, string assignTo)
        {
            var component = _nodes[compName];

			_nodes[assignTo].LinkNext(component);
        }

        public void AddComponent(string varName, string componentName, string input = "LOW")
        {
            input = input ?? "LOW"; // Fix nullable input

            var component = _componentFactory.Create(componentName);
            component.Name = varName;
            component.Output = (Bit)Enum.Parse(typeof(Bit), input, true);

            _nodes.Add(varName, component);
        }
    }
}
