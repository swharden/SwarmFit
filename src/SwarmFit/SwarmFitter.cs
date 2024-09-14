/* Adapted from "Particle Swarm Optimization Using C#" by James McCaffrey,
 * published in Visual Studio Magazine on 11/25/2013
 * https://visualstudiomagazine.com/Articles/2013/11/01/Particle-Swarm-Optimization.aspx
 */

using System.Diagnostics;

namespace SwarmFit;

public class SwarmFitter
{
    public double[] Xs { get; }
    public double[] Ys { get; }
    public Func<double, double[], double> Function { get; }
    VariableLimits[] VarLimits { get; }

    public Random Rand = new(0); // TODO: make this settable
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

    double GetError(double[] vars)
    {
        double error = 0;

        for (int i = 0; i < Xs.Length; i++)
        {
            double predictedY = GetY(Xs[i], vars);
            double actualY = Ys[i];
            double diff = Math.Abs(predictedY - actualY);
            error += SquareError ? diff * diff : diff;
        }

        return error;
    }

    public double GetY(double x, double[] vars) => Function(x, vars);
    public double[] GetYs(double[] xs, double[] vars) => xs.Select(x => GetY(x, vars)).ToArray();

    public FitSolution Solve(int iterations = 1000)
    {
        Stopwatch sw = Stopwatch.StartNew();

        double[] bestGlobalPositions = new double[VariableCount];
        double bestGlobalError = double.MaxValue;

        Particle[] particles = new Particle[NumParticles];
        List<FitSolution> intermediateSolutions = [];

        for (int i = 0; i < particles.Length; i++)
        {
            double[] randomPositions = VarLimits.Select(x => x.Random(Rand)).ToArray();
            double error = GetError(randomPositions);
            double[] randomVelocities = VarLimits.Select(x => x.Random(Rand) * VelocityRandomness).ToArray();
            particles[i] = new Particle(randomPositions, error, randomVelocities, randomPositions, error);

            if (particles[i].Error < bestGlobalError)
            {
                bestGlobalError = particles[i].Error;
                particles[i].Positions.CopyTo(bestGlobalPositions, 0);
            }
        }

        if (StoreIntermediateSolutions)
        {
            intermediateSolutions.Add(new(bestGlobalPositions, bestGlobalError, sw.Elapsed, 0, particles.Length));
        }

        for (int iteration = 1; iteration <= iterations; iteration++)
        {
            double[] newVelocity = new double[VariableCount];
            double[] newPosition = new double[VariableCount];

            foreach (Particle particle in particles)
            {
                for (int j = 0; j < particle.Velocities.Length; j++)
                {
                    double inertia = InertiaWeight * particle.Velocities[j];
                    double local = LocalWeight * Rand.NextDouble() * (particle.BestPositions[j] - particle.Positions[j]);
                    double global = GlobalWeight * Rand.NextDouble() * (bestGlobalPositions[j] - particle.Positions[j]);
                    newVelocity[j] = inertia + local + global;
                }

                newVelocity.CopyTo(particle.Velocities, 0);

                for (int j = 0; j < particle.Positions.Length; j++)
                {
                    newPosition[j] = particle.Positions[j] + newVelocity[j];
                    newPosition[j] = VarLimits[j].Clamp(newPosition[j]);
                }
                newPosition.CopyTo(particle.Positions, 0);

                double newError = GetError(newPosition);
                particle.Error = newError;

                if (newError < particle.BestError)
                {
                    newPosition.CopyTo(particle.BestPositions, 0);
                    particle.BestError = newError;
                }

                if (newError < bestGlobalError)
                {
                    newPosition.CopyTo(bestGlobalPositions, 0);
                    bestGlobalError = newError;
                    intermediateSolutions.Add(new(bestGlobalPositions, bestGlobalError, sw.Elapsed, iteration, particles.Length));
                }

                if (Rand.NextDouble() < probDeath)
                {
                    particle.RandomizePositions(Rand, VarLimits);
                    particle.Error = GetError(particle.Positions);
                    particle.BestError = particle.Error;

                    if (particle.Error < bestGlobalError)
                    {
                        bestGlobalError = particle.Error;
                        particle.Positions.CopyTo(bestGlobalPositions, 0);
                        intermediateSolutions.Add(new(bestGlobalPositions, bestGlobalError, sw.Elapsed, iteration, particles.Length));
                    }
                }
            }
        }

        return new FitSolution(bestGlobalPositions, bestGlobalError, sw.Elapsed, iterations, particles.Length, intermediateSolutions.Any() ? [.. intermediateSolutions] : null);
    }
}