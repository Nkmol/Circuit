namespace Models
{
    using System.Linq;

    internal class XOR : Port
    {
        public override void Calculate()
        {
            // Odd number HIGH inputs
            if (Inputs.Select(x => x == Bit.HIGH).Count() % 2 != 0)
            {
                Output = Bit.HIGH;
            }
            else
            {
                Output = Bit.LOW;
            }
        }
    }
}