namespace SwarmFit;

public class FitSolution
{
    required public double[] Variables { get; init; }
    required public double[] ErrorHistory { get; init; }
    required public TimeSpan Elapsed { get; init; }
    required public int Iterations { get; init; }
    required public int Particles { get; init; }
};