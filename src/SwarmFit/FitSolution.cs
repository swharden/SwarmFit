namespace SwarmFit;

public class FitSolution(double[] vars, double[] errors, TimeSpan elapsed, int iterations, int particles)
{
    public double[] Variables { get; } = vars;
    public double[] ErrorHistory { get; } = errors;
    public TimeSpan Elapsed { get; } = elapsed;
    public int Iterations { get; } = iterations;
    public int Particles { get; } = particles;

    public override string ToString()
    {
        string vars = string.Join(", ", Variables.Select(x => x.ToString()));
        return $"Solution [{vars}] achieved in {Elapsed.TotalMilliseconds} msec using {Particles} particles and {Iterations} iterations";
    }
};