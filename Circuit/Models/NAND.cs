namespace Models
{
    using System.Linq;

    internal class NAND : Port
    {
        public override void Calculate()
        {
            if (Previous.Select(x => x.Value == Bit.HIGH).Count() == Previous.Count)
            {
                Value = Bit.LOW;
            }
            else
            {
                Value = Bit.HIGH;
            }
        }
    }
}