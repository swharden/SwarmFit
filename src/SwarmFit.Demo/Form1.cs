using ScottPlot;

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

        Load += (s, e) => btnExp2P.PerformClick();
    }

    void Fit(Func<double, double[], double> fitFunc, VariableLimits[] limits)
    {
        double[] vars = limits.Select(x => x.Random(Random.Shared)).ToArray();
        double[] xs = ScottPlot.Generate.RandomSample(10, 0, 10);
        double[] ys = xs.Select(x => fitFunc.Invoke(x, vars)).ToArray();

        if (checkNoise.Checked)
        {
            double noiseFrac = 0.2;
            double xSpan = xs.Max() - xs.Min();
            double ySpan = ys.Max() - ys.Min();
            double noiseX = noiseFrac * xSpan;
            double noiseY = noiseFrac * ySpan;
            for (int i = 0; i < xs.Length; i++)
            {
                xs[i] += (Random.Shared.NextDouble() - 0.5) * noiseX;
                ys[i] += (Random.Shared.NextDouble() - 0.5) * noiseY;
            }
        }

        SwarmFitter fitter = new(xs, ys, fitFunc, limits) { StoreIntermediateSolutions = true };

        FitSolution solution = fitter.Solve();
        label1.Text = $"Fit achieved in {solution.Elapsed.TotalMilliseconds:N2} msec after {solution.Iterations:N0} iterations using {solution.Particles:N0} particles";

        PlotFitCurve(xs, ys, fitFunc, solution);
        PlotError(solution);
    }

    void PlotFitCurve(double[] xs, double[] ys, Func<double, double[], double> fitFunc, FitSolution solution)
    {
        formsPlot1.Plot.Clear();

        var sp = formsPlot1.Plot.Add.ScatterPoints(xs, ys);
        sp.MarkerSize = 10;
        sp.Color = Colors.C0.WithAlpha(.8);

        formsPlot1.Plot.Axes.AutoScale();
        formsPlot1.Plot.Axes.ZoomOut(2, 2);

        double fitXMin = formsPlot1.Plot.Axes.Bottom.Min;
        double fitXMax = formsPlot1.Plot.Axes.Bottom.Max;
        double[] fitXs = Generate.Range(fitXMin, fitXMax, (fitXMax - fitXMin) / 100);
        double[] fitYs = fitXs.Select(x => fitFunc.Invoke(x, solution.Variables)).ToArray();

        var sl = formsPlot1.Plot.Add.ScatterLine(fitXs, fitYs);
        sl.LineWidth = 2;
        sl.Color = Colors.Black;
        sl.LinePattern = LinePattern.DenselyDashed;

        formsPlot1.Refresh();
    }

    void PlotError(FitSolution solution)
    {
        if (solution.History is null)
            return;

        double[] errors = solution.History.Select(x => x.Error).ToArray();
        int[] epochs = Enumerable.Range(1, errors.Length).ToArray();
        double finalError = errors.Last();
        double[] logDeltaError = errors.Select(x => x - finalError).Where(x => x > 0).Select(Math.Log10).ToArray();
        formsPlot2.Plot.Clear();
        var sp = formsPlot2.Plot.Add.ScatterLine(epochs, logDeltaError);
        sp.LineWidth = 2;
        formsPlot2.Plot.Axes.AutoScale();
        formsPlot2.Plot.YLabel($"Relative Error (10^x)");
        formsPlot2.Plot.XLabel($"Epoch Number");
        formsPlot2.Refresh();
    }
}
