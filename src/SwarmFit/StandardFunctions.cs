namespace SwarmFit;

public static class StandardFunctions
{
    /// <summary>
    /// A * e^(B*x)
    /// </summary>
    public static double Exponential2P(double x, double[] parameters)
    {
        if (parameters.Length != 2) throw new ArgumentException($"{nameof(parameters)} length must equal 2");
        return parameters[0] * Math.Exp(parameters[1] * x);
    }

    /// <summary>
    /// A * e^(B*x) + C
    /// </summary>
    public static double Exponential3P(double x, double[] parameters)
    {
        if (parameters.Length != 3) throw new ArgumentException($"{nameof(parameters)} length must equal 3");
        return parameters[0] * Math.Exp(parameters[1] * x) + parameters[2];
    }
}
