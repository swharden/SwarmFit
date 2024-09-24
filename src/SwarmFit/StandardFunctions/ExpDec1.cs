namespace SwarmFit.StandardFunctions;

// https://www.originlab.com/doc/en/Origin-Help/ExpDec1-FitFunc
public class ExpDec1 : IFunction
{
    public string Name => "One-phase exponential decay function with time constant parameter";
    public string Formula() => "Y = y0 + a * Exp(-x/t)";
    public string Formula(double[] parameters, int precision = 3)
    {
        double y0 = Math.Round(parameters[0], precision);
        double a = Math.Round(parameters[1], precision);
        double t = Math.Round(parameters[2], precision);
        return $"Y = {y0} + {a} * Exp(-x/{t})";
    }

    public int ParameterCount => 3;
    public (double min, double max) TypicalXRange => (1, 10);
    public Func<double, double[], double> Function { get; } = (double x, double[] parameters) =>
    {
        double y0 = parameters[0];
        double a = parameters[1];
        double t = parameters[2];
        return y0 + a + Math.Exp(-x / t);
    };

    public ParameterLimits[] TypicalParameterLimits => [
        new ParameterLimits(0, 10), // y0
        new ParameterLimits(1, 10), // a
        new ParameterLimits(.1, 5), // t
    ];
}
