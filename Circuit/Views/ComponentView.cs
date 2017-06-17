using System;

namespace Views
{
    public class ComponentView : IDrawAble
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string Type { get; set; }

        public void Draw()
        {
            Console.Write($"{Name} [{Type}, {Value}]");
        }
    }
}