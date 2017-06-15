namespace Models
{
    using System.Linq;

    public class NAND : Port
    {
        public override void Calculate()
        {
            if (Previous.Count(x => x.Value == Bit.HIGH) == Previous.Count)
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