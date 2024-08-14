namespace SwarmFit;

public static class SampleData
{
    public static double[] Range(double min, double max, double delta)
    {
        int count = (int)((max - min) / delta) + 1;
        double[] result = new double[count];

        for (int i = 0; i < count; i++)
        {
            result[i] = min + i * delta;
        }

        return result;
    }

    public static double[] Exponential(double[] xs, double a, double b, double c)
    {
        double[] ys = new double[xs.Length];

        for (int i = 0; i < xs.Length; i++)
        {
            ys[i] = a + b * Math.Exp(xs[i] * c);
        }

        return ys;
    }
}
