namespace Models
{
    using System.Linq;

    internal class OR : Port
    {
        public override void Calculate()
        {
            if (Previous.Select(x => x.Value == Bit.HIGH).Any())
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