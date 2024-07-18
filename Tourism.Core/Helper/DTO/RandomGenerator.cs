namespace Tourism.Core.Helper.DTO
{
    public static class RandomGenerator
    {
        private static Random _random = new Random();
        public static int Generate(int min, int max)
        {
            return _random.Next(min, max);
        }

    }
}
