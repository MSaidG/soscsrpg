using System.Security.Cryptography;

namespace Engine.Factories
{
    public static class RandomNumberGenerator
    {
        private static readonly RNGCryptoServiceProvider _generator =
            new RNGCryptoServiceProvider();

        public static int NumberBetween(int minValue, int maxValue)
        {
            byte[] randomNumber = new byte[1];
            _generator.GetBytes(randomNumber);
            double asciiValueOfRandomCharacter = Convert.ToDouble(randomNumber[0]);

            double multiplier = Math.Max(0, (asciiValueOfRandomCharacter / 255d) - 0.00000000001d);

            int range = maxValue - minValue + 1;
            double randomValueInRange = Math.Floor(multiplier * range);

            return (int)(minValue + randomValueInRange);
        }

        private static readonly Random _simpleGenerator = new Random();
        public static int SimpleNumberBetween(int minValue, int maxValue)
        {
            return _simpleGenerator.Next(minValue, maxValue);
        }
    }
}
