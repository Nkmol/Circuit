namespace Models
{
    using System.Linq;

    internal class NOR : Port
    {
        public override void Calculate()
        {
            if (Inputs.Select(x => x == Bit.LOW).Count() == Inputs.Count)
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