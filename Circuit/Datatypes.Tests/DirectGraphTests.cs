using System.Collections.Generic;
using Datatypes.DirectedGraph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Datatypes.Tests
{
    [TestClass]
    public class DirectGraphTests
    {
        //                     node2 
        //            node1 ->      -> node4
        //                     node3
        [TestMethod]
        [TestCategory("DFS")]
        public void Graph_CreateTwoCycleValidGraph_ReturnTwoCycles()
        {
            // Arrange
            var graph = new DirectGraph<GraphNode>();

            var node1 = new GraphNode();
            var node2 = new GraphNode();
            var node3 = new GraphNode();
            var node4 = new GraphNode();

            // Arrange - setup relations
            node1.LinkNext(node2);
            node1.LinkNext(node3);
            node2.LinkNext(node4);
            node3.LinkNext(node4);

            graph.Add("node1", node1);
            graph.Add("node2", node2);
            graph.Add("node3", node3);
            graph.Add("node4", node4);

            // Act
            var cycles = new List<Cycle<GraphNode>>();
            foreach (var cycle in graph.DepthFirstCycle(node1))
                cycles.Add(cycle);

            // Assert
            Assert.AreEqual(2, cycles.Count);

            var cycle1 = cycles[0];
            Assert.AreEqual(1, cycle1.Number);
            Assert.AreEqual(node1, cycle1[0]);
            Assert.AreEqual(node3, cycle1[1]);
            Assert.AreEqual(node4, cycle1[2]);

            var cycle2 = cycles[1];
            Assert.AreEqual(2, cycle2.Number);
            Assert.AreEqual(node1, cycle2[0]);
            Assert.AreEqual(node2, cycle2[1]);
            Assert.AreEqual(node4, cycle2[2]);
        }


        //                     node2 -> node5
        //            node1 ->  
        //                     node3 -> node4 --> node1

        // This test shows that it ignores the cycle "Node1 -> node3 -> node4 -> node1", as it is infinite and thus not valid
        [TestMethod]
        [TestCategory("DFS")]
        public void Graph_AddBackEdge_BypassInfiniteLoop_ReturnOneCycle()
        {
            // Arrange
            var graph = new DirectGraph<GraphNode>();

            var node1 = new GraphNode {Name = "node1"};
            var node2 = new GraphNode {Name = "node2"};
            var node3 = new GraphNode {Name = "node3"};
            var node4 = new GraphNode {Name = "node4"};
            var node5 = new GraphNode {Name = "node5"};

            // Arrange - setup relations
            node1.LinkNext(node2);
            node2.LinkNext(node5);

            node1.LinkNext(node3);
            node3.LinkNext(node4);
            node4.LinkNext(node1);

            graph.Add("node1", node1);
            graph.Add("node2", node2);
            graph.Add("node3", node3);
            graph.Add("node4", node4);
            graph.Add("node5", node5);

            // Act
            var cycles = new List<Cycle<GraphNode>>();
            foreach (var cycle in graph.DepthFirstCycle(node1))
                cycles.Add(cycle);

            // Assert
            Assert.IsTrue(graph.IsCyclic);
            Assert.AreEqual(1, cycles.Count);

            var cycle1 = cycles[0];
            Assert.AreEqual(3, cycle1.Count);
            Assert.AreEqual(1, cycle1.Number);
            Assert.AreEqual(node1, cycle1[0], "node1");
            Assert.AreEqual(node2, cycle1[1], "node2");
            Assert.AreEqual(node5, cycle1[2], "node5");
        }

        // Node1 -> node2 -> node1
        [TestMethod]
        [TestCategory("DFS")]
        public void Graph_AddInfiniteRelation_ReturnNoCycles()
        {
            // Arrange
            var graph = new DirectGraph<GraphNode>();

            var node1 = new GraphNode();
            var node2 = new GraphNode();

            node1.LinkNext(node2);
            node2.LinkNext(node1);

            graph.Add("node1", node1);
            graph.Add("node2", node2);

            // Act
            var cycles = new List<Cycle<GraphNode>>();
            foreach (var cycle in graph.DepthFirstCycle(node1))
                cycles.Add(cycle);

            // Assert
            Assert.IsTrue(graph.IsCyclic);
            Assert.AreEqual(0, cycles.Count);
        }

        // node1 -> node2 -> node3
        [TestMethod]
        [TestCategory("DFS")]
        public void Graph_StartDFSInTheMiddle_ReturnCycleOfTwo()
        {
            // Arrange
            var graph = new DirectGraph<GraphNode>();

            var node1 = new GraphNode();
            var node2 = new GraphNode();
            var node3 = new GraphNode();

            node1.LinkNext(node2);
            node2.LinkNext(node3);

            graph.Add("node1", node1);
            graph.Add("node2", node2);
            graph.Add("node3", node2);

            // Act
            var cycles = new List<Cycle<GraphNode>>();
            foreach (var cycle in graph.DepthFirstCycle(node2))
                cycles.Add(cycle);

            // Assert
            Assert.AreEqual(1, cycles.Count);

            var cycle1 = cycles[0];
            Assert.AreEqual(2, cycle1.Count);
            Assert.AreEqual(node2, cycle1[0]);
            Assert.AreEqual(node3, cycle1[1]);
        }

        // Quickfix to use graphnode
        public class GraphNode : GraphNode<GraphNode>
        {
        }
    }
}