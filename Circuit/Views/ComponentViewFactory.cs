namespace Views
{
    using Models;

    public class ComponentViewFactory
    {
        public ComponentView Create(Component component)
        {
            return new ComponentView
            {
                Name = component.name,
                Output = (int) component.output,
                Type = component.GetType().Name
            };
        }
    }
}