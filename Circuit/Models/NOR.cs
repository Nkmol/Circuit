namespace Models
{
    using System.Linq;

    internal class NOR : Port
    {
        public override void Calculate()
        {
            if (Previous.Select(x => x.Value == Bit.LOW).Count() == Previous.Count)
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