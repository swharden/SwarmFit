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
    public int NumParticles = 5;
    public int ParameterCount => ParLimits.Length;
    public bool StoreIntermediateSolutions = false;

    public SwarmFitter(double[] xs, double[] ys, Func<double, double[], double> func, ParameterLimits[] limits)
    {
        if (xs.Length != ys.Length)
            throw new ArgumentException($"{nameof(xs)} and {nameof(ys)} must have equal length");

        Xs = xs;
        Ys = ys;
        Function = func;
        ParLimits = limits;
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

    public FitSolution Solve(int iterations = 1000)
    {
        Stopwatch sw = Stopwatch.StartNew();

        double[] bestGlobalPositions = new double[ParameterCount];
        double bestGlobalError = double.MaxValue;

        Span<Particle> particles = new Particle[NumParticles];
        List<FitSolution>? intermediateSolutions = StoreIntermediateSolutions ? [] : null;

        for (int i = 0; i < particles.Length; i++)
        {
            double[] randomPositions = ParLimits.Select(x => x.Random(Rand)).ToArray();
            double error = GetError(randomPositions);
            double[] randomVelocities = ParLimits.Select(x => x.Random(Rand) * VelocityRandomness).ToArray();
            particles[i] = new Particle(randomPositions, error, randomVelocities, randomPositions, error);

            if (particles[i].Error < bestGlobalError)
            {
                bestGlobalError = particles[i].Error;
                particles[i].Positions.AsSpan().CopyTo(bestGlobalPositions);
            }
        }

        intermediateSolutions?.Add(new(bestGlobalPositions, bestGlobalError, sw.Elapsed, 0, particles.Length));

        double[] newVelocity = new double[ParameterCount];
        double[] newPosition = new double[ParameterCount];

        for (int iteration = 1; iteration <= iterations; iteration++)
        {
            foreach (ref Particle particle in particles)
            {
                double[] positions = particle.Positions;
                double[] bestPositions = particle.BestPositions;
                double[] velocities = particle.Velocities;

                for (int j = 0; j < velocities.Length; j++)
                {
                    double inertia = InertiaWeight * velocities[j];
                    double local = LocalWeight * Rand.NextDouble() * (bestPositions[j] - positions[j]);
                    double global = GlobalWeight * Rand.NextDouble() * (bestGlobalPositions[j] - positions[j]);
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

                if (newError < bestGlobalError)
                {
                    newPosition.AsSpan().CopyTo(bestGlobalPositions);
                    bestGlobalError = newError;
                    intermediateSolutions?.Add(new(bestGlobalPositions, bestGlobalError, sw.Elapsed, iteration, particles.Length));
                }

                // TODO: never kill the best performing particle. Maybe only kill the worst particle?
                if (Rand.NextDouble() < probDeath)
                {
                    particle.RandomizePositions(Rand, ParLimits);
                    particle.Error = GetError(particle.Positions);
                    particle.BestError = particle.Error;

                    if (particle.Error < bestGlobalError)
                    {
                        bestGlobalError = particle.Error;
                        positions.AsSpan().CopyTo(bestGlobalPositions);
                        intermediateSolutions?.Add(new(bestGlobalPositions, bestGlobalError, sw.Elapsed, iteration, particles.Length));
                    }
                }
            }
        }

        return new FitSolution(bestGlobalPositions, bestGlobalError, sw.Elapsed, iterations, particles.Length, intermediateSolutions?.ToArray());
    }
}