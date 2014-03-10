using System;
using System.Security.Cryptography;

namespace BoringDotNet.Random
{
    public class SecureRandomSource : IRandomSource
    {
        private readonly RandomNumberGenerator _rng;

        public SecureRandomSource()
            : this(RandomNumberGenerator.Create())
        {
        }

        public SecureRandomSource(RandomNumberGenerator rng)
        {
            if (rng == null)
                throw new ArgumentNullException("rng");
            _rng = rng;
        }

        public double Next()
        {
            var bytes = new byte[8];
            _rng.GetBytes(bytes);
            return BytesToDouble(bytes);
        }

        internal static double BytesToDouble(byte[] bytes)
        {
            ulong uintResult = BitConverter.ToUInt64(bytes, 0);
            return (double)uintResult / UInt64.MaxValue;
        }
    }
}
