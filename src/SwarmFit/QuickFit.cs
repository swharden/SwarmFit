namespace SwarmFit;

public static class QuickFit
{
    public static double[] Solve(double[] xs, double[] ys, Func<double, double[], double> func, double[] minVars, double[] maxVars, int particles = 100, int iterations = 10_000)
    {
        if (minVars.Length != maxVars.Length)
        {
            throw new ArgumentException($"{nameof(minVars)} and {nameof(maxVars)} must have equal length");
        }

        VariableLimits[] limits = Enumerable
            .Range(0, minVars.Length)
            .Select(x => new VariableLimits(minVars[x], maxVars[x]))
            .ToArray();

        SwarmFitter fit = new(xs, ys, func, limits)
        {
            NumParticles = 100
        };

        return fit.Solve(iterations).Variables;
    }
}
