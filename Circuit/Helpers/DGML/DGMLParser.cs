using System.Collections.Generic;

namespace Helpers.DGML
{
    public class DGMLParser
    {
        public List<DgmlWriter.Link> Links;
        public List<DgmlWriter.Node> Nodes;

        public void Parse<T>(IParseDgmlStrategy<T> parser, T objectToParse)
        {
            Links = parser.ParseLinks(objectToParse);
            Nodes = parser.ParseNodes(objectToParse);
        }
    }
}