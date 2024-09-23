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
    VariableLimits[] VarLimits { get; }

    public Random Rand = new(); // TODO: make this settable
    public double VelocityRandomness = 0.1;
    public double InertiaWeight = 0.729;
    public double LocalWeight = 1.49445;
    public double GlobalWeight = 1.49445;
    public double probDeath = 0.01;
    public int NumParticles = 5;
    public bool SquareError = false;
    public int VariableCount => VarLimits.Length;
    public bool StoreIntermediateSolutions = false;

    public SwarmFitter(double[] xs, double[] ys, Func<double, double[], double> func, VariableLimits[] limits)
    {
        if (xs.Length != ys.Length)
            throw new ArgumentException($"{nameof(xs)} and {nameof(ys)} must have equal length");

        Xs = xs;
        Ys = ys;
        Function = func;
        VarLimits = limits;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    double GetError(double[] vars)
    {
        double error = 0;
        double[] xs = Xs;
        double[] ys = Ys;
        bool squareError = SquareError;

        for (int i = 0; i < xs.Length; i++)
        {
            double predictedY = GetY(xs[i], vars);
            double actualY = ys[i];
            double diff = Math.Abs(predictedY - actualY);
            error += squareError ? diff * diff : diff;
        }

        return error;
    }

    public double GetY(double x, double[] vars) => Function(x, vars);
    public double[] GetYs(double[] xs, double[] vars) => xs.Select(x => GetY(x, vars)).ToArray();

    public FitSolution Solve(int iterations = 1000)
    {
        Stopwatch sw = Stopwatch.StartNew();
        Random rand = Rand;

        double[] bestGlobalPositions = new double[VariableCount];
        double bestGlobalError = double.MaxValue;

        Span<Particle> particles = new Particle[NumParticles];
        List<FitSolution>? intermediateSolutions = StoreIntermediateSolutions ? [] : null;

        for (int i = 0; i < particles.Length; i++)
        {
            double[] randomPositions = VarLimits.Select(x => x.Random(rand)).ToArray();
            double error = GetError(randomPositions);
            double[] randomVelocities = VarLimits.Select(x => x.Random(rand) * VelocityRandomness).ToArray();
            particles[i] = new Particle(randomPositions, error, randomVelocities, randomPositions, error);

            if (particles[i].Error < bestGlobalError)
            {
                bestGlobalError = particles[i].Error;
                particles[i].Positions.AsSpan().CopyTo(bestGlobalPositions);
            }
        }

        intermediateSolutions?.Add(new(bestGlobalPositions, bestGlobalError, sw.Elapsed, 0, particles.Length));

        double[] newVelocity = new double[VariableCount];
        double[] newPosition = new double[VariableCount];

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
                    double local = LocalWeight * rand.NextDouble() * (bestPositions[j] - positions[j]);
                    double global = GlobalWeight * rand.NextDouble() * (bestGlobalPositions[j] - positions[j]);
                    newVelocity[j] = inertia + local + global;
                }

                newVelocity.AsSpan().CopyTo(velocities);

                for (int j = 0; j < positions.Length; j++)
                {
                    newPosition[j] = positions[j] + newVelocity[j];
                    newPosition[j] = VarLimits[j].Clamp(newPosition[j]);
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

                if (rand.NextDouble() < probDeath)
                {
                    particle.RandomizePositions(rand, VarLimits);
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