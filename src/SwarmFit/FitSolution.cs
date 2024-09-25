namespace SwarmFit;

public class FitSolution(double[] parameters, double error, TimeSpan elapsed, int iterations, int improvements)
{
    public double[] Parameters { get; } = [.. parameters];
    public double Error { get; } = error;
    public TimeSpan Elapsed { get; } = elapsed;
    public int Iterations { get; } = iterations;
    public int ImprovementCount { get; } = improvements;
};