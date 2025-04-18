using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XHierarchy
{
    public static class MathExtension
    {
        public static int RoundToInt(this float value)
        {
            return Mathf.RoundToInt(value);
        }

        public static float Max(this float value, float value2)
        {
            return Mathf.Max(value, value2);
        }

        public static float Min(this float value, float value2)
        {
            return Mathf.Min(value, value2);
        }
    }

}
