namespace SwarmFit;

public class FitSolution(double[] parameters, double error, TimeSpan elapsed, int iterations, int particles)
{
    public double[] Parameters { get; } = [.. parameters];
    public double Error { get; } = error;
    public TimeSpan Elapsed { get; } = elapsed;
    public int Iterations { get; } = iterations;
    public int Particles { get; } = particles;

    public override string ToString()
    {
        string parameters = string.Join(", ", Parameters.Select(x => x.ToString()));
        return $"Solution [{parameters}] achieved in {Elapsed.TotalMilliseconds} msec using {Particles} particles and {Iterations} iterations";
    }
};