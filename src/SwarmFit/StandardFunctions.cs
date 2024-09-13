namespace SwarmFit;

public static class StandardFunctions
{
    public static double Exponential2P(double x, double[] vars)
    {
        return vars[0] * Math.Exp(vars[1] * x); // A*e^(B*x)
    }
}
