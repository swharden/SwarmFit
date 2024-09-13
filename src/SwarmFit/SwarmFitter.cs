/* Adapted from "Particle Swarm Optimization Using C#" by James McCaffrey,
 * published in Visual Studio Magazine on 11/25/2013
 * https://visualstudiomagazine.com/Articles/2013/11/01/Particle-Swarm-Optimization.aspx
 */

namespace SwarmFit;

public class SwarmFitter
{
    double[] DataXs { get; }
    double[] DataYs { get; }
    public Func<double, double[], double> Function { get; }
    VariableLimits[] VarLimits { get; }

    Random Rand = new(0); // TODO: make this settable
    double VelocityRandomness = 0.1;
    double InertiaWeight = 0.729;
    double LocalWeight = 1.49445;
    double GlobalWeight = 1.49445;
    double probDeath = 0.01;
    int NumParticles = 5;
    int MaxEpochs = 1000;

    public SwarmFitter(double[] xs, double[] ys, Func<double, double[], double> func, VariableLimits[] limits)
    {
        if (xs.Length != ys.Length)
            throw new ArgumentException($"{nameof(xs)} and {nameof(ys)} must have equal length");

        try
        {
            double[] testVars = limits.Select(x => x.Mid).ToArray();
            func.Invoke(xs[0], testVars);
        }
        catch (IndexOutOfRangeException)
        {
            throw new ArgumentException("Function variable length must equal length of variable limits");
        }

        DataXs = xs;
        DataYs = ys;
        Function = func;
        VarLimits = limits;
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

    public double GetY(double x, double[] vars) => Function(x, vars);
    public double[] GetYs(double[] xs, double[] vars) => xs.Select(x => GetY(x, vars)).ToArray();

    public double[] Solve()
    {
        if (Function is null)
            throw new NullReferenceException(nameof(Function));

        int variableCount = VarLimits.Length;

        Particle[] swarm = new Particle[NumParticles];
        double[] bestGlobalPosition = new double[variableCount];
        double bestGlobalError = double.MaxValue;

        for (int i = 0; i < swarm.Length; ++i)
        {
            double[] randomPosition = new double[variableCount];
            for (int j = 0; j < randomPosition.Length; ++j)
            {
                randomPosition[j] = VarLimits[j].Random(Rand);
            }

            double error = Error(randomPosition);
            double[] randomVelocity = new double[variableCount];

            for (int j = 0; j < randomVelocity.Length; ++j)
            {
                randomVelocity[j] = VarLimits[j].Random(Rand) * VelocityRandomness;
            }
            swarm[i] = new Particle(randomPosition, error, randomVelocity, randomPosition, error);

            if (swarm[i].error < bestGlobalError)
            {
                bestGlobalError = swarm[i].error;
                swarm[i].position.CopyTo(bestGlobalPosition, 0);
            }
        }

        int epoch = 0;

        double[] newVelocity = new double[variableCount];
        double[] newPosition = new double[variableCount];
        double newError;

        while (epoch < MaxEpochs)
        {
            for (int i = 0; i < swarm.Length; ++i)
            {
                Particle currP = swarm[i];

                for (int j = 0; j < currP.velocity.Length; ++j)
                {
                    double inertia = InertiaWeight * currP.velocity[j];
                    double local = LocalWeight * Rand.NextDouble() * (currP.bestPosition[j] - currP.position[j]);
                    double global = GlobalWeight * Rand.NextDouble() * (bestGlobalPosition[j] - currP.position[j]);
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

                double die = Rand.NextDouble();
                if (die < probDeath)
                {
                    for (int j = 0; j < currP.position.Length; ++j)
                    {
                        currP.position[j] = VarLimits[j].Random(Rand);
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

        double[] result = new double[variableCount];
        bestGlobalPosition.CopyTo(result, 0);
        return result;
    }
}