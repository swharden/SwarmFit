using ScottPlot;

namespace SwarmFit.Tests;

public class Animation
{
    [Ignore("only used for creating graphics for the website")]
    [Test]
    public void Test_Fit_Rainbow()
    {

        static double MyFunc(double x, double[] parameters)
        {
            double a = parameters[0];
            double b = parameters[1];
            double c = parameters[2];
            return a + b * Math.Exp(-x * c);
        }

        double[] parameters = [5, 7, .7];
        double[] xs = ScottPlot.Generate.Consecutive(10, first: 1);
        double[] ys = xs.Select(x => MyFunc(x, parameters)).ToArray();
        ScottPlot.Generate.AddNoiseInPlace(xs, .3);
        ScottPlot.Generate.AddNoiseInPlace(ys, .3);

        ParameterLimits[] limits = [
            new(0, 20),
            new(0, 20),
            new (.1, 10)
        ];

        SwarmFitter fitter = new(xs, ys, MyFunc, limits);

        Plot plot = new();

        var dataPoints = plot.Add.Markers(fitter.Xs, fitter.Ys);
        dataPoints.MarkerSize = 15;
        dataPoints.MarkerShape = MarkerShape.OpenCircle;
        dataPoints.MarkerLineWidth = 2;
        dataPoints.Color = Colors.Black;

        plot.Axes.AutoScale();
        plot.Axes.ZoomOut(1.5, 1.5);
        double[] fitXs = Generate.Range(plot.Axes.Bottom.Range.Min, plot.Axes.Bottom.Range.Max, plot.Axes.Bottom.Range.Span / 100);
        ScottPlot.Colormaps.Turbo cmap = new();

        int improvements = 150;
        for (int i = 0; i < improvements; i++)
        {
            FitSolution solution = fitter.Solve(maxImprovements: 1);

            double a = solution.Parameters[0];
            double b = solution.Parameters[1];
            double c = solution.Parameters[2];
            string formula = $"Y = {a:N3} + {b:N3} * e^(-x * {c:N3})";
            plot.Title(formula);

            plot.Clear<ScottPlot.Plottables.Scatter>();
            double[] fitYs = fitXs.Select(x => MyFunc(x, solution.Parameters)).ToArray();
            var tempLine = plot.Add.ScatterLine(fitXs, fitYs);
            tempLine.Color = cmap.GetColor(i, improvements, .2, 1).WithAlpha(.7);
            tempLine.LineWidth = 4;

            plot.SavePng($"animation/test-{i:0000}.png", 400, 300);
        }
    }
}
