using System;

namespace BoringDotNet.Random
{
    // ReSharper disable InconsistentNaming
    public static class IRandomSourceExtensions
    // ReSharper restore InconsistentNaming
    {
        /// <summary>
        /// Get a random integer between <para>minValue</para> (inclusive) and <para>maxValue</para> (exclusive).
        /// </summary>
        public static int NextInt(this IRandomSource random, int minValue, int maxValue)
        {
            double doubleResult = random.Next();
            double candidate = minValue + (doubleResult * (maxValue - minValue)) - 0.5;
            double rounded = Math.Round(candidate, 0, MidpointRounding.AwayFromZero);
            return (int)Math.Min(Math.Max(rounded, minValue), maxValue);
        }

        public static string NextString(this IRandomSource random, string validCharacters, int length)
        {
            if (validCharacters == null)
                throw new ArgumentNullException("validCharacters");
            if (validCharacters.Length == 0)
                throw new ArgumentException("validCharacters must constain at least one character", "validCharacters");
            if (length < 0)
                throw new ArgumentOutOfRangeException("length", length, "length must be >= 0");

            string result = string.Empty;
            for (int i = 0; i < length; i++)
                result += random.NextCharacter(validCharacters);
            return result;
        }

        private static char NextCharacter(this IRandomSource random, string validCharacters)
        {
            var i = random.NextInt(0, validCharacters.Length);
            return validCharacters[i];
        }
    }
}
