namespace Models
{
    using System.Linq;

    public class Probe : Component
    {
        public override void Calculate()
        {
            Output = Inputs.FirstOrDefault();
        }
    }
}