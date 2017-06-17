using System.Collections;
using System.Collections.Generic;
using Datatypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;

namespace Tests
{
    [TestClass]
    public class Factories
    {
        [TestMethod]
        [TestCategory("Factory")]
        public void Factory_AddNewType_InstantionOfType()
        {
            // Arrange
            var factory = new Factory<IEnumerable>();
            var typeToAdd = typeof(List<string>);

            factory.AddType("list", typeToAdd);

            // Act
            var list = factory.Create("list");

            // Assert
            Assert.IsInstanceOfType(list, typeToAdd);
        }

        [TestMethod]
        [TestCategory("Factory")]
        public void Factory_AddNewType_InstantionOfTypeIgnoreCasing()
        {
            // Arrange
            var factory = new Factory<IEnumerable>();
            var typeToAdd = typeof(List<string>);

            factory.AddType("LISt", typeToAdd);

            // Act
            var list = factory.Create("list");

            // Assert
            Assert.IsInstanceOfType(list, typeToAdd);
        }

        [TestMethod]
        [TestCategory("Factory")]
        public void Factory_CreateNonExistingType_Null()
        {
            // Arrange
            var factory = new Factory<IEnumerable>();

            // Act
            var list = factory.Create("list");

            // Assert
            Assert.AreEqual(list, null);
        }

        [TestMethod]
        [TestCategory("Factory component")]
        public void Factory_CreateOR_InstantionOfOR()
        {
            // Arrange
            var factory = ComponentFactory.Instance;

            // Act
            var OR = factory.Create("or");

            // Assert
            Assert.IsInstanceOfType(OR, typeof(OR));
        }
    }
}