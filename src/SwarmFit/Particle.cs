namespace SwarmFit;

public class Particle
{
    public double[] Positions;
    public double[] BestPositions;
    public double[] Velocities;

    public double Error;
    public double BestError;

    public Particle(double[] pos, double err, double[] vel, double[] bestPos, double bestErr)
    {
        Positions = new double[pos.Length];
        pos.CopyTo(Positions, 0);
        Error = err;
        Velocities = new double[vel.Length];
        vel.CopyTo(Velocities, 0);
        BestPositions = new double[bestPos.Length];
        bestPos.CopyTo(BestPositions, 0);
        BestError = bestErr;
    }

    public void RandomizePositions(Random rand, VariableLimits[] limits)
    {
        for (int i = 0; i < Positions.Length; i++)
        {
            Positions[i] = limits[i].Random(rand);
        }
        Positions.CopyTo(BestPositions, 0);
    }
}
