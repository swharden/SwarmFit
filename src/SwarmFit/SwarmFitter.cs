using System.Diagnostics;

namespace SwarmFit;

public class SwarmFitter
{
    // Adapted from "Particle Swarm Optimization Using C#" by James McCaffrey,
    // published in Visual Studio Magazine on 11/25/2013
    // https://visualstudiomagazine.com/Articles/2013/11/01/Particle-Swarm-Optimization.aspx

    public readonly double[] Xs;
    public readonly double[] Ys;
    public readonly int Dimensions;
    public readonly string[] VariableName;
    public readonly double[] VariableMin;
    public readonly double[] VariableMax;
    public readonly double[] Solution;
    public TimeSpan Elapsed { get; private set; } = TimeSpan.Zero;

    public SwarmFitter(double[] xs, double[] ys, int dimensions)
    {
        ArgumentNullException.ThrowIfNull(xs, nameof(xs));
        ArgumentNullException.ThrowIfNull(ys, nameof(ys));
        if (xs.Length != ys.Length)
            throw new ArgumentException($"{nameof(xs)} and {nameof(ys)} must have equal length");

        Xs = xs;
        Ys = ys;

        Dimensions = dimensions;
        VariableName = Enumerable.Range(0, dimensions).Select(x => $"var{x + 1}").ToArray();
        VariableMin = Enumerable.Range(0, dimensions).Select(x => -10.0).ToArray();
        VariableMax = Enumerable.Range(0, dimensions).Select(x => +10.0).ToArray();
        Solution = Enumerable.Range(0, dimensions).Select(x => double.NaN).ToArray();
    }

    double Error(double[] variables)
    {
        double totalError = 0;

        for (int i = 0; i < Xs.Length; i++)
        {
            totalError += Math.Abs(GetY(Xs[i], variables) - Ys[i]);
        }

        return totalError * totalError;
    }

    public double GetY(double x, double[] variables)
    {
        return variables[0] + variables[1] * Math.Exp(variables[2] * x);
    }

    public double GetY(double x)
    {
        return GetY(x, Solution);
    }

    public double[] GetYs(double[] xs)
    {
        double[] ys = new double[xs.Length];
        for (int i = 0; i < xs.Length; i++)
        {
            ys[i] = GetY(xs[i], Solution);
        }
        return ys;
    }

    public double[] GetXs(double xMin, double xMax, double xStep)
    {
        int count = (int)((xMax - xMin) / xStep) + 1;
        double[] xs = new double[count];
        for (int i = 0; i < count; i++)
        {
            xs[i] = xMin + xStep * i;
        }
        return xs;
    }

    public double[] GetYs(double xMin, double xMax, double xStep)
    {
        int count = (int)((xMax - xMin) / xStep) + 1;

        double[] ys = new double[count];
        for (int i = 0; i < count; i++)
        {
            ys[i] = GetY(xMin + xStep * i, Solution);
        }
        return ys;
    }

    public void SetRange(int variable, double min, double max)
    {
        VariableMin[variable] = min;
        VariableMax[variable] = max;
    }

