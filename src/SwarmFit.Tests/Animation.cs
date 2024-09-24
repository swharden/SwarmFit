using ScottPlot;
using ScottPlot.Plottables;

namespace SwarmFit.Tests;

public class Animation
{
    [Ignore("only used for creating graphics for the website")]
    [Test]
    public void Test_Fit_Rainbow()
    {
        double[] xs = [1, 2, 3, 4, 5];
        double[] ys = [304, 229, 174, 134, 111];

        static double MyFunc(double x, double[] vars)
        {
            // Y = A + B * e^(x*C)
            return vars[0] + vars[1] * Math.Exp(x * vars[2]);
        }

        ParameterLimits[] limits = [
            new(0, 500),
            new(0, 2000),
            new (-10, 10)
        ];

        SwarmFitter fit = new(xs, ys, MyFunc, limits);

        FitSolution solution = fit.Solve();

        Plot plot = new();
        double a = solution.Parameters[0];
        double b = solution.Parameters[1];
        double c = solution.Parameters[2];
        string formula = $"Y = {a:N2} + {b:N2} * e^(x * {c:N2})";
        plot.Title(formula);

        var dataPoints = plot.Add.ScatterPoints(fit.Xs, fit.Ys);
        dataPoints.MarkerSize = 15;
        dataPoints.MarkerShape = MarkerShape.OpenCircle;
        dataPoints.MarkerLineWidth = 2;
        dataPoints.Color = Colors.Black;

        plot.Axes.AutoScale();
        plot.Axes.ZoomOut(1.5, 1.5);
        double[] fitXs = Generate.Range(plot.Axes.Bottom.Range.Min, plot.Axes.Bottom.Range.Max, plot.Axes.Bottom.Range.Span / 100);
        double[] fitYs = fitXs.Select(x => MyFunc(x, solution.Parameters)).ToArray();

        /*
        if (solution.History is not null)
        {
            ScottPlot.Colormaps.Turbo cmap = new();
            for (int i = 0; i < solution.History.Length; i += 5)
            {
                FitSolution intermediateSolution = solution.History[i];
                Console.WriteLine(intermediateSolution);
                double[] tempYs = fitXs.Select(x => MyFunc(x, intermediateSolution.Parameters)).ToArray();
                var tempLine = plot.Add.ScatterLine(fitXs, tempYs);
                tempLine.Color = cmap.GetColor(i, solution.History.Length).WithAlpha(.5);
                tempLine.LineWidth = 2;
            }
        }
        */

        plot.SavePng("test.png", 400, 300).LaunchInBrowser();

        Console.WriteLine(solution);
    }

    [Ignore("only used for creating graphics for the website")]
    [Test]
    public void Test_Fit_Animation()
    {
        double[] xs = [1, 2, 3, 4, 5];
        double[] ys = [304, 229, 174, 134, 111];

        static double MyFunc(double x, double[] vars)
        {
            // Y = A + B * e^(x*C)
            return vars[0] + vars[1] * Math.Exp(x * vars[2]);
        }

        ParameterLimits[] limits = [
            new(0, 500),
            new(0, 2000),
            new (-10, 10)
        ];

        SwarmFitter fit = new(xs, ys, MyFunc, limits);

        FitSolution solution = fit.Solve(400);

        Plot plot = new();

        var dataPoints = plot.Add.ScatterPoints(fit.Xs, fit.Ys);
        dataPoints.MarkerSize = 15;
        dataPoints.MarkerShape = MarkerShape.OpenCircle;
        dataPoints.MarkerLineWidth = 2;
        dataPoints.Color = Colors.Black;

        plot.Axes.AutoScale();
        plot.Axes.ZoomOut(1.5, 1.5);

        if (Directory.Exists("animation"))
            Directory.Delete("animation", true);
        Directory.CreateDirectory("animation");

        /*
        if (solution.History is not null)
        {
            double[] fitXs = Generate.Range(plot.Axes.Bottom.Range.Min, plot.Axes.Bottom.Range.Max, plot.Axes.Bottom.Range.Span / 100);

            ScottPlot.Colormaps.Turbo cmap = new();
            for (int i = 0; i < solution.History.Length; i++)
            {
                plot.GetPlottables<Scatter>().Where(x => x.LineWidth > 0).ToList().ForEach(plot.Remove);

                FitSolution intermediateSolution = solution.History[i];
                Console.WriteLine(intermediateSolution);
                double[] tempYs = fitXs.Select(x => MyFunc(x, intermediateSolution.Parameters)).ToArray();
                var tempLine = plot.Add.ScatterLine(fitXs, tempYs);
                tempLine.Color = cmap.GetColor(i, solution.History.Length).WithAlpha(.8);
                tempLine.LineWidth = 4;

                double a = intermediateSolution.Parameters[0];
                double b = intermediateSolution.Parameters[1];
                double c = intermediateSolution.Parameters[2];
                string formula = $"Y = {a:N2} + {b:N2} * e^(x * {c:N2})";
                plot.Title(formula);

                plot.SavePng($"animation/test-{i:0000}.png", 400, 300);
            }
        }
        */

        Console.WriteLine(solution);
    }
}
