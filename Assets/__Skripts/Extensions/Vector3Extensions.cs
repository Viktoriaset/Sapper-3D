using System;
using UnityEngine;

namespace ExtensionMethods
{
    public static class Vector3Extensionss
    {
        public static float GetVectorValueByChar(this Vector3 v, char c)
        {
            c = char.ToLower(c);
            if (c == 'x')
                return v.x;
            if (c == 'y')
                return v.y;
            if (c == 'z')
                return v.z;

            throw new ArgumentException($"Char {c} is not valid!");
        }
    }
}

