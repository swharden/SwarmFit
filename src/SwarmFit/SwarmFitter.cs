/* Adapted from "Particle Swarm Optimization Using C#" by James McCaffrey,
 * published in Visual Studio Magazine on 11/25/2013
 * https://visualstudiomagazine.com/Articles/2013/11/01/Particle-Swarm-Optimization.aspx
 */

namespace SwarmFit;

public class SwarmFitter
{
    double[] DataXs { get; }
    double[] DataYs { get; }
    VariableLimits[] VarLimits = [
        new(-500, 500),
        new(-1, 1),
    ];

    public SwarmFitter(double[] xs, double[] ys)
    {
        if (xs.Length != ys.Length)
            throw new ArgumentException($"{nameof(xs)} and {nameof(ys)} must have equal length");

        DataXs = xs;
        DataYs = ys;
    }

    double Error(double[] vars)
    {
        double error = 0;

        for (int i = 0; i < DataXs.Length; i++)
        {
            double predictedY = GetY(DataXs[i], vars);
            double actualY = DataYs[i];
            double diff = Math.Abs(predictedY - actualY);
            error += diff; // TODO: square this?
        }

        return error;
    }

    public double GetY(double x, double[] vars)
    {
        return vars[0] * Math.Exp(vars[1] * x); // A*e^(B*x)
    }

    public double[] Solve()
    {
        int dim = 2;
        int numParticles = 5;
        int maxEpochs = 1000; ;
        return Solve(dim, numParticles, maxEpochs);
    }

    double VelocityRandomness = 0.1;
    double InertiaWeight = 0.729;
    double LocalWeight = 1.49445;
    double GlobalWeight = 1.49445;
    double probDeath = 0.01;

    public double[] Solve(int dim, int numParticles, int maxEpochs)
    {
        Random rnd = new(0); // TODO: make this settable

        Particle[] swarm = new Particle[numParticles];
        double[] bestGlobalPosition = new double[dim];
        double bestGlobalError = double.MaxValue;

        for (int i = 0; i < swarm.Length; ++i)
        {
            double[] randomPosition = new double[dim];
            for (int j = 0; j < randomPosition.Length; ++j)
            {
                randomPosition[j] = VarLimits[j].Random(rnd);
            }

            double error = Error(randomPosition);
            double[] randomVelocity = new double[dim];

            for (int j = 0; j < randomVelocity.Length; ++j)
            {
                randomVelocity[j] = VarLimits[j].Random(rnd) * VelocityRandomness;
            }
            swarm[i] = new Particle(randomPosition, error, randomVelocity, randomPosition, error);

            if (swarm[i].error < bestGlobalError)
            {
                bestGlobalError = swarm[i].error;
                swarm[i].position.CopyTo(bestGlobalPosition, 0);
            }
        }


        int epoch = 0;

        double[] newVelocity = new double[dim];
        double[] newPosition = new double[dim];
        double newError;

        // main loop
        while (epoch < maxEpochs)
        {
            for (int i = 0; i < swarm.Length; ++i)
            {
                Particle currP = swarm[i];

                for (int j = 0; j < currP.velocity.Length; ++j)
                {
                    double inertia = InertiaWeight * currP.velocity[j];
                    double local = LocalWeight * rnd.NextDouble() * (currP.bestPosition[j] - currP.position[j]);
                    double global = GlobalWeight * rnd.NextDouble() * (bestGlobalPosition[j] - currP.position[j]);
                    newVelocity[j] = inertia + local + global;
                }

                newVelocity.CopyTo(currP.velocity, 0);

                for (int j = 0; j < currP.position.Length; ++j)
                {
                    newPosition[j] = currP.position[j] + newVelocity[j];
                    newPosition[j] = VarLimits[j].Clamp(newPosition[j]);
                }
                newPosition.CopyTo(currP.position, 0);

                newError = Error(newPosition);
                currP.error = newError;

                if (newError < currP.bestError)
                {
                    newPosition.CopyTo(currP.bestPosition, 0);
                    currP.bestError = newError;
                }

                if (newError < bestGlobalError)
                {
                    newPosition.CopyTo(bestGlobalPosition, 0);
                    bestGlobalError = newError;
                }

                double die = rnd.NextDouble();
                if (die < probDeath)
                {
                    for (int j = 0; j < currP.position.Length; ++j)
                    {
                        currP.position[j] = VarLimits[j].Random(rnd);
                    }
                    currP.error = Error(currP.position);
                    currP.position.CopyTo(currP.bestPosition, 0);
                    currP.bestError = currP.error;

                    if (currP.error < bestGlobalError)
                    {
                        bestGlobalError = currP.error;
                        currP.position.CopyTo(bestGlobalPosition, 0);
                    }
                }

            }
            ++epoch;
        }

        double[] result = new double[dim];
        bestGlobalPosition.CopyTo(result, 0);
        return result;
    }
}