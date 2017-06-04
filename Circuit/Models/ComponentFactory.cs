using System;
using System.Collections.Generic;

namespace Models
{
    public class ComponentFactory
    {
        private readonly string _assembly = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        private const string _seperator = ".";

        private Dictionary<string, Type> _types;

        public ComponentFactory()
        {
            _types = new Dictionary<string, Type>();
        }

		public void AddNodeType(string name, Type type)
		{
			_types[name] = type;
		}

        public bool Exists(string name)
        {
            return _types.ContainsKey(name);
        }

		public Type GetType(string typeName)
		{
            var type = Type.GetType(_assembly + _seperator + typeName, true);
			if (type != null) return type;
			foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
			{
				type = a.GetType(typeName);
				if (type != null)
					return type;
			}
			return null;
		}

        public Component CreateComponent(string type)
		{
			Type t = _types[type];
            Component c = (Component)Activator.CreateInstance(t);
			return c;
		}

    }
}
