namespace SwarmFit.Demo;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();

        btnExp2P.Click += (s, e) =>
        {
            Func<double, double[], double> fitFunc = StandardFunctions.Exponential2P;
            VariableLimits[] limits = [new(-500, 500), new(-1, 1)];
            Fit(fitFunc, limits);
        };

        btnExp3P.Click += (s, e) =>
        {
            Func<double, double[], double> fitFunc = StandardFunctions.Exponential3P;
            VariableLimits[] limits = [new(-500, 500), new(-1, 1), new(-100, 100)];
            Fit(fitFunc, limits);
        };
    }

    void Fit(Func<double, double[], double> fitFunc, VariableLimits[] limits)
    {
        double[] vars = limits.Select(x => x.Random(Random.Shared)).ToArray();
        double[] xs = ScottPlot.Generate.RandomSample(10, 0, 10);
        double[] ys = xs.Select(x => fitFunc.Invoke(x, vars)).ToArray();

        SwarmFitter fitter = new(xs, ys, fitFunc, limits);
        double[] fitVars = fitter.Solve();
        PlotFitCurve(xs, ys, fitFunc, fitVars);
    }

    void PlotFitCurve(double[] xs, double[] ys, Func<double, double[], double> fitFunc, double[] vars)
    {
        formsPlot1.Plot.Clear();

        var sp = formsPlot1.Plot.Add.ScatterPoints(xs, ys);
        sp.MarkerSize = 10;
        formsPlot1.Plot.Axes.AutoScale();
        formsPlot1.Plot.Axes.ZoomOut(2, 2);

        double fitXMin = formsPlot1.Plot.Axes.Bottom.Min;
        double fitXMax = formsPlot1.Plot.Axes.Bottom.Max;
        double[] fitXs = ScottPlot.Generate.Range(fitXMin, fitXMax, (fitXMax - fitXMin) / 100);
        double[] fitYs = fitXs.Select(x => fitFunc.Invoke(x, vars)).ToArray();

        var sl = formsPlot1.Plot.Add.ScatterLine(fitXs, fitYs);
        sl.LineWidth = 2;
        formsPlot1.Refresh();
    }
}
