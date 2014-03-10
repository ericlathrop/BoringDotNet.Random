using NSubstitute;
using NUnit.Framework;
using System;

namespace BoringDotNet.Random
{
    [TestFixture]
    // ReSharper disable InconsistentNaming
    public class IRandomSourceExtensionsTests
    // ReSharper restore InconsistentNaming
    {
        [Test]
        public void NextInt_WithZeroRange_ReturnsZero()
        {
            var source = Substitute.For<IRandomSource>();
            Assert.AreEqual(0, source.NextInt(0, 0));
        }

        [Test]
        public void NextInt_WithOneRangeAndAlmostOne_ReturnsZero()
        {
            var source = Substitute.For<IRandomSource>();
            source.Next().Returns(0.99999);
            Assert.AreEqual(0, source.NextInt(0, 1));
        }

        [Test]
        public void NextInt_WithTwoRangeAndAlmostOne_ReturnsOne()
        {
            var source = Substitute.For<IRandomSource>();
            source.Next().Returns(0.99999);
            Assert.AreEqual(1, source.NextInt(0, 2));
        }

        [Test]
        public void NextInt_WithMinThree_ReturnsThree()
        {
            var source = Substitute.For<IRandomSource>();
            source.Next().Returns(0.0);
            Assert.AreEqual(3, source.NextInt(3, 20));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Generate_WithNullValidChars_ThrowsException()
        {
            var randomSource = Substitute.For<IRandomSource>();
            randomSource.NextString(null, 1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Generate_WithEmptyValidChars_ThrowsException()
        {
            var randomSource = Substitute.For<IRandomSource>();
            randomSource.NextString("", 1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Generate_WithNegativeLength_ThrowsException()
        {
            var randomSource = Substitute.For<IRandomSource>();
            randomSource.NextString("a", -1);
        }

        [Test]
        public void Generate_WithFirstChar_ReturnsFirstChar()
        {
            var randomSource = Substitute.For<IRandomSource>();
            randomSource.Next().Returns(0.0);
            var actual = randomSource.NextString("abc", 1);
            Assert.AreEqual("a", actual);
        }

        [Test]
        public void Generate_WithLastChar_ReturnsLastChar()
        {
            var randomSource = Substitute.For<IRandomSource>();
            randomSource.Next().Returns(0.999999999);
            var actual = randomSource.NextString("abc", 1);
            Assert.AreEqual("c", actual);
        }

        [Test]
        public void Generate_WithThreeChars_ReturnsThreeChars()
        {
            var randomSource = Substitute.For<IRandomSource>();
            randomSource.Next().Returns(0.0, 0.5, 0.999999999);
            var actual = randomSource.NextString("abc", 3);
            Assert.AreEqual("abc", actual);
        }
    }
}
