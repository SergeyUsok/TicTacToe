using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace TicTacToe.Core.Tests
{
    [TestFixture]
    public class RoundRobinIteratorTests
    {
        [Test]
        public void Ctor_Test_Does_Not_Accept_Null()
        {
            // Arrange
            IList<int> items = null;

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => new RoundRobinIterator<int>(items));
        }

        [Test]
        public void Ctor_Test_Items_Should_Contain_AT_Least_One_Record()
        {
            // Arrange
            IList<int> items = new List<int>();

            // Act, Assert
            Assert.Throws<ArgumentException>(() => new RoundRobinIterator<int>(items));
        }

        [Test]
        public void Ctor_Test_Accepts_Items_With_One_Record()
        {
            // Arrange
            IList<int> items = new List<int> {1};

            // Act
            var iterator = new RoundRobinIterator<int>(items);

            // Assert
            Assert.That(iterator.Next(), Is.EqualTo(1));
        }

        [Test]
        public void Ctor_Test_Accepts_One_Parameter()
        {
            // Arrange
            const int firstItem = 10;

            // Act
            var iterator = new RoundRobinIterator<int>(firstItem);

            // Assert
            Assert.That(iterator.Next(), Is.EqualTo(firstItem));
        }

        [Test]
        public void Ctor_Test_Accepts_Any_Number_Of_Parameters()
        {
            // Arrange
            const int firstItem = 10;

            // Act
            var iterator = new RoundRobinIterator<int>(firstItem, 11, 9, 8);

            // Assert
            Assert.That(iterator.Next(), Is.EqualTo(firstItem));
        }

        [Test]
        public void Next_Test_Returns_Items_In_Added_Order()
        {
            // Arrange
            const int firstItem = 10;

            // Act
            var iterator = new RoundRobinIterator<int>(firstItem, 11, 9, 8);

            // Assert
            Assert.That(iterator.Next(), Is.EqualTo(firstItem));
            Assert.That(iterator.Next(), Is.EqualTo(11));
            Assert.That(iterator.Next(), Is.EqualTo(9));
            Assert.That(iterator.Next(), Is.EqualTo(8));
        }

        [Test]
        public void Next_Test_Returns_Items_In_Lopped_Fashion()
        {
            // Arrange
            const int firstItem = 10;

            // Act
            var iterator = new RoundRobinIterator<int>(firstItem, 11);

            // Assert
            Assert.That(iterator.Next(), Is.EqualTo(firstItem));
            Assert.That(iterator.Next(), Is.EqualTo(11));
            Assert.That(iterator.Next(), Is.EqualTo(firstItem));
            Assert.That(iterator.Next(), Is.EqualTo(11));
        }

        [Test]
        public void Next_Test_Returns_Items_In_Lopped_Fashion_With_One_Item()
        {
            // Arrange
            const int firstItem = 13;

            // Act
            var iterator = new RoundRobinIterator<int>(firstItem);

            // Assert
            Assert.That(iterator.Next(), Is.EqualTo(firstItem));
            Assert.That(iterator.Next(), Is.EqualTo(firstItem));
            Assert.That(iterator.Next(), Is.EqualTo(firstItem));
        }
    }
}
