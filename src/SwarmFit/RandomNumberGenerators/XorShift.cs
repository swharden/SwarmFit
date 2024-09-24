namespace SwarmFit.RandomNumberGenerators;

// See: https://en.wikipedia.org/wiki/Xorshift
// Note: 2463534242 in binary is 10010010110101101000110010100010 which has a nice balance of 1s and 0s
public class XorShift(uint seed = 2463534242) : IRandomNumberGenerator
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
