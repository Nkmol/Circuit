using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helpers.Tests
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    [TestClass]
    public class FileReaderTests
    {
        private readonly string _dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        [TestMethod]
        [TestCategory("Load")]
        public void Reader_ExistingFile_IsValidFile()
        {
            // Arrange
            var path = $"{_dir}\\Files\\OnlyComments.txt";
            var fr = new FileReader(path);

            // Act

            // Assert
            Assert.IsTrue(fr.Exists());
            Assert.IsFalse(fr.IsEmpty());
            Assert.IsTrue(fr.HasExtension(".txt"));
        }

        [TestMethod]
        [TestCategory("Load")]
        public void Reader_ExistingEmptyFile_IsEmpty()
        {
            // Arrange
            var path = $"{_dir}\\Files\\IsEmpty.txt";
            var fr = new FileReader(path);

            // Act

            // Assert
            Assert.IsTrue(fr.IsEmpty());
        }

        [TestMethod]
        [TestCategory("Load")]
        public void Reader_NonExistingFile_DoesNotExist()
        {
            // Arrange
            var path = $"{_dir}\\Files\\DoesNotExist.txt";
            var fr = new FileReader(path);

            // Act

            // Assert
            Assert.IsFalse(fr.Exists());
        }

        [TestMethod]
        [TestCategory("Read")]
        public void Reader_ReadLines_FirstAndLastLineAreRight()
        {
            // Arrange
            var path = $"{_dir}\\Files\\OnlyComments.txt";
            var fr = new FileReader(path);

            // Act
            var lines = new List<string>();
            foreach (var line in fr.ReadLine())
            {
                lines.Add(line);
            }

            // Assert
            Assert.AreEqual("# This file only contains comments", lines[0]);
            Assert.AreEqual("###Lalasldlasd", lines.Last());
        }
    }
}
