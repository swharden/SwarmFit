using ScottPlot;

namespace SwarmFit.Tests;

public class Tests
{
    [Test]
    public void Test_Fit()
    {
        double[] xs = SampleData.Range(0, 2, .1);
        double[] ys = SampleData.Exponential(xs, 5, 3, 2);

        SwarmFitter fitter = new(xs, ys, 3);
        fitter.Fit(1000);
        Console.WriteLine($"Fit achieved in {fitter.Elapsed.TotalMilliseconds} ms");
        Console.WriteLine(fitter);

        Plot plot = new();
        plot.Add.Markers(xs, ys);
        plot.Add.ScatterLine(xs, fitter.GetYs(xs));

        var saved = plot.SavePng("test.png", 400, 300);
        Console.WriteLine(saved);
        //saved.LaunchInBrowser();
    }

    [Test]
    public void Test_Logo()
    {
        double[] xs = Generate.Consecutive(12, 1, 7);
        double[] ys = [304.08994, 229.13878, 173.71886, 135.75499,
               111.096794, 94.25109, 81.55578, 71.30187,
               62.146603, 54.212032, 49.20715, 46.765743];

        SwarmFitter fitter = new(xs, ys, 3);
        fitter.SetRange(0, 0, 100);
        fitter.SetRange(1, 0, 5000);
        fitter.SetRange(2, -10, 10);

        fitter.Fit(10_000);
        Console.WriteLine($"Fit achieved in {fitter.Elapsed.TotalMilliseconds} ms");
        Console.WriteLine(fitter);

        Plot plot = new();
        var data = plot.Add.Markers(xs, ys);
        data.LegendText = "Data Points";

        var fit = plot.Add.ScatterLine(xs, fitter.GetYs(xs));
        fit.LegendText = "Fitted Curve";
        fit.LineWidth = 2;
        fit.LinePattern = LinePattern.DenselyDashed;
        fit.LineColor = Colors.Black;

        plot.Legend.Alignment = Alignment.UpperRight;

        plot.Title($"Y = {fitter.Solution[0]:0.00} + {fitter.Solution[1]:0.00} * e^({fitter.Solution[2]:0.00} * x)");
        plot.Axes.Title.Label.Bold = false;
        //plot.Axes.Title.Label.FontName = ScottPlot.Fonts.Serif;

        var saved = plot.SavePng("test.png", 400, 300);
        Console.WriteLine(saved);
        //saved.LaunchInBrowser();
    }
}