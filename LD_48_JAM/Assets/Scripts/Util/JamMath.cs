using UnityEngine;

public static class JamMath
{
    static float sqrt2 = Mathf.Sqrt(2);
    static float sqrt6 = Mathf.Sqrt(6);

    public static Vector2 From3DToIsometric2D(Vector3 vector)
    {
        //x' = (x-z)/ sqrt(2)

        //y' = (x + 2y + z) / sqrt(6)

        return new Vector2((vector.x - vector.z) / sqrt2, (vector.x + (2 * vector.y) + vector.z) / sqrt6);
    }

}
