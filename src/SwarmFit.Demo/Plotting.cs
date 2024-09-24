using ScottPlot;

namespace SwarmFit.Demo;

internal static class Plotting
{
    public static void PlotDataPoints(Plot plot, double[] xs, double[] ys)
    {
        plot.Clear();
        var pts = plot.Add.Markers(xs, ys);
        pts.MarkerSize = 10;
        plot.Axes.AutoScale();
    }

    public static void PlotFitCurve(Plot plot, IFunction function, FitSolution solution)
    {
        plot.Clear<ScottPlot.Plottables.Scatter>();

        double fitXMin = plot.Axes.Bottom.Min;
        double fitXMax = plot.Axes.Bottom.Max;
        double[] fitXs = Generate.Range(fitXMin, fitXMax, (fitXMax - fitXMin) / 100);
        double[] fitYs = fitXs.Select(x => function.Function.Invoke(x, solution.Parameters)).ToArray();

        var sl = plot.Add.ScatterLine(fitXs, fitYs);
        sl.LineWidth = 2;
        sl.Color = Colors.Black;
        sl.LinePattern = LinePattern.DenselyDashed;
    }
}
