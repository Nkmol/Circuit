using System.Collections.Generic;
using Datatypes.DirectedGraph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Datatypes.Tests
{
    [TestClass]
    public class EdgeTests
    {
        // Quickfix to use graphnode
        public class GraphNode : GraphNode<GraphNodeTests.GraphNode>
        {
        }

        [TestMethod]
        public void Edge_CompareEqualEdges_Equals()
        {
            // Arrange
            GraphNode node1 = new GraphNode();
            GraphNode node2 = new GraphNode();

            Edge<GraphNode> edge1 = new Edge<GraphNode>(node1, node2);
            Edge<GraphNode> edge2 = new Edge<GraphNode>(node1, node2);

            // Act
            var result = edge1.Equals(edge2);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Edge_CompareNotEqualsEdges_NotEquals()
        {
            // Arrange
            GraphNode node1 = new GraphNode();
            GraphNode node2 = new GraphNode();
            GraphNode node3 = new GraphNode();

            Edge<GraphNode> edge1 = new Edge<GraphNode>(node1, node2);
            Edge<GraphNode> edge2 = new Edge<GraphNode>(node1, node3);

            // Act
            var result = edge1.Equals(edge2);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
