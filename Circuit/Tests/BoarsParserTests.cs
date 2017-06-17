using System;
using System.Collections.Generic;
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
            var resultLinkLine = new List<BoardParser.LinkLine>();
            var resultVarLine = new List<BoardParser.VariableLine>();
            foreach (var line in lines)
            {
                resultLinkLine.Add(parser.ParseLinkLine(line));
                resultVarLine.Add(parser.ParseVariableLine(line));
            }

            // Arrange
            Assert.AreEqual(0, resultVarLine.Count(x => x != null));
            Assert.AreEqual(0, resultLinkLine.Count(x => x != null));
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
            var resultLinkLine = new List<BoardParser.LinkLine>();
            var resultVarLine = new List<BoardParser.VariableLine>();
            foreach (var line in lines)
            {
                resultLinkLine.Add(parser.ParseLinkLine(line));
                resultVarLine.Add(parser.ParseVariableLine(line));
            }

            // Arrange
            Assert.AreEqual(0, resultVarLine.Count(x => x != null));
            Assert.AreEqual(0, resultLinkLine.Count(x => x != null));
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
            var resultLinkLine = new List<BoardParser.LinkLine>();
            var resultVarLine = new List<BoardParser.VariableLine>();
            foreach (var line in lines)
            {
                resultLinkLine.Add(parser.ParseLinkLine(line));
                resultVarLine.Add(parser.ParseVariableLine(line));
            }

            // Arrange
            Assert.AreEqual(0, resultVarLine.Count(x => x != null));
            Assert.AreEqual(0, resultLinkLine.Count(x => x != null));
        }

        [TestMethod]
        public void Parser_EmptyFile_Null()
        {
            // Arrange
            var parser = new BoardParser();
            var lines = new string[2];
            // Act

            // Both methods should ignore comments
            var resultLinkLine = new List<BoardParser.LinkLine>();
            var resultVarLine = new List<BoardParser.VariableLine>();
            foreach (var line in lines)
            {
                resultLinkLine.Add(parser.ParseLinkLine(line));
                resultVarLine.Add(parser.ParseVariableLine(line));
            }

            // Arrange
            Assert.AreEqual(0, resultVarLine.Count(x => x != null));
            Assert.AreEqual(0, resultLinkLine.Count(x => x != null));
        }

        [TestMethod]
        public void Parser_VariableAisInput_GetVariableLine()
        {
            // Arrange
            var parser = new BoardParser();
            var lines = new[] {"A: Input"};

            // Act
            var resultVarLine = new List<BoardParser.VariableLine>();
            foreach (var line in lines)
                resultVarLine.Add(parser.ParseVariableLine(line));

            // Arrange
            var result = resultVarLine[0];
            Assert.AreEqual("A", result.Varname);
            Assert.AreEqual("Input", result.Compname);
            Assert.AreEqual(null, result.Input);
        }

        [TestMethod]
        public void Parser_VariableAisInputHigh_GetVariableLine()
        {
            // Arrange
            var parser = new BoardParser();
            var lines = new[] {"A: Input_High"};

            // Act
            var resultVarLine = new List<BoardParser.VariableLine>();
            foreach (var line in lines)
                resultVarLine.Add(parser.ParseVariableLine(line));

            // Arrange
            var result = resultVarLine[0];
            Assert.AreEqual("A", result.Varname);
            Assert.AreEqual("Input", result.Compname);
            Assert.AreEqual("High", result.Input);
        }

        [TestMethod]
        public void Parser_VariableAisInputSpaces_GetVariableLineTrimmed()
        {
            // Arrange
            var parser = new BoardParser();
            var lines = new[] {"      A   :    Input_High       "};

            // Act
            var resultVarLine = new List<BoardParser.VariableLine>();
            foreach (var line in lines)
                resultVarLine.Add(parser.ParseVariableLine(line));

            // Arrange
            var result = resultVarLine[0];
            Assert.AreEqual("A", result.Varname);
            Assert.AreEqual("Input", result.Compname);
            Assert.AreEqual("High", result.Input);
        }

        [TestMethod]
        public void Parser_VariableAisInputTabs_GetVariableLineTrimmed()
        {
            // Arrange
            var parser = new BoardParser();
            var lines = new[] {"	A: 	Input_High	"};

            // Act
            var resultVarLine = new List<BoardParser.VariableLine>();
            foreach (var line in lines)
                resultVarLine.Add(parser.ParseVariableLine(line));

            // Arrange
            var result = resultVarLine[0];
            Assert.AreEqual("A", result.Varname);
            Assert.AreEqual("Input", result.Compname);
            Assert.AreEqual("High", result.Input);
        }

        [TestMethod]
        public void Parser_NewLineTriggersLinking_LinkingActivated()
        {
            // Arrange
            var parser = new BoardParser();
            var lines = new[] {Environment.NewLine};

            // Act
            var resultVarLine = new List<BoardParser.VariableLine>();
            foreach (var line in lines)
                resultVarLine.Add(parser.ParseVariableLine(line));

            // Arrange
            var result = resultVarLine[0];
            Assert.IsTrue(parser.StartProbLinking);
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void Parser_ParseLinking_LinkingObject()
        {
            // Arrange
            var parser = new BoardParser();
            var lines = new[] {"A: B, C, D"};

            // Act
            var resultVarLine = new List<BoardParser.LinkLine>();
            foreach (var line in lines)
                resultVarLine.Add(parser.ParseLinkLine(line));

            // Arrange
            var result = resultVarLine[0];
            Assert.AreEqual("A", result.Varname);
            Assert.AreEqual("B", result.Values[0]);
            Assert.AreEqual("C", result.Values[1]);
            Assert.AreEqual("D", result.Values[2]);
        }
    }
}