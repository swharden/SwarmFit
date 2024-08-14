namespace SwarmFit.Tests;

public class Tests
{
    [Test]
    public void Test1()
    {
        double[] xs = SampleData.Range(0, 2, .1);
        double[] ys = SampleData.Exponential(xs, 5, 3, 2);

        SwarmFitter fitter = new(xs, ys, 3);
        fitter.Fit(1000);
        Console.WriteLine($"Fit achieved in {fitter.Elapsed.TotalMilliseconds} ms");
        Console.WriteLine(fitter);

        ScottPlot.Plot plot = new();
        plot.Add.Markers(xs, ys);
        plot.Add.ScatterLine(xs, fitter.GetYs(xs));

        plot.SavePng("test.png", 800, 600).LaunchInBrowser();
    }
}