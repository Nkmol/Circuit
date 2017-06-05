namespace Models
{
    using System.Linq;

    internal class AND : Port
    {
        public override void Calculate()
        {
            if (Inputs.Select(x => x == Bit.HIGH).Count() == Inputs.Count)
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