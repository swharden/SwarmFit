namespace SwarmFit.RandomNumberGenerators;

public class XorShift(uint seed = 0) : IRandomNumberGenerator
{
    private uint Rand = seed;

    public uint Next()
    {
        Rand ^= Rand << 13;
        Rand ^= Rand >> 17;
        Rand ^= Rand << 5;
        return Rand;
    }

    public double NextDouble()
    {
        return (double)Next() / uint.MaxValue;
    }
}
