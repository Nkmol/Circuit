using System;
namespace Models
{
    public class BoardBuilder
    {

        private DirectGraph<Component> _nodes;
        private GraphNode<Component> _current;
        private GraphNode<Component> _componentAssign;

        private ComponentFactory _componentFactory = new ComponentFactory();


        public DirectGraph<Component> Nodes {
            get {
                return this._nodes;
            }
        }

        public BoardBuilder()
        {
            _nodes =  new DirectGraph<Component>();
        }

        public void Link(string compName, string assignTo) {
			_current = _nodes[compName];
            _componentAssign = _nodes[assignTo];
            this._componentAssign.LinkNext(_current);
        }

        public void addNode(string varName, Component component)
        {
            this._nodes.Add(varName, component);
        }

        public Component Create(string compName)
        {
			Type t = _componentFactory.GetType(compName);

			if (!_componentFactory.Exists(compName)){
			    _componentFactory.AddNodeType(compName, t);
			}

			return _componentFactory.CreateComponent(compName);
		}
    }
}
