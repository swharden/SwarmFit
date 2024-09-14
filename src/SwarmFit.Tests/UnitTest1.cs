using FluentAssertions;
using ScottPlot;

namespace SwarmFit.Tests;

public class Tests
{
    [Test]
    public void Test_Logo()
    {
        double[] xs = [7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18];
        double[] ys = [304.08994, 229.13878, 173.71886, 135.75499,
               111.096794, 94.25109, 81.55578, 71.30187,
               62.146603, 54.212032, 49.20715, 46.765743];

        Func<double, double[], double> func = StandardFunctions.Exponential3P;
        VariableLimits[] limits = [new(-5000, 5000), new(-100, 100), new(-100, 100)];
        SwarmFitter fitter = new(xs, ys, func, limits);
        FitSolution solution = fitter.Solve(10_000);

        double[] fitYs = xs.Select(x => func.Invoke(x, solution.Variables)).ToArray();

        Plot plot = new();
        var data = plot.Add.Markers(xs, ys);
        data.LegendText = "Data Points";

        var fit = plot.Add.ScatterLine(xs, fitYs);
        fit.LegendText = "Fitted Curve";
        fit.LineWidth = 2;
        fit.LinePattern = LinePattern.DenselyDashed;
        fit.LineColor = Colors.Black;

        plot.Legend.Alignment = Alignment.UpperRight;

        plot.Title($"Y = {solution.Variables[2]:0.00} + {solution.Variables[0]:0.00} * e^({solution.Variables[1]:0.00} * x)");
        plot.Axes.Title.Label.Bold = false;

        var saved = plot.SavePng("test.png", 400, 300);
        Console.WriteLine(saved);
        //saved.LaunchInBrowser();
    }

    [Test]
    public void Test_Fit_Values()
    {
        double[] xs = [7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18];
        double[] ys = [304.08994, 229.13878, 173.71886, 135.75499,
               111.096794, 94.25109, 81.55578, 71.30187,
               62.146603, 54.212032, 49.20715, 46.765743];

        Func<double, double[], double> func = StandardFunctions.Exponential3P;
        double[] minVars = [-5000, -10, -100];
        double[] maxVars = [5000, 10, 100];
        double[] solution = QuickFit.Solve(xs, ys, func, minVars, maxVars);

        solution[0].Should().Be(2606.6074457530053);
        solution[1].Should().Be(-0.32811539608184037);
        solution[2].Should().Be(40.53170947533161);
    }
}