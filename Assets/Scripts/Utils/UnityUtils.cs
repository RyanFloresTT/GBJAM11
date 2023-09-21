using System.Collections.Generic;
using System;
using UnityEngine;
public static class UnityUtils {
    private static System.Random random = new();
    // Turns a Vector3 into a Vector 2, excluding the z coordinate.
    public static Vector2 ToVector2(this Vector3 vector) {
        return new Vector2(vector.x, vector.y);
    }

    // Turns a Vector2 into a Vector3, using 0 as the z coordinate.
    public static Vector3 ToVector3(this Vector2 vector) {
        return new Vector3(vector.x, vector.y, 0);
    }

    public static T GetRandomItem<T>(this List<T> list) {
        if (list == null || list.Count == 0) {
            throw new InvalidOperationException("The list is null or empty.");
        }
        int randomIndex = random.Next(list.Count);
        return list[randomIndex];
    }
}
