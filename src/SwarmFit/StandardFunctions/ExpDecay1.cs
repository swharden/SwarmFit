namespace SwarmFit.StandardFunctions;

// https://www.originlab.com/doc/en/Origin-Help/ExpDecay1-FitFunc
public class ExpDecay1 : IFunction
{
    public string Name => "One-phase exponential decay function with time offset";
    public string Formula() => "Y = y0 + a * Exp(-(x-x0)/t)";
    public string Formula(double[] parameters, int precision = 3)
    {
        double y0 = Math.Round(parameters[0], precision);
        double a = Math.Round(parameters[1], precision);
        double x0 = Math.Round(parameters[2], precision);
        double t = Math.Round(parameters[3], precision);
        return $"Y = {y0} + {a} * Exp(-(x-{x0})/{t})";
    }

    public int ParameterCount => 4;
    public (double min, double max) TypicalXRange => (1, 10);
    public Func<double, double[], double> Function { get; } = (double x, double[] parameters) =>
    {
        double y0 = parameters[0];
        double a = parameters[1];
        double x0 = parameters[0];
        double t = parameters[1];
        return y0 + a + Math.Exp(-(x - x0) / t);
    };

    public ParameterLimits[] TypicalParameterLimits => [
        new ParameterLimits(0, 10), // y0
        new ParameterLimits(1, 10), // a
        new ParameterLimits(1, 10), // x0
        new ParameterLimits(.1, 5), // t
    ];
}
