using System.Linq;
using Datatypes.DirectedGraph;

namespace Models
{
    public class Probe : Component, ILeaf
    {
        public override void Calculate()
        {
            var firstOrDefault = Previous.FirstOrDefault();
            if (firstOrDefault != null)
                Value = firstOrDefault.Value;
        }
    }
}