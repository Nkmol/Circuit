using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;

namespace Tests
{
    [TestClass]
    public class BoarsParserTests
    {
        [TestMethod]
        public void Parser_FileWithOnlyComments_Null()
        {
            // Arrange
            var parser = new BoardParser();
            var lines = new[]
            {
                "##########",
                "#####asdasd##",
                Environment.NewLine,
                "# Test should not show"
            };
            // Act

            // Both methods should ignore comments
            var resultLines = lines.Select(x => parser.Parse(x)).ToArray();

            // Arrange
            Assert.AreEqual(0, resultLines.Count(x => x != null));
        }

        [TestMethod]
        public void Parser_FileWithOnlyNewLines_Null()
        {
            // Arrange
            var parser = new BoardParser();
            var lines = new[]
            {
                Environment.NewLine,
                Environment.NewLine,
                Environment.NewLine,
                Environment.NewLine
            };
            // Act

            // Both methods should ignore comments
            var resultLines = lines.Select(x => parser.Parse(x)).ToArray();

            // Arrange
            Assert.AreEqual(0, resultLines.Count(x => x != null));
        }

        [TestMethod]
        public void Parser_FileWithOnlySpaces_Null()
        {
            // Arrange
            var parser = new BoardParser();
            var lines = new[]
            {
                "      "
            };
            // Act

            // Both methods should ignore comments
            var resultLines = lines.Select(x => parser.Parse(x)).ToArray();

            // Arrange
            Assert.AreEqual(0, resultLines.Count(x => x != null));
        }

        [TestMethod]
        public void Parser_EmptyFile_Null()
        {
            // Arrange
            var parser = new BoardParser();
            var lines = new string[2];
            // Act

            // Both methods should ignore comments
            var resultLines = lines.Select(x => parser.Parse(x)).ToArray();

            // Arrange
            Assert.AreEqual(0, resultLines.Count(x => x != null));
        }

        [TestMethod]
        public void Parser_VariableAisInput_GetVariableLine()
        {
            // Arrange
            var parser = new BoardParser();
            var lines = new[] {"A: Input"};

            // Act
            var resultLines = lines.Select(x => parser.Parse(x)).ToArray();

            // Arrange
            var result = resultLines[0];
            Assert.AreEqual("A", result[0]);
            Assert.AreEqual("Input", result[1]);
            Assert.AreEqual(null, result[2]);
        }

        [TestMethod]
        public void Parser_VariableAisInputHigh_GetVariableLine()
        {
            // Arrange
            var parser = new BoardParser();
            var lines = new[] {"A: Input_High"};

            // Act
            var resultLines = lines.Select(x => parser.Parse(x)).ToArray();

            // Arrange
            var result = resultLines[0];
            Assert.AreEqual("A", result[0]);
            Assert.AreEqual("Input", result[1]);
            Assert.AreEqual("High", result[2]);
        }

        [TestMethod]
        public void Parser_VariableAisInputSpaces_GetVariableLineTrimmed()
        {
            // Arrange
            var parser = new BoardParser();
            var lines = new[] {"      A   :    Input_High       "};

            // Act
            var resultLines = lines.Select(x => parser.Parse(x)).ToArray();

            // Arrange
            var result = resultLines[0];
            Assert.AreEqual("A", result[0]);
            Assert.AreEqual("Input", result[1]);
            Assert.AreEqual("High", result[2]);
        }

        [TestMethod]
        public void Parser_VariableAisInputTabs_GetVariableLineTrimmed()
        {
            // Arrange
            var parser = new BoardParser();
            var lines = new[] {"	A: 	Input_High	"};

            // Act
            var resultLines = lines.Select(x => parser.Parse(x)).ToArray();

            // Arrange
            var result = resultLines[0];
            Assert.AreEqual("A", result[0]);
            Assert.AreEqual("Input", result[1]);
            Assert.AreEqual("High", result[2]);
        }

        [TestMethod]
        public void Parser_NewLineTriggersLinking_LinkingActivated()
        {
            // Arrange
            var parser = new BoardParser();
            var lines = new[] {Environment.NewLine};

            // Act
            var resultLines = lines.Select(x => parser.Parse(x)).ToArray();

            // Arrange
            var result = resultLines[0];
            Assert.IsTrue(parser.StartProbLinking);
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void Parser_ParseLinking_LinkingObject()
        {
            // Arrange
            var parser = new BoardParser();
            var lines = new[]
            {
                Environment.NewLine, // to activate linking flag
                "A: B, C, D"
            };

            // Act
            var resultLines = lines.Select(x => parser.Parse(x)).ToArray();

            // Arrange
            var result = resultLines[1];
            Assert.AreEqual("A", result[0]);
            Assert.AreEqual("B", result[1]);
            Assert.AreEqual("C", result[2]);
            Assert.AreEqual("D", result[3]);
        }
    }
}