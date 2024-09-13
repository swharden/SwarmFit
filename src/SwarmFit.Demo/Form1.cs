namespace SwarmFit.Demo;

public partial class Form1 : Form
{
    double[]? Xs = null;
    double[]? Ys = null;

    public Form1()
    {
        InitializeComponent();
        btnRandomize.Click += (s, e) => { Randomize(); Fit(); };
        //btnStep.Click += (s, e) => Fit();
    }

    // Y = A + B * e^(x * C)
    static double MyFunc(double x, double[] vars) => vars[0] * Math.Exp(vars[1] * x);

    void Randomize()
    {
        double[] vars =
        [
            ScottPlot.Generate.RandomNumber(-100, 100),
            ScottPlot.Generate.RandomNumber(-1, 1),
        ];

        int pointCount = 10;
        Xs = ScottPlot.Generate.RandomSample(pointCount, 0, 10);
        Ys = Xs.Select(x => MyFunc(x, vars)).ToArray();

        formsPlotData.Plot.Clear();
        var sp = formsPlotData.Plot.Add.ScatterPoints(Xs, Ys);
        sp.MarkerSize = 10;
        formsPlotData.Plot.Axes.AutoScale();
        formsPlotData.Plot.Axes.ZoomOut(2, 2);
        formsPlotData.Refresh();
    }

    void Fit()
    {
        if (Xs is null)
            return;
        if (Ys is null)
            return;

        double[] xs = [1, 2, 3, 4, 5, 6, 7];
        double[] ys = [258, 183, 127, 89, 65, 48, 35];

        VariableLimits[] variableLimits = [
            new(-500, 500),
            new(-1, 1),
        ];

        SwarmFitter fitter = new(Xs, Ys, StandardFunctions.Exponential2P, variableLimits);
        double[] vars = fitter.Solve();

        double x1 = formsPlotData.Plot.Axes.Bottom.Min;
        double x2 = formsPlotData.Plot.Axes.Bottom.Max;
        double[] fitXs = ScottPlot.Generate.Range(x1, x2, (x2 - x1) / 100);
        double[] fitYs = fitXs.Select(x => fitter.GetY(x, vars)).ToArray();

        formsPlotData.Plot
            .GetPlottables<ScottPlot.Plottables.Scatter>()
            .Where(x => x.LineWidth > 0)
            .ToList()
            .ForEach(formsPlotData.Plot.Remove);

        var sl = formsPlotData.Plot.Add.ScatterLine(fitXs, fitYs);
        sl.LineWidth = 2;
        formsPlotData.Refresh();
    }
}
