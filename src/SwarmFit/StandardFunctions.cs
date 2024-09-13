namespace SwarmFit;

public static class StandardFunctions
{
    public static double Exponential2P(double x, double[] vars)
    {
        if (vars.Length != 2) throw new ArgumentException($"{nameof(vars)} length must equal 2");
        return vars[0] * Math.Exp(vars[1] * x); // A*e^(B*x)
    }

    public static double Exponential3P(double x, double[] vars)
    {
        if (vars.Length != 3) throw new ArgumentException($"{nameof(vars)} length must equal 3");
        return vars[0] * Math.Exp(vars[1] * x) + vars[2]; // A*e^(B*x) + C
    }
}
