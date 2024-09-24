namespace SwarmFit.Tests;

class RandomNumberGeneratorTests
{
    [Test]
    public void Test_SystemRandom_ValuesAreUnique()
    {
        // NOTE: a duplicate is found by luck after 66k iterations
        AssertNoDuplicates(new RandomNumberGenerators.SystemRandom(randomSeed: false), 50_000);
    }

    [Test]
    public void Test_XorShift_ValuesAreUnique()
    {
        AssertNoDuplicates(new RandomNumberGenerators.XorShift(), 100_000);
    }

    private static void AssertNoDuplicates(IRandomNumberGenerator RNG, int count)
    {
        HashSet<double> seen = [];
        for (int i = 0; i < count; i++)
        {
            double value = RNG.NextDouble();
            if (seen.Contains(value))
                throw new InvalidOperationException($"duplicate number found after {i} iterations");
            seen.Add(value);
        }
    }
}
