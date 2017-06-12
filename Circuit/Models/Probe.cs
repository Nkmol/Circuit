using System.CodeDom;
using Datatypes;

namespace Models
{
    using System.Linq;

    public class Probe : Component, ILeaf
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