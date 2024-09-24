namespace SwarmFit.StandardFunctions;

public class Linear : IFunction
{
    public string Name => "Linear";
    public string Formula() => "Y=m*X+b";
    public string Formula(double[] p, int precision = 3)
    {
        double m = Math.Round(p[0], precision);
        double b = Math.Round(p[1], precision);
        return $"Y={m}*X+{b}";
    }

    public int ParameterCount => 2;
    public (double min, double max) TypicalXRange => (-10, 10);
    public Func<double, double[], double> Function { get; } = (double x, double[] parameters) =>
    {
        double m = parameters[0];
        double b = parameters[1];
        return m * x *  + b;
    };
    public ParameterLimits[] TypicalParameterLimits => [
        new(-10, 10), // m
        new(-10, 10), // b
    ];
}
