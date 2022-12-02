using UnityEngine;

namespace BroWar.Common.Utilities
{
    public static class ObjectExtensions
    {
        public static void SafeDestroy(this Object obj)
        {
#if UNITY_EDITOR
            if (Application.isEditor)
            {
                Object.DestroyImmediate(obj);
            }
            else
#endif
            {
                Object.Destroy(obj);
            }
        }
    }
}