namespace SwarmFit.RandomNumberGenerators;

internal class SystemRandom(bool randomSeed = false) : IRandomNumberGenerator
{
    private readonly Random Rand = new(randomSeed ? GetRandomSeed() : 0);

    private static int GetRandomSeed()
    {
        var RNG = System.Security.Cryptography.RandomNumberGenerator.Create();
        byte[] data = new byte[sizeof(int)];
        RNG.GetBytes(data);
        return BitConverter.ToInt32(data, 0) & (int.MaxValue - 1);
    }

    public double NextDouble()
    {
        return Rand.NextDouble();
    }
}
