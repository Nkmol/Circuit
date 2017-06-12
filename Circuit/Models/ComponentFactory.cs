using System;

namespace Models
{
    using System.Linq;

    public class ComponentFactory : Factory<Component>
    {
        private static readonly Lazy<ComponentFactory> InstanceHolder =
            new Lazy<ComponentFactory>(() => new ComponentFactory());

        public static ComponentFactory Instance => InstanceHolder.Value;

        private ComponentFactory()
        {
            // Init default values - Own implementation
            var loadTypes = typeof(Component);
            loadTypes.Assembly.GetTypes()
                .Where(t => loadTypes.IsAssignableFrom(t))
                .ToList()
                .ForEach(t => AddType(t.Name, t));
        }
        
        public override Component Create(string type)
        {
            if (Types.TryGetValue(type, out var t))
            {
                return (Component) Activator.CreateInstance(t);
            }

            return null;
        }
    }
}