    public void Fit(int iterations = 100, int particleCount = 5)
    {
        Stopwatch sw = Stopwatch.StartNew();
        Random rand = new(0);

        Particle[] swarm = new Particle[particleCount];
        double[] bestGlobalPosition = new double[Dimensions];
        double bestGlobalError = double.MaxValue;

        // swarm initialization
        for (int i = 0; i < swarm.Length; ++i)
        {
            double[] randomPosition = new double[Dimensions];
            for (int j = 0; j < randomPosition.Length; ++j)
            {
                double varMin = VariableMin[j];
                double varMax = VariableMax[j];
                randomPosition[j] = (varMax - varMin) * rand.NextDouble() + varMin;
            }

            double error = Error(randomPosition);
            double[] randomVelocity = new double[Dimensions];

            for (int j = 0; j < randomVelocity.Length; ++j)
            {
                double varMin = VariableMin[j];
                double varMax = VariableMax[j];
                double lo = varMin * 0.1;
                double hi = varMax * 0.1;
                randomVelocity[j] = (hi - lo) * rand.NextDouble() + lo;
            }
            swarm[i] = new Particle(randomPosition, error, randomVelocity, randomPosition, error);

            if (swarm[i].Error < bestGlobalError)
            {
                bestGlobalError = swarm[i].Error;
                swarm[i].Position.CopyTo(bestGlobalPosition, 0);
            }
        }

        // prepare
        double w = 0.729; // inertia weight
        double c1 = 1.49445; // cognitive/local weight
        double c2 = 1.49445; // social/global weight
        double deathProbabilityFraction = 0.01;

        double[] newVelocity = new double[Dimensions];
        double[] newPosition = new double[Dimensions];

        for (int epoch = 0; epoch < iterations; epoch++)
        {
            foreach (Particle particle in swarm)
            {
                // new velocity
                for (int j = 0; j < particle.Velocity.Length; ++j)
                {
                    newVelocity[j] = (w * particle.Velocity[j]) +
                      (c1 * rand.NextDouble() * (particle.BestPosition[j] - particle.Position[j])) +
                      (c2 * rand.NextDouble() * (bestGlobalPosition[j] - particle.Position[j]));
                }
                newVelocity.CopyTo(particle.Velocity, 0);

                // new position
                for (int j = 0; j < particle.Position.Length; ++j)
                {
                    newPosition[j] = particle.Position[j] + newVelocity[j];
                    if (newPosition[j] < VariableMin[j])
                    {
                        newPosition[j] = VariableMin[j];
                    }
                    else if (newPosition[j] > VariableMax[j])
                    {
                        newPosition[j] = VariableMax[j];
                    }
                }
                newPosition.CopyTo(particle.Position, 0);

                double newError = Error(newPosition);
                particle.Error = newError;

                if (newError < particle.BestError)
                {
                    newPosition.CopyTo(particle.BestPosition, 0);
                    particle.BestError = newError;
                }

                if (newError < bestGlobalError)
                {
                    newPosition.CopyTo(bestGlobalPosition, 0);
                    bestGlobalError = newError;
                }

                if (rand.NextDouble() < deathProbabilityFraction)
                {
                    for (int j = 0; j < particle.Position.Length; ++j)
                    {
                        double varMin = VariableMin[j];
                        double varMax = VariableMax[j];
                        particle.Position[j] = (varMax - varMin) * rand.NextDouble() + varMin;
                    }
                    particle.Error = Error(particle.Position);
                    particle.Position.CopyTo(particle.BestPosition, 0);
                    particle.BestError = particle.Error;

                    if (particle.Error < bestGlobalError)
                    {
                        bestGlobalError = particle.Error;
                        particle.Position.CopyTo(bestGlobalPosition, 0);
                    }
                }

            }
        }

        bestGlobalPosition.CopyTo(Solution, 0);
        Elapsed = sw.Elapsed;
    }

    public override string ToString()
    {
        List<string> parts = [];
        for (int i = 0; i < Solution.Length; i++)
        {
            parts.Add($"{VariableName[i]}={Solution[i]}");
        }

        return "Swarm fit: " + string.Join(", ", parts);
    }

    private class Particle
    {
        public double[] Position;
        public double Error;
        public double[] Velocity;
        public double[] BestPosition;
        public double BestError;

        public Particle(double[] pos, double err, double[] vel, double[] bestPos, double bestErr)
        {
            Position = new double[pos.Length];
            pos.CopyTo(Position, 0);
            Error = err;
            Velocity = new double[vel.Length];
            vel.CopyTo(Velocity, 0);
            BestPosition = new double[bestPos.Length];
            bestPos.CopyTo(BestPosition, 0);
            BestError = bestErr;
        }
    }
}