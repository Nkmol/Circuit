using System;
using System.Collections.Generic;

namespace Models
{
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class ComponentFactory
    {
        private readonly Assembly _assembly = Assembly.GetExecutingAssembly();
        private Dictionary<string, Type> _types;

        public ComponentFactory()
        {
            _types = _assembly.GetTypes()
                   .Where(x => x.Namespace == "Models")
                   .Where(x => Attribute.GetCustomAttribute(x, typeof(CompilerGeneratedAttribute)) == null) // Only get user generated types
                   .ToDictionary(t => t.Name, t => t,
                        StringComparer.OrdinalIgnoreCase);
        }
        
        public Component Create(string type)
        {
            if (_types.TryGetValue(type, out var t))
            {
                return (Component) Activator.CreateInstance(t);
            }

            return null;
        }

    }
}
