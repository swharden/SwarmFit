namespace SwarmFit;

/// <summary>
/// Classes implement this to pair a function with information that describes it
/// </summary>
public interface IFunction
{
    /// <summary>
    /// Common name for the formula
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Text description of the formula (e.g., Y = m * X + b)
    /// </summary>
    string Formula();

    /// <summary>
    /// Text description of the formula using the given parameters
    /// </summary>
    string Formula(double[] parameters, int precision = 3);

    /// <summary>
    /// Number of parameters to use when calling <see cref="Function"/>
    /// </summary>
    int ParameterCount { get; }

    /// <summary>
    /// Typical parameter ranges that may be used to evaluate the shape of the curve
    /// </summary>
    public ParameterLimits[] TypicalParameterLimits { get; }

    /// <summary>
    /// Typical horizontal range used to evaluate the shape of the curve
    /// </summary>
    public (double min, double max) TypicalXRange { get; }

    /// <summary>
    /// The function that returns a Y given an X and a collection of parameters (with a length equal to <see cref="ParameterCount"/>)
    /// </summary>
    Func<double, double[], double> Function { get; }
}
