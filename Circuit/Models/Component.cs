namespace Models
{
    public abstract class Component
    {
        public string Description;
        public string Name;
        public Bit Output { get; set; } = Bit.LOW;
    }
}