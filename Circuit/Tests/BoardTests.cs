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
        public void Board_AddTwoProbesAndOneInput_GetTwoProbesByProperty()
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

        //                     Node1 
        //            Input --      -- Output
        //                     Node2
        [TestMethod]
        [TestCategory("Cycle")]
        public void Board_AddTwoCycles_YieldTwoCycles()
        {
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

        [TestMethod]
        [TestCategory("Cycle")]
        public void Board_AddBoardWithUnconnectedStartingPoint_ReturnNoCycles()
        {
            // Arrange
            var board = new Board();

            var input = new Input();
            var node1 = new OR();
            var node2 = new OR();
            var output = new Probe();

            // Arrange - setup relations
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
            Assert.AreEqual(0, cycles.Count);
        }

        [TestMethod]
        [TestCategory("Calculate")]
        public void Board_AddBoardPositiveInput_PositiveProbe()
        {
            // Arrange
            var board = new Board();

            var input = new Input() { Value = Bit.HIGH};
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
            board.Calculate();

            // Assert
            var probes = board.Probes[0];
            Assert.AreEqual(probes.Value, Bit.HIGH);
        }

        [TestMethod]
        [TestCategory("Calculate")]
        public void Board_AddBoardNegativeInput_NegativeProbe()
        {
            // Arrange
            var board = new Board();

            var input = new Input() { Value = Bit.LOW };
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
            board.Calculate();

            // Assert
            var probes = board.Probes[0];
            Assert.AreEqual(probes.Value, Bit.LOW);
        }

        [TestMethod]
        [TestCategory("Connection")]
        public void Board_AddOnlyInput_NotConnected()
        {
            // Arrange
            var board = new Board();

            var input = new Input() { Value = Bit.LOW };

            board.Components.Add("input", input);

            // Act
            var isFullCircuit = board.CheckConnection();

            // Assert
            Assert.AreEqual(false, isFullCircuit);
        }

        [TestMethod]
        [TestCategory("Connection")]
        public void Board_AddInputAndOuput_NotConnected()
        {
            // Arrange
            var board = new Board();

            var input = new Input() { Value = Bit.LOW };
            var output = new Probe();

            board.Components.Add("input", input);
            board.Components.Add("output", output);

            // Act
            var isFullCircuit = board.CheckConnection();

            // Assert
            Assert.AreEqual(false, isFullCircuit);
        }

        [TestMethod]
        [TestCategory("Connection")]
        public void Board_AddInputAndOuputConnected_Connected()
        {
            // Arrange
            var board = new Board();

            var input = new Input() { Value = Bit.LOW };
            var output = new Probe();

            input.LinkNext(output);

            board.Components.Add("input", input);
            board.Components.Add("output", output);

            // Act
            var isFullCircuit = board.CheckConnection();

            // Assert
            Assert.AreEqual(true, isFullCircuit);
        }

        [TestMethod]
        [TestCategory("Connection")]
        public void Board_AddTwoComponentsConnected_NotConnected()
        {
            // Arrange
            var board = new Board();

            var node1 = new OR() { Value = Bit.LOW };
            var node2 = new OR();

            node1.LinkNext(node2);

            board.Components.Add("node1", node1);
            board.Components.Add("node2", node2);

            // Act
            var isFullCircuit = board.CheckConnection();

            // Assert
            Assert.AreEqual(false, isFullCircuit);
        }
    }
}
