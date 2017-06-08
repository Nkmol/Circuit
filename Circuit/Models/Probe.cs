using System.CodeDom;

namespace Models
{
    using System.Linq;

    public class PROBE : Component
    {
        public override void Calculate()
        {
            var firstOrDefault = Previous.FirstOrDefault();
            if (firstOrDefault != null)
            {
                Value = firstOrDefault.Value;
            }
        }
    }
}