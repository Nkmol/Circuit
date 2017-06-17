using System;
using System.Linq;
using Datatypes;

namespace Models
{
    public class ComponentFactory : Singleton<ComponentFactory>, IFactory<Component>
    {
        private readonly IFactory<Component> _factory = new Factory<Component>();

        private ComponentFactory()
        {
            // Init default values - Own implementation
            var loadTypes = typeof(Component);
            loadTypes.Assembly.GetTypes()
                .Where(t => loadTypes.IsAssignableFrom(t))
                .ToList()
                .ForEach(t => AddType(t.Name, t));
        }

        public void AddType(string typenaming, Type type)
        {
            _factory.AddType(typenaming, type);
        }

        public Component Create(string type)
        {
            return _factory.Create(type);
        }
    }
}