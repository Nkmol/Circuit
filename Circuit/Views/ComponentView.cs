namespace Views
{
    using System;

    public class ComponentView : IDrawAble
    {
        public string Name { get; set; }
        public int Output { get; set; }
        public string Type { get; set; }

        //TODO Add Decorater pattern for drawing with type and/or output
        public void Draw()
        {
            Console.Write($"{Name} [{Type}, {Output}]");
        }
    }
}