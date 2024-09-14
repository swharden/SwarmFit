using ScottPlot;
using ScottPlot.Control;

namespace SwarmFit.Tests;

public class Animation
{
    [Test]
    public void Test_Fit_WithAnimation()
    {
        double[] xs = [1, 2, 3, 4, 5];
        double[] ys = [304, 229, 174, 134, 111];

        static double MyFunc(double x, double[] vars)
        {
            // Y = A + B * e^(x*C)
            return vars[0] + vars[1] * Math.Exp(x * vars[2]);
        }

        VariableLimits[] limits = [
            new(0, 500),
            new(0, 2000),
            new (-10, 10)
        ];

        SwarmFitter fit = new(xs, ys, MyFunc, limits);

        FitSolution solution = fit.Solve();
        Console.WriteLine(solution);

        ScottPlot.Plot plot = new();

        var dataPoints = plot.Add.ScatterPoints(fit.Xs, fit.Ys);
        dataPoints.MarkerSize = 15;

        plot.Axes.AutoScale();
        plot.Axes.ZoomOut(1.5, 1.5);
        double[] fitXs = ScottPlot.Generate.Range(plot.Axes.Bottom.Range.Min, plot.Axes.Bottom.Range.Max, plot.Axes.Bottom.Range.Span / 100);
        double[] fitYs = fitXs.Select(x => MyFunc(x, solution.Variables)).ToArray();

        var fitLine = plot.Add.ScatterLine(fitXs, fitYs);
        fitLine.LineWidth = 2;
        fitLine.Color = Colors.Black;
        fitLine.LinePattern = LinePattern.DenselyDashed;

        double a = solution.Variables[0];
        double b = solution.Variables[1];
        double c = solution.Variables[2];
        string formula = $"Y = {a:N2} + {b:N2} * e^(x * {c:N2})";
        plot.Title(formula);

        plot.SavePng("test.png", 400, 300).LaunchInBrowser();
    }
}
