namespace Models
{
    using System.Linq;

    internal class OR : Port
    {
        public override void Calculate()
        {
            if (Inputs.Select(x => x == Bit.HIGH).Any())
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