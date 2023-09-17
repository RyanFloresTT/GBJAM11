using UnityEngine;
public static class UnityUtils {
    // Turns a Vector3 into a Vector 2, excluding the z coordinate.
    public static Vector2 ToVector2(this Vector3 vector) {
        return new Vector2(vector.x, vector.y);
    }

    // Turns a Vector2 into a Vector3, using 0 as the z coordinate.
    public static Vector3 ToVector3(this Vector2 vector) {
        return new Vector3(vector.x, vector.y, 0);
    }
}