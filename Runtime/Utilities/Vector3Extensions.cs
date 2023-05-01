using UnityEngine;

namespace BroWar.Common.Utilities
{
    public static class Vector3Extensions
    {
        public static float FlatDistanceTo(this Vector3 start, Vector3 end)
        {
            Vector2 a = new Vector2(start.x, start.z);
            Vector2 b = new Vector2(end.x, end.z);
            return Vector2.Distance(a, b);
        }

        public static bool Approximately(this Vector3 a, Vector3 b)
        {
            return Mathf.Approximately(a.x, b.x) &&
                   Mathf.Approximately(a.y, b.y) &&
                   Mathf.Approximately(a.z, b.z);
        }
    }
}