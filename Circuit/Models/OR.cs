using System.Linq;

namespace Models
{
    public class OR : Port
    {
        public override void Calculate()
        {
            if (Previous.Any(x => x.Value == Bit.HIGH))
                Value = Bit.HIGH;
            else
                Value = Bit.LOW;
        }
    }
}