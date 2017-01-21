using System;

public static class RNG
{
    static Random s_Random = new Random();

    public static float RandomFloat()
    {
        return (float)s_Random.NextDouble();
    }
}
