using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;

namespace Tests
{
    [TestClass]
    public class ComponentsTests
    {
        [TestMethod]
        [TestCategory("AND")]
        public void AND_OnePositiveOneNegative_NegativeOutput()
        {
            // Arrange
            var port = new AND();
            port.Previous.Add(new Input {Value = Bit.HIGH});
            port.Previous.Add(new Input {Value = Bit.LOW});

            // Act
            port.Calculate();

            // Assert
            Assert.AreEqual(Bit.LOW, port.Value);
        }

        [TestMethod]
        [TestCategory("AND")]
        public void AND_TwoNegative_NegativeOutput()
        {
            // Arrange
            var port = new AND();
            port.Previous.Add(new Input {Value = Bit.LOW});
            port.Previous.Add(new Input {Value = Bit.LOW});

            // Act
            port.Calculate();

            // Assert
            Assert.AreEqual(Bit.LOW, port.Value);
        }

        [TestMethod]
        [TestCategory("AND")]
        public void AND_TwoPositive_NegativeOutput()
        {
            // Arrange
            var port = new AND();
            port.Previous.Add(new Input {Value = Bit.HIGH});
            port.Previous.Add(new Input {Value = Bit.HIGH});

            // Act
            port.Calculate();

            // Assert
            Assert.AreEqual(Bit.HIGH, port.Value);
        }

        [TestMethod]
        [TestCategory("NOT")]
        public void NOT_OnePositive_NegativeOutput()
        {
            // Arrange
            var port = new NOT();
            port.Previous.Add(new Input {Value = Bit.LOW});

            // Act
            port.Calculate();

            // Assert
            Assert.AreEqual(Bit.HIGH, port.Value);
        }

        [TestMethod]
        [TestCategory("NOT")]
        public void NOT_OneNegative_PositiveOutput()
        {
            // Arrange
            var port = new NOT();
            port.Previous.Add(new Input {Value = Bit.HIGH});

            // Act
            port.Calculate();

            // Assert
            Assert.AreEqual(Bit.LOW, port.Value);
        }

        [TestMethod]
        [TestCategory("OR")]
        public void OR_OneNegativeOnePositive_PositiveOutput()
        {
            // Arrange
            var port = new OR();
            port.Previous.Add(new Input {Value = Bit.LOW});
            port.Previous.Add(new Input {Value = Bit.HIGH});

            // Act
            port.Calculate();

            // Assert
            Assert.AreEqual(Bit.HIGH, port.Value);
        }

        [TestMethod]
        [TestCategory("OR")]
        public void OR_TwoPositive_PositiveOutput()
        {
            // Arrange
            var port = new OR();
            port.Previous.Add(new Input {Value = Bit.HIGH});
            port.Previous.Add(new Input {Value = Bit.HIGH});

            // Act
            port.Calculate();

            // Assert
            Assert.AreEqual(Bit.HIGH, port.Value);
        }

        [TestMethod]
        [TestCategory("OR")]
        public void OR_TwoNegative_NegativeOutput()
        {
            // Arrange
            var port = new OR();
            port.Previous.Add(new Input {Value = Bit.LOW});
            port.Previous.Add(new Input {Value = Bit.LOW});

            // Act
            port.Calculate();

            // Assert
            Assert.AreEqual(Bit.LOW, port.Value);
        }

        [TestMethod]
        [TestCategory("NAND")]
        public void NAND_TwoNegative_PositiveOutput()
        {
            // Arrange
            var port = new NAND();
            port.Previous.Add(new Input {Value = Bit.LOW});
            port.Previous.Add(new Input {Value = Bit.LOW});

            // Act
            port.Calculate();

            // Assert
            Assert.AreEqual(Bit.HIGH, port.Value);
        }

        [TestMethod]
        [TestCategory("NAND")]
        public void NAND_OnePositiveOneNegative_PositiveOutput()
        {
            // Arrange
            var port = new NAND();
            port.Previous.Add(new Input {Value = Bit.LOW});
            port.Previous.Add(new Input {Value = Bit.HIGH});

            // Act
            port.Calculate();

            // Assert
            Assert.AreEqual(Bit.HIGH, port.Value);
        }

        [TestMethod]
        [TestCategory("NAND")]
        public void NAND_TwoPositive_NegativeOutput()
        {
            // Arrange
            var port = new NAND();
            port.Previous.Add(new Input {Value = Bit.HIGH});
            port.Previous.Add(new Input {Value = Bit.HIGH});

            // Act
            port.Calculate();

            // Assert
            Assert.AreEqual(Bit.LOW, port.Value);
        }

        [TestMethod]
        [TestCategory("NOR")]
        public void NOR_TwoPositive_NegativeOutput()
        {
            // Arrange
            var port = new NOR();
            port.Previous.Add(new Input {Value = Bit.HIGH});
            port.Previous.Add(new Input {Value = Bit.HIGH});

            // Act
            port.Calculate();

            // Assert
            Assert.AreEqual(Bit.LOW, port.Value);
        }

        [TestMethod]
        [TestCategory("NOR")]
        public void NOR_OneNegativeOnePositive_NegativeOutput()
        {
            // Arrange
            var port = new NOR();
            port.Previous.Add(new Input {Value = Bit.LOW});
            port.Previous.Add(new Input {Value = Bit.HIGH});

            // Act
            port.Calculate();

            // Assert
            Assert.AreEqual(Bit.LOW, port.Value);
        }

        [TestMethod]
        [TestCategory("NOR")]
        public void NOR_TwoNegative_PositiveOutput()
        {
            // Arrange
            var port = new NOR();
            port.Previous.Add(new Input {Value = Bit.LOW});
            port.Previous.Add(new Input {Value = Bit.LOW});

            // Act
            port.Calculate();

            // Assert
            Assert.AreEqual(Bit.HIGH, port.Value);
        }

        [TestMethod]
        [TestCategory("Input")]
        public void Input_DefaultPositive_OutputPositive()
        {
            // Arrange
            var port = new Input {Value = Bit.HIGH};

            // Act
            port.Calculate();

            // Assert
            Assert.AreEqual(Bit.HIGH, port.Value);
        }

        [TestMethod]
        [TestCategory("Input")]
        public void Input_DefaultNegative_OutputNegative()
        {
            // Arrange
            var port = new Input {Value = Bit.LOW};

            // Act
            port.Calculate();

            // Assert
            Assert.AreEqual(Bit.LOW, port.Value);
        }

        [TestMethod]
        [TestCategory("Probe")]
        public void Probe_OnePositive_OutputPositive()
        {
            // Arrange
            var port = new Probe();
            port.Previous.Add(new Input {Value = Bit.HIGH});

            // Act
            port.Calculate();

            // Assert
            Assert.AreEqual(Bit.HIGH, port.Value);
        }

        [TestMethod]
        [TestCategory("Probe")]
        public void Probe_OneNegative_OutputPositive()
        {
            // Arrange
            var port = new Probe();
            port.Previous.Add(new Input {Value = Bit.LOW});

            // Act
            port.Calculate();

            // Assert
            Assert.AreEqual(Bit.LOW, port.Value);
        }
    }
}