namespace Views
{
    using Models;

    public class ComponentViewFactory
    {
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