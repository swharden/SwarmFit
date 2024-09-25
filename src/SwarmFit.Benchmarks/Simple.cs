using BenchmarkDotNet.Attributes;

namespace SwarmFit.Benchmarks;

[ShortRunJob]
[MemoryDiagnoser]
public class Simple
{
    [Benchmark]
    public void SolveDemo()
    {
        // data points to fit
        double[] xs = [1, 2, 3, 4, 5];
        double[] ys = [304, 229, 174, 134, 111];

        // a custom function to fit to the data using any number of parameters
        static double MyFitFunc(double x, double[] parameters)
        {
            double a = parameters[0];
            double b = parameters[1];
            double c = parameters[2];
            return a + b * Math.Exp(x * c);
        }

        // the minimum and maximum value for each parameter
        double[] minVars = [-100, -5000, -10];
        double[] maxVars = [100, 5000, 10];

        // perform the fit with general purpose settings
        double[] solution = QuickFit.Solve(xs, ys, MyFitFunc, minVars, maxVars);

        // display the solution
        double a = solution[0];
        double b = solution[1];
        double c = solution[2];
    }
}
