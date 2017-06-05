namespace Models
{
    internal class NOT : Port
    {
        public override void Calculate()
        {
            if (Inputs[0] == Bit.HIGH)
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