namespace SwarmFit.RandomNumberGenerators;

public class XorShift(uint seed = 2463534242) : IRandomNumberGenerator
{
    // note: 2463534242 in binary is 10010010110101101000110010100010 which has a nice balance of 1s and 0s

    private uint Rand = seed != 0 ? seed : throw new InvalidOperationException("Don't seed me with a 0 or you'll always get 0 back");

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
