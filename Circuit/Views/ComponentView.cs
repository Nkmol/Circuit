namespace Views
{
    using System;

    public class ComponentView : IDrawAble
    {
        public string Name { get; set; }
        public int Output { get; set; }
        public string Type { get; set; }

        public void Draw()
        {
            Console.Write($"{Name} [{Type}, {Output}]");
        }
    }
}