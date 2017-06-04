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
                Output = (int) component.Output,
                Type = component.GetType().Name
            };
        }
    }
}