using System.Collections.Generic;

namespace Helpers.DGML
{
    public interface IParseDgmlStrategy<in T>
    {
        List<DgmlWriter.Link> ParseLinks(T toParse);
        List<DgmlWriter.Node> ParseNodes(T toParse);
    }
}