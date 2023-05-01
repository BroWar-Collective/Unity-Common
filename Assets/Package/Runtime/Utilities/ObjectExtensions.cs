using UnityEngine;

namespace BroWar.Common.Utilities
{
    public static class ObjectExtensions
    {
        public static void SafeDestroy(this Object obj)
        {
#if UNITY_EDITOR
            if (Application.isPlaying)
            {
                Object.Destroy(obj);
            }
            else
            {
                Object.DestroyImmediate(obj);
            }
#else
            Object.Destroy(obj);
#endif
        }
    }
}