using FluentAssertions;

namespace SwarmFit.Tests;

public class KnownSolutions
{
    [Test]
    public void Test_Fit_Values()
    {
        // Note: these points aren't from a perfect curve (they contain noise)
        double[] xs = [7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18];
        double[] ys = [304.08994, 229.13878, 173.71886, 135.75499,
               111.096794, 94.25109, 81.55578, 71.30187,
               62.146603, 54.212032, 49.20715, 46.765743];

        // This function represents the equation: Y = A + B * e^(x*C)
        static double MyFunc(double x, double[] vars) => vars[0] + vars[1] * Math.Exp(vars[2] * x);

        // Define limits for each variable
        double[] minVars = [-100, -5000, -10];
        double[] maxVars = [100, 5000, 10];

        // Find a solution for the best parameters to fit the curve to the data points
        double[] solution = QuickFit.Solve(xs, ys, MyFunc, minVars, maxVars);

        solution[0].Should().BeApproximately(40.490, 0.002);
        solution[1].Should().BeApproximately(2594.155, 1);
        solution[2].Should().BeApproximately(-0.327, 0.002);

        Plotting.PlotFit(xs, ys, MyFunc, solution).SavePng("Test_Fit_Values.png", 400, 300);
    }
}
