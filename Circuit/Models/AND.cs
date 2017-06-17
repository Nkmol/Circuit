namespace Models
{
    using System.Linq;

    internal class AND : Port
    {
        public override void Calculate()
        {
            if (Previous.Count(x => x.Value == Bit.HIGH) == Previous.Count)
            {
                Value = Bit.HIGH;
            }
            else
            {
                Value = Bit.LOW;
            }
        }
    }
}