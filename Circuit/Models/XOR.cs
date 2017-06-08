namespace Models
{
    using System.Linq;

    internal class XOR : Port
    {
        public override void Calculate()
        {
            // Odd number HIGH inputs
            if (Previous.Select(x => x.Value == Bit.HIGH).Count() % 2 != 0)
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