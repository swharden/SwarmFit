using ScottPlot;

namespace SwarmFit.Tests;

public class Quickstart
{
    [Test]
    public void Test_Fit_Quickstart()
    {
        // data points to fit
        double[] xs = [1, 2, 3, 4, 5];
        double[] ys = [304, 229, 174, 134, 111];

        // define a fit function using any number of variables.
        static double MyFunc(double x, double[] vars)
        {
            // Y = A + B * e^(x*C)
            return vars[0] + vars[1] * Math.Exp(vars[2] * x);
        }

        // define the minimum and maximum value for each variable
        double[] minVars = [-100, -5000, -10];
        double[] maxVars = [100, 5000, 10];

        // perform the fit
        double[] solution = QuickFit.Solve(xs, ys, MyFunc, minVars, maxVars);

        // display the solution
        double a = solution[0];
        double b = solution[1];
        double c = solution[2];
        Console.WriteLine($"Y = {a:N2} + {b:N2} * e^(x * {c:N2})");
    }

    [Test]
    public void Test_Logo()
    {
        double[] xs = [7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18];
        double[] ys = [304.08994, 229.13878, 173.71886, 135.75499,
               111.096794, 94.25109, 81.55578, 71.30187,
               62.146603, 54.212032, 49.20715, 46.765743];

        static double Exponential3P(double x, double[] parameters)
        {
            return parameters[0] * Math.Exp(parameters[1] * x) + parameters[2];
        }

        ParameterLimits[] limits = [new(-5000, 5000), new(-100, 100), new(-100, 100)];
        SwarmFitter fitter = new(xs, ys, Exponential3P, limits);
        FitSolution solution = fitter.Solve(10_000);

        double[] fitYs = xs.Select(x => Exponential3P(x, solution.Parameters)).ToArray();

        Plot plot = new();
        var data = plot.Add.Markers(xs, ys);
        data.LegendText = "Data Points";

        var fit = plot.Add.ScatterLine(xs, fitYs);
        fit.LegendText = "Fitted Curve";
        fit.LineWidth = 2;
        fit.LinePattern = LinePattern.DenselyDashed;
        fit.LineColor = Colors.Black;

        plot.Legend.Alignment = Alignment.UpperRight;

        plot.Title($"Y = {solution.Parameters[2]:0.00} + {solution.Parameters[0]:0.00} * e^({solution.Parameters[1]:0.00} * x)");
        plot.Axes.Title.Label.Bold = false;

        var saved = plot.SavePng("test.png", 400, 300);
        Console.WriteLine(saved);
        //saved.LaunchInBrowser();
    }
}