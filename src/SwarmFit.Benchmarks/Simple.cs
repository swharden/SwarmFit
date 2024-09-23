using BenchmarkDotNet.Attributes;

namespace SwarmFit.Benchmarks;

[ShortRunJob]
[MemoryDiagnoser]
public class Simple
{
    [Benchmark]
    public (double a, double b, double c) SolveDemo()
    {
        // data points to fit
        double[] xs = [1, 2, 3, 4, 5];
        double[] ys = [304, 229, 174, 134, 111];
        
        static double Fit(double x, double[] vars)
        {
            // Y = A + B * e^(x*C)
            return vars[0] + vars[1] * Math.Exp(x * vars[2]);
        }

        // define the minimum and maximum value for each variable
        double[] minVars = [-100, -5000, -10];
        double[] maxVars = [100, 5000, 10];

        // perform the fit
        double[] solution = QuickFit.Solve(xs, ys, Fit, minVars, maxVars);

        // display the solution
        double a = solution[0];
        double b = solution[1];
        double c = solution[2];
        return (a, b, c);
    }
}
