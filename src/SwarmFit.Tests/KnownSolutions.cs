using FluentAssertions;

namespace SwarmFit.Tests;

public class KnownSolutions
{
    [Test]
    public void Test_Fit_Values()
    {
        double[] parameters = [5, 7, 0.3];
        static double MyFunc(double x, double[] p) => p[0] + p[1] * Math.Exp(p[2] * x);

        double[] xs = [7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18];
        double[] ys = xs.Select(x => MyFunc(x, parameters)).ToArray();

        // Define limits for each variable
        double[] minVars = [0, 0, 0];
        double[] maxVars = [10, 10, 1];

        // Find a solution for the best parameters to fit the curve to the data points
        double[] solution = QuickFit.Solve(xs, ys, MyFunc, minVars, maxVars);
        solution[0].Should().BeApproximately(parameters[0], .01);
        solution[1].Should().BeApproximately(parameters[1], .01);
        solution[2].Should().BeApproximately(parameters[2], .01);

        Plotting.PlotFit(xs, ys, MyFunc, solution).SavePng("Test_Fit_Values.png", 400, 300);
    }
}
