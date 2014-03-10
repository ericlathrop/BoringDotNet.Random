using NUnit.Framework;
using System;

namespace BoringDotNet.Random
{
    [TestFixture]
    public class SecureRandomSourceTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithNullGenerator_ThrowsException()
        {
            // ReSharper disable ObjectCreationAsStatement
            new SecureRandomSource(null);
            // ReSharper restore ObjectCreationAsStatement
        }

        [Test]
        public void BytesToDouble_WithZeros_ReturnsZero()
        {
            var bytes = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
            var actual = SecureRandomSource.BytesToDouble(bytes);
            Assert.AreEqual(0.0, actual);
        }

        [Test]
        public void BytesToDouble_WithOnes_ReturnsOne()
        {
            var bytes = new byte[] { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff };
            var actual = SecureRandomSource.BytesToDouble(bytes);
            Assert.AreEqual(1.0, actual);
        }

        [Test]
        public void Next_MultipleIterations_IsWithin0And1()
        {
            var source = new SecureRandomSource();
            const int iterations = 100;
            for (int i = 0; i < iterations; i++)
            {
                var actual = source.Next();
                Assert.GreaterOrEqual(actual, 0.0);
                Assert.LessOrEqual(actual, 1.0);
            }
        }
    }
}
