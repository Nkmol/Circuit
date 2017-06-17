using Datatypes.DirectedGraph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Datatypes.Tests
{
    [TestClass]
    public class GraphNodeTests
    {
        [TestMethod]
        public void Node_AddLinkNext_NodeHasOneLinkNext()
        {
            // Arrange
            var node1 = new GraphNode();
            var node2 = new GraphNode();

            // Act
            node1.LinkNext(node2);

            // Assert
            Assert.AreEqual(1, node1.Next.Count);
            Assert.AreEqual(node1.Next[0], node2);
        }

        [TestMethod]
        public void Node_AddLinkNext_Node2HasOneLinkPrevious()
        {
            // Arrange
            var node1 = new GraphNode();
            var node2 = new GraphNode();

            // Act
            node1.LinkNext(node2);

            // Assert
            Assert.AreEqual(1, node2.Previous.Count);
            Assert.AreEqual(node2.Previous[0], node1);
        }

        // Quickfix to use graphnode
        public class GraphNode : GraphNode<GraphNode>
        {
        }
    }
}