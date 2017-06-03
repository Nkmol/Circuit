namespace Models
{
    public abstract class Component
    {
        public string description;
        public string name;
        public Bit output = Bit.LOW;
    }
}