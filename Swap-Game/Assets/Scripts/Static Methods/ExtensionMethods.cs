using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    /// <summary>
    /// Convert a Vector3 to a Vector2
    /// </summary>
    public static Vector2 ConvertToVector2(this Vector3 vector3)
    {
        float x = vector3.x;
        float y = vector3.y;

        Vector2 finalVector2 = new Vector2(x, y);

        return finalVector2;
    }
}
