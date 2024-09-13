namespace SwarmFit;

public class Particle
{
    public double[] position;
    public double error;
    public double[] velocity;
    public double[] bestPosition;
    public double bestError;

    public Particle(double[] pos, double err, double[] vel, double[] bestPos, double bestErr)
    {
        position = new double[pos.Length];
        pos.CopyTo(this.position, 0);
        error = err;
        velocity = new double[vel.Length];
        vel.CopyTo(this.velocity, 0);
        bestPosition = new double[bestPos.Length];
        bestPos.CopyTo(this.bestPosition, 0);
        bestError = bestErr;
    }
}
