namespace SwarmFit.RandomNumberGenerators;

public class CruptoRandom : IRandomNumberGenerator
{
    private readonly System.Security.Cryptography.RandomNumberGenerator Rand = System.Security.Cryptography.RandomNumberGenerator.Create();

    readonly byte[] bytes = new byte[sizeof(int)];

    public double NextDouble()
    {
        Rand.GetBytes(bytes);
        return BitConverter.ToInt32(bytes, 0) & (int.MaxValue - 1);
    }
}
