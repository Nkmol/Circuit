using System;
using System.Collections.Generic;

namespace Models
{
    using System.Linq;
    using System.Reflection;

    public class ComponentFactory
    {
        private readonly Assembly _assembly = System.Reflection.Assembly.GetExecutingAssembly();
        private Dictionary<string, Type> _types;

        public ComponentFactory()
        {
            _types = _assembly.GetTypes()
                   .Where(x => x.Namespace == "Models")
                   .ToDictionary(t => t.Name, t => t,
                        StringComparer.OrdinalIgnoreCase);
        }
        
        public Component Create(string type)
        {
            Type t;
            if (_types.TryGetValue(type, out t))
            {
                Component c = (Component) Activator.CreateInstance(t);
                return c;
            }

            return null;
        }

    }
}
