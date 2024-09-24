namespace SwarmFit.Tests;

internal static class Plotting
{
    public static ScottPlot.Plot PlotFit(double[] xs, double[] ys, Func<double, double[], double> func, double[] solution)
    {
        ScottPlot.Plot plot = new();

        var marks = plot.Add.ScatterPoints(xs, ys);
        marks.MarkerSize = 10;
        plot.Axes.AutoScale();
        
        var limits = plot.Axes.GetLimits();
        double[] fitXs = ScottPlot.Generate.Range(limits.Left, limits.Right, limits.HorizontalSpan / 100);
        double[] fitYs = fitXs.Select(x => func(x, solution)).ToArray();
        var line = plot.Add.ScatterLine(fitXs, fitYs);
        line.LineWidth = 2;

        return plot;
    }
}
