using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    /// <summary>
    /// Convert a Vector2 to a Vector3 (For 2D: Z value will be zero)
    /// </summary>
    public static Vector3 ConvertToVector3(this Vector2 vector2)
    {
        float x = vector2.x;
        float y = vector2.y;

        Vector3 finalVector3 = new Vector3(x, y, 0f);

        return finalVector3;
    }
}
