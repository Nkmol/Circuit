using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;

namespace Tests
{
    [TestClass]
    public class BoardBuilderTests
    {
        [TestMethod]
        [TestCategory("Assign")]
        public void Builder_AssignInput_InputExists()
        {
            // Arrange
            var builder = new BoardBuilder();

            // Act
            var varName = "InputNode";
            var compName = "Input";
            builder.AddComponent(varName, compName);

            // Assert
            var components = builder.Build().Components;
            Assert.IsTrue(components.ContainsKey(varName));

            var node = components[varName];
            Assert.IsInstanceOfType(node, typeof(Input));
        }

        [TestMethod]
        [TestCategory("Assign")]
        public void Builder_AssignInputHigh_InputHighExists()
        {
            // Arrange
            var builder = new BoardBuilder();

            // Act
            var varName = "InputNode";
            var compName = "Input";
            var value = "High";
            builder.AddComponent(varName, compName, value);

            // Assert
            var components = builder.Build().Components;
            Assert.IsTrue(components.ContainsKey(varName));

            var node = components[varName];
            Assert.IsInstanceOfType(node, typeof(Input));
            Assert.AreEqual(Bit.HIGH, node.Value);
        }

        [TestMethod]
        [TestCategory("Assign")]
        public void Builder_AssignInputNonExistingValue_InputExistsWithLowValue()
        {
            // Arrange
            var builder = new BoardBuilder();

            // Act
            var varName = "InputNode";
            var compName = "Input";
            var value = "Non-Existing";
            builder.AddComponent(varName, compName, value);

            // Assert
            var components = builder.Build().Components;
            Assert.IsTrue(components.ContainsKey(varName));

            var node = components[varName];
            Assert.IsInstanceOfType(node, typeof(Input));
            Assert.AreEqual(Bit.LOW, node.Value);
        }

        [TestMethod]
        [TestCategory("Assign")]
        public void Builder_AssignNonExistingComponent_InputIgnored()
        {
            // Arrange
            var builder = new BoardBuilder();

            // Act
            var varName = "InputNode";
            var compName = "Non-existing-component";
            builder.AddComponent(varName, compName);

            // Assert
            var components = builder.Build().Components;
            Assert.IsTrue(!components.ContainsKey(varName));
        }

        [TestMethod]
        [TestCategory("Assign")]
        public void Builder_AssignAlreadyExistingVariable_OverwriteComponent()
        {
            // Arrange
            var builder = new BoardBuilder();

            // Act
            var varName = "Node1";
            var compName = "Input";
            builder.AddComponent(varName, compName);

            var compName2 = "Probe";
            builder.AddComponent(varName, compName2);

            // Assert
            var components = builder.Build().Components;
            Assert.IsTrue(components.ContainsKey(varName));

            var node = components[varName];
            Assert.IsInstanceOfType(node, typeof(Probe));
        }

        [TestMethod]
        [TestCategory("Linking")]
        public void Builder_LinkAToB_AIsLinkedToB()
        {
            // Arrange
            var builder = new BoardBuilder();

            // Act
            var varName = "A";
            var compName = "Input";
            builder.AddComponent(varName, compName);

            var varName2 = "B";
            var compName2 = "OR";
            builder.AddComponent(varName2, compName2);


            builder.Link(varName2, varName);
            // Assert
            var components = builder.Build().Components;
            var node1 = components[varName];
            var node2 = components[varName2];

            Assert.AreEqual(node1, node2.Previous[0]);
            Assert.AreEqual(node2, node1.Next[0]);
        }

        [TestMethod]
        [TestCategory("Linking")]
        public void Builder_LinkBCToAWithBulk_BCLinkedToA()
        {
            // Arrange
            var builder = new BoardBuilder();

            // Act
            var varName1 = "A";
            var compName = "Input";
            builder.AddComponent(varName1, compName);

            var varName2 = "B";
            var compName2 = "OR";
            builder.AddComponent(varName2, compName2);

            var varName3 = "C";
            var compName3 = "OR";
            builder.AddComponent(varName3, compName3);

            builder.LinkList(varName1, new[] {varName2, varName3});
            // Assert
            var components = builder.Build().Components;
            var node1 = components[varName1];
            var node2 = components[varName2];
            var node3 = components[varName3];

            Assert.AreEqual(node2, node1.Next[0]);
            Assert.AreEqual(node3, node1.Next[1]);
        }

        [TestMethod]
        [TestCategory("Linking")]
        public void Builder_LinkAToNonExisting_NothingIsLinked()
        {
            // Arrange
            var builder = new BoardBuilder();

            // Act
            var varName1 = "A";
            var compName = "Input";
            builder.AddComponent(varName1, compName);

            var varName2 = "B";

            builder.LinkList(varName1, new[] {varName2});
            // Assert
            var components = builder.Build().Components;
            var node1 = components[varName1];

            Assert.IsTrue(node1.Next.Count == 0);
        }

        [TestMethod]
        [TestCategory("Linking")]
        public void Builder_LinkBoardToA_BoardIsLinkedToA()
        {
            // Arrange
            var builder = new BoardBuilder();

            // Act
            // Create first board
            var varName1 = "A";
            var compName1 = "Input";
            builder.AddComponent(varName1, compName1);

            var varName2 = "B";
            var compName2 = "Probe";
            builder.AddComponent(varName2, compName2);

            builder.LinkList(varName1, new[] {varName2});
            var board = builder.Build();

            // Create second board
            builder.AddComponent(varName1, compName1);
            builder.AddBoard("board1", board);

            builder.Link("board1", "A");

            // Assert
            var components = builder.Build().Components;
            var node1 = components[varName1];

            Assert.IsInstanceOfType(node1.Next[0], typeof(Board));
            Assert.AreEqual(node1.Next[0].Name, "board1");
        }
    }
}