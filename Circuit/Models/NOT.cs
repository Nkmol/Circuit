namespace Models
{
    internal class NOT : Port
    {
        public override void Calculate()
        {
            if (Previous[0].Value == Bit.HIGH)
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