/* Adapted from "Particle Swarm Optimization Using C#" by James McCaffrey,
 * published in Visual Studio Magazine on 11/25/2013
 * https://visualstudiomagazine.com/Articles/2013/11/01/Particle-Swarm-Optimization.aspx
 */

using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace SwarmFit;

public class SwarmFitter
{
    public double[] Xs { get; }
    public double[] Ys { get; }
    public Func<double, double[], double> Function { get; }
    ParameterLimits[] ParLimits { get; }

    public IRandomNumberGenerator Rand { get; set; } = new RandomNumberGenerators.XorShift();
    public double VelocityRandomness = 0.1;
    public double InertiaWeight = 0.729;
    public double LocalWeight = 1.49445;
    public double GlobalWeight = 1.49445;
    public double probDeath = 0.01;
    public int NumParticles { get; }

    private Particle[] Particles { get; }
    double[] BestParameters { get; }
    double BestError = double.MaxValue;
    Stopwatch Stopwatch = new();
    TimeSpan CalculationTime => Stopwatch.Elapsed;
    int Improvements = 0;
    int Iterations = 0;
    FitSolution BestSolution => new(BestParameters, BestError, CalculationTime, Iterations, Improvements);

    public int ParameterCount => ParLimits.Length;

    public SwarmFitter(double[] xs, double[] ys, Func<double, double[], double> func, ParameterLimits[] limits, int numParticles = 5)
    {
        if (xs.Length != ys.Length)
            throw new ArgumentException($"{nameof(xs)} and {nameof(ys)} must have equal length");

        Xs = xs;
        Ys = ys;
        Function = func;
        ParLimits = limits;
        NumParticles = numParticles;
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
            double predictedY = GetY(xs[i], parameters);
            double actualY = ys[i];
            double diff = Math.Abs(predictedY - actualY);
            error += diff;
        }

        return error;
    }

    public double GetY(double x, double[] parameters) => Function(x, parameters);
    public double[] GetYs(double[] xs, double[] parameters) => xs.Select(x => GetY(x, parameters)).ToArray();

    /// <summary>
    /// Progress toward an ideal solution.
    /// This method may be called multiple times.
    /// </summary>
    /// <param name="maxIterations">Stop trying to improve the fit once this number of particle progressions has been reached</param>
    /// <param name="maxImprovements">Stop trying to improve the fit once parameter sets have been found this many times</param>
    public FitSolution Solve(int maxIterations = 1000, int maxImprovements = 100)
    {
        Stopwatch.Start();

        if (BestError == double.MaxValue)
        {
            // This is the first time running the solver so randomize everything and start from scratch.
            // Otherwise, the solver can be run multiple times to get progressively closer to a solution.
            for (int i = 0; i < Particles.Length; i++)
            {
                double[] randomPositions = ParLimits.Select(x => x.Random(Rand)).ToArray();
                double error = GetError(randomPositions);
                double[] randomVelocities = ParLimits.Select(x => x.Random(Rand) * VelocityRandomness).ToArray();
                Particles[i] = new Particle(randomPositions, error, randomVelocities, randomPositions, error);

                if (Particles[i].Error < BestError)
                {
                    BestError = Particles[i].Error;
                    Particles[i].Positions.AsSpan().CopyTo(BestParameters);
                }
            }
        }

        double[] newVelocity = new double[ParameterCount];
        double[] newPosition = new double[ParameterCount];

        int iterationLimit = maxIterations + Iterations;
        int improvementLimit = maxImprovements + Improvements;
        while (Iterations++ <= iterationLimit)
        {
            foreach (Particle particle in Particles)
            {
                double[] positions = particle.Positions;
                double[] bestPositions = particle.BestPositions;
                double[] velocities = particle.Velocities;

                for (int j = 0; j < velocities.Length; j++)
                {
                    double inertia = InertiaWeight * velocities[j];
                    double local = LocalWeight * Rand.NextDouble() * (bestPositions[j] - positions[j]);
                    double global = GlobalWeight * Rand.NextDouble() * (BestParameters[j] - positions[j]);
                    newVelocity[j] = inertia + local + global;
                }

                newVelocity.AsSpan().CopyTo(velocities);

                for (int j = 0; j < positions.Length; j++)
                {
                    newPosition[j] = positions[j] + newVelocity[j];
                    newPosition[j] = ParLimits[j].Clamp(newPosition[j]);
                }
                newPosition.AsSpan().CopyTo(positions);

                double newError = GetError(newPosition);
                particle.Error = newError;

                if (newError < particle.BestError)
                {
                    newPosition.AsSpan().CopyTo(bestPositions);
                    particle.BestError = newError;
                }

                if (newError < BestError)
                {
                    newPosition.AsSpan().CopyTo(BestParameters);
                    BestError = newError;
                    if (Improvements++ >= improvementLimit)
                    {
                        Stopwatch.Stop();
                        return BestSolution;
                    }
                }

                // TODO: never kill the best performing particle. Maybe only kill the worst particle?
                if (Rand.NextDouble() < probDeath)
                {
                    particle.RandomizePositions(Rand, ParLimits);
                    particle.Error = GetError(particle.Positions);
                    particle.BestError = particle.Error;

                    if (particle.Error < BestError)
                    {
                        BestError = particle.Error;
                        positions.AsSpan().CopyTo(BestParameters);
                        if (Improvements++ >= improvementLimit)
                        {
                            Stopwatch.Stop();
                            return BestSolution;
                        }
                    }
                }
            }
        }

        Stopwatch.Stop();
        return BestSolution;
    }
}
