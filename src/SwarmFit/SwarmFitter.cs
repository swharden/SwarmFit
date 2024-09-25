/* Adapted from "Particle Swarm Optimization Using C#" by James McCaffrey,
 * published in Visual Studio Magazine on 11/25/2013 and other texts:
 * https://visualstudiomagazine.com/Articles/2013/11/01/Particle-Swarm-Optimization.aspx
 * https://ieeexplore.ieee.org/document/9376550
 * https://www.mathworks.com/matlabcentral/fileexchange/58895-ppso?s_tid=blogs_rc_5
 */

using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace SwarmFit;

public class SwarmFitter
{
    /// <summary>
    /// X values (provided by the user) that the parameters of <see cref="Function"/> will be optimized to fit.
    /// </summary>
    public double[] Xs { get; }

    /// <summary>
    /// Y values (provided by the user) that the parameters of <see cref="Function"/> will be optimized to fit
    /// </summary>
    public double[] Ys { get; }

    /// <summary>
    /// The function (provided by the user) that parameters will be optimized for to fit <see cref="Xs"/> and <see cref="Ys"/>
    /// </summary>
    public Func<double, double[], double> Function { get; }

    /// <summary>
    /// Bounds for each parameter used when randomizing particle positions.
    /// </summary>
    private ParameterLimits[] ParameterLimits { get; }

    /// <summary>
    /// Number of parameters to be optimized.
    /// This must be consistent with the size of <see cref="ParameterLimits"/> and the
    /// length of the parameter array <see cref="Function"/> consumes.
    /// </summary>
    public int ParameterCount => ParameterLimits.Length;

    /// <summary>
    /// Source of randomness used for randomizing particle placement and inertial angle
    /// </summary>
    public IRandomNumberGenerator Rand { get; set; } = new RandomNumberGenerators.XorShift();


    public double InertiaWeight { get; set; } = 0.729;
    public double LocalWeight { get; set; } = 1.49445;
    public double GlobalWeight { get; set; } = 1.49445;

    /// <summary>
    /// Fractional chance that a particle will be killed and randomized after an iteration
    /// </summary>
    public double ParticleDeathProbability = 0.01;

    /// <summary>
    /// Number of particles in the field
    /// </summary>
    public int ParticleCount => Particles.Length;

    private Particle[] Particles { get; }
    double[] BestParameters { get; }
    double BestError = double.MaxValue;

    readonly Stopwatch Stopwatch = new();

    /// <summary>
    /// Total time the solver was running
    /// </summary>
    public TimeSpan CalculationTime => Stopwatch.Elapsed;

    /// <summary>
    /// Total number of best fit curves identified including the present one
    /// </summary>
    public int ImprovementCount { get; private set; } = 0;

    /// <summary>
    /// Total number of times the entire particle field was iterated forward
    /// </summary>
    public int IterationCount { get; private set; } = 0;

    public FitSolution BestSolution => new(BestParameters, BestError, CalculationTime, IterationCount, ImprovementCount);

    public SwarmFitter(double[] xs, double[] ys, Func<double, double[], double> func, ParameterLimits[] limits, int numParticles = 5)
    {
        if (xs.Length != ys.Length)
            throw new ArgumentException($"{nameof(xs)} and {nameof(ys)} must have equal length");

        Xs = xs;
        Ys = ys;
        Function = func;
        ParameterLimits = limits;
        Particles = new Particle[numParticles];
        BestParameters = new double[ParameterCount];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    double GetError(double[] parameters)
    {
        double error = 0;
        double[] xs = Xs;
        double[] ys = Ys;

        for (int i = 0; i < xs.Length; i++)
        {
            double predictedY = Function(xs[i], parameters);
            double actualY = ys[i];
            double diff = Math.Abs(predictedY - actualY);
            error += diff;
        }

        return error;
    }

    /// <summary>
    /// Progress toward an ideal solution.
    /// This method may be called multiple times.
    /// </summary>
    /// <param name="maxIterations">Stop trying to improve the fit if this number of particle progressions has been reached</param>
    /// <param name="maxImprovements">Stop trying to improve the fit if this number of additional improvements has been made</param>
    /// <param name="maxImprovements">Stop trying to improve the fit if the total error no longer exceeds this value</param>
    public FitSolution Solve(int maxIterations = 1000, int maxImprovements = int.MaxValue, float errorThreshold = 0)
    {
        Stopwatch.Start();

        // Randomize all particles only if this is the first time running the solver.
        // This way the solve method may be called multiple times to improve the solution.
        if (IterationCount == 0)
        {
            RandomizeAll();
        }

        int improvementLimit = maxImprovements == int.MaxValue ? maxImprovements : ImprovementCount + maxImprovements;

        for (int i = 0; i < maxIterations; i++)
        {
            IterateAllParticles();
            if (ImprovementCount >= improvementLimit || BestError <= errorThreshold)
                break;
        }

        Stopwatch.Stop();

        return BestSolution;
    }

    private void RandomizeAll()
    {
        double initialVelocityRandomness = 0.01;
        for (int i = 0; i < Particles.Length; i++)
        {
            double[] randomPositions = ParameterLimits.Select(x => x.Random(Rand)).ToArray();
            double error = GetError(randomPositions);
            double[] randomVelocities = ParameterLimits.Select(x => x.Random(Rand) * x.Span * initialVelocityRandomness).ToArray();
            Particles[i] = new Particle(randomPositions, error, randomVelocities, randomPositions, error);

            if (Particles[i].Error < BestError)
            {
                BestError = Particles[i].Error;
                Particles[i].Positions.AsSpan().CopyTo(BestParameters);
            }
        }
    }

    private void IterateAllParticles()
    {
        double originalBestError = BestError;

        foreach (Particle particle in Particles)
        {
            IterateParticle(particle);
        }

        IterationCount++;
        if (BestError != originalBestError)
        {
            ImprovementCount++;
        }
    }

    private void IterateParticle(Particle particle)
    {
        double[] positions = particle.Positions;
        double[] bestPositions = particle.BestPositions;
        double[] velocities = particle.Velocities;

        for (int j = 0; j < velocities.Length; j++)
        {
            double inertia = InertiaWeight * velocities[j];
            double local = LocalWeight * Rand.NextDouble() * (bestPositions[j] - positions[j]);
            double global = GlobalWeight * Rand.NextDouble() * (BestParameters[j] - positions[j]);
            velocities[j] = inertia + local + global;
        }

        for (int j = 0; j < positions.Length; j++)
        {
            positions[j] = positions[j] + velocities[j];
            positions[j] = ParameterLimits[j].Clamp(positions[j]);
        }

        particle.Error = GetError(positions);

        if (particle.Error < particle.BestError)
        {
            positions.AsSpan().CopyTo(bestPositions);
            particle.BestError = particle.Error;
        }

        if (particle.Error < BestError)
        {
            positions.AsSpan().CopyTo(BestParameters);
            BestError = particle.BestError;
        }

        bool isBestParticle = particle.BestError == BestError;

        if (!isBestParticle && (Rand.NextDouble() < ParticleDeathProbability))
        {
            particle.RandomizePositions(Rand, ParameterLimits);
            particle.Error = GetError(particle.Positions);
            particle.BestError = particle.Error;

            if (particle.Error < BestError)
            {
                BestError = particle.Error;
                positions.AsSpan().CopyTo(BestParameters);
            }
        }
    }
}