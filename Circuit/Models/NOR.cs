namespace Models
{
    using System.Linq;

    public class NOR : Port
    {
        public override void Calculate()
        {
            if (Previous.Count(x => x.Value == Bit.LOW) == Previous.Count)
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