namespace Models
{
    using System.Linq;

    public class PROBE : Component
    {
        public override void Calculate()
        {
            Output = Inputs.FirstOrDefault();
        }
    }
}