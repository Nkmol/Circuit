namespace Models
{
    using System;

    public abstract class Port : Component
    {
        private int delay;

        public Bit CalculateOutput()
        {
            throw new NotImplementedException();
        }
    }
}