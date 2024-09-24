using System.Runtime.CompilerServices;

namespace SwarmFit;

public readonly struct VariableLimits(double min, double max)
{
    public double Min { get; } = Math.Min(min, max);
    public double Max { get; } = Math.Max(min, max);
    public double Mid => (Min + Max) / 2;
    public double Span => Max - Min;
    public double Random(IRandomNumberGenerator rand) => Span * rand.NextDouble() + Min;
    public double Random(Random rand) => Span * rand.NextDouble() + Min;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Clamp(double value)
    {
        if (value < Min) return Min;
        else if (value > Max) return Max;
        else return value;
    }
}
