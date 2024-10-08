﻿namespace SwarmFit;

public static class QuickFit
{
    /// <summary>
    /// This function provides a simple API for fitting parameters of a function to find the best for for a collection of X/Y data points.
    /// The fitter will attempt continue to optimize parameters until 500 iterations yields no further improvement.
    /// The swarm fitter has many configuration options which are not available when calling this function, so advanced users
    /// are encouraged to instantiate a <see cref="SwarmFitter"/> and interact it with directly to find the ideal solution.
    /// </summary>
    /// <param name="xs">horizontal data values to fit</param>
    /// <param name="ys">vertical data values to fit</param>
    /// <param name="func">a user defined function which calculates Y given X according to a collection of parameters</param>
    /// <param name="parameterMins">minimum possible value for each parameter</param>
    /// <param name="parameterMaxes">maximum possible value for each parameter</param>
    /// <param name="numParticles">Number of particles to use for fitting</param>
    /// <param name="maxIterations">Fitting will be aborted after this number of total iterations is performed</param>
    /// <returns>optimized parameters for the curve</returns>
    /// <exception cref="ArgumentException"></exception>
    public static double[] Solve(double[] xs, double[] ys, Func<double, double[], double> func, double[] parameterMins, double[] parameterMaxes, int numParticles = 10, int maxIterations = 10_000)
    {
        if (parameterMins.Length != parameterMaxes.Length)
        {
            throw new ArgumentException($"{nameof(parameterMins)} and {nameof(parameterMaxes)} must have equal length");
        }

        ParameterLimits[] limits = Enumerable
            .Range(0, parameterMins.Length)
            .Select(x => new ParameterLimits(parameterMins[x], parameterMaxes[x]))
            .ToArray();

        SwarmFitter fitter = new(xs, ys, func, limits, numParticles);

        while (fitter.IterationCount < maxIterations)
        {
            int originalImprovementCount = fitter.ImprovementCount;
            FitSolution solution = fitter.Solve(maxIterations: 500);
            if (fitter.ImprovementCount == originalImprovementCount)
                break;
        }

        return fitter.BestSolution.Parameters;
    }
}
