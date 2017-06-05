namespace Models
{
    using System.Linq;

    internal class NAND : Port
    {
        public override void Calculate()
        {
            if (Inputs.Select(x => x == Bit.HIGH).Count() == Inputs.Count)
            {
                Output = Bit.LOW;
            }
            else
            {
                Output = Bit.HIGH;
            }
        }
    }
}