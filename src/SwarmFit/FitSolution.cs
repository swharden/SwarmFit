namespace SwarmFit;

public class FitSolution(double[] vars, double[] errors, TimeSpan elapsed, int iterations, int particles)
{
    public double[] Variables { get; } = vars;
    public double[] ErrorHistory { get; } = errors;
    public TimeSpan Elapsed { get; } = elapsed;
    public int Iterations { get; } = iterations;
    public int Particles { get; } = particles;
};