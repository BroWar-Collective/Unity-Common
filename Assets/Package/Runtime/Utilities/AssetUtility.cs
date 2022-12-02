using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Object = UnityEngine.Object;

namespace BroWar.Common.Utilities
{
    public static class AssetUtility
    {
        public static List<T> CollectAllAssets<T>()
            where T : ScriptableObject
        {
            return CollectAllAssets<T>(null);
        }

        public static List<T> CollectAllAssets<T>(Func<T, bool> predicate)
            where T : ScriptableObject
        {
            var assets = new List<T>();
#if UNITY_EDITOR
            var guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}");
            if (guids == null || guids.Length == 0)
            {
                return assets;
            }

            for (var i = 0; i < guids.Length; i++)
            {
                var path = AssetDatabase.GUIDToAssetPath(guids[i]);
                var asset = AssetDatabase.LoadAssetAtPath<T>(path);
                if (predicate != null && !predicate(asset))
                {
                    continue;
                }

                assets.Add(asset);
            }
#endif
            return assets;
        }

        public static void MakeDirty(Object target)
        {
#if UNITY_EDITOR
            if (Application.isPlaying)
            {
                return;
            }

            EditorUtility.SetDirty(target);
#endif
        }
    }
}