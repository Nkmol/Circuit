using System.Collections.Generic;
using System.Linq;
using Datatypes.DirectedGraph;
using Helpers.DGML;

namespace Models
{
    public class BoardDgmlStrategy : IParseDgmlStrategy<List<Edge<Component>>>
    {
        public List<DgmlWriter.Link> ParseLinks(List<Edge<Component>> toParse)
        {
            var uniqueLinks = toParse.Distinct();

            return uniqueLinks.Select(x => new DgmlWriter.Link(x.From.Name, x.To.Name, "")).ToList();
        }

        public List<DgmlWriter.Node> ParseNodes(List<Edge<Component>> toParse)
        {
            var uniqueComp = toParse.Select(x => x.From).GroupBy(x => x.Name, (key, group) => group.First());

            return uniqueComp.Select(x => new DgmlWriter.Node(x.Name, $"{x.Name} = {x.GetType().Name}[{x.Value}]")).ToList();
        }
    }
}