using System;
using Datatypes;

namespace Views
{
    using Models;

    // Only uses one component, so no need for IFactory
    public class ComponentViewFactory : Singleton<ComponentViewFactory>
    {
        private ComponentViewFactory()
        {
            
        }

        public ComponentView Create(Component component)
        {
            return new ComponentView
            {
                Name = component.Name,
                Value = (int) component.Value,
                Type = component.GetType().Name
            };
        }
    }
}