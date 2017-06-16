using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    using Datatypes.DirectedGraph;
    using Models;

    /// <summary>
    /// Summary description for BoardTests
    /// </summary>
    [TestClass]
    public class BoardTests
    {

        [TestMethod]
        [TestCategory("Components")]
        public void Board_AddTwoProbesAnd1Input_GetTwoProbesByProperty()
        {
            // Arrange
            var board = new Board();

            var probe1 = new Probe();
            var input1 = new Input();
            var probe2 = new Probe();

            board.Components.Add("probe1", probe1);
            board.Components.Add("input1", input1);
            board.Components.Add("probe2", probe2);

            // Act
            var probes = board.Probes;

            // Assert
            Assert.AreEqual(2, probes.Count);
            Assert.AreSame(probe1, probes[0]);
            Assert.AreSame(probe2, probes[1]);
        }

        [TestMethod]
        [TestCategory("Cycle")]
        public void Board_AddTwoCycles_YieldTwoCycles()
        {
//                     Node1 
//            Input --      -- Output
//                     Node2

            // Arrange
            var board = new Board();

            var input = new Input();
            var node1 = new OR();
            var node2 = new OR();
            var output = new Probe();

            // Arrange - setup relations
            input.LinkNext(node1);
            input.LinkNext(node2);
            node1.LinkNext(output);
            node2.LinkNext(output);

            board.Components.Add("input", input);
            board.Components.Add("node1", node1);
            board.Components.Add("node2", node2);
            board.Components.Add("output", output);

            // Act
            var cycles = new List<Cycle<Component>>();
            foreach (var cycle in board.Cycle())
            {
                cycles.Add(cycle);
            }

            // Assert
            Assert.AreEqual(2, cycles.Count);

            var cycle1 = cycles[0];
            Assert.AreSame(input, cycle1[0]);
        }
    }
}
