using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BroWar.Common.Patterns
{
    using BroWar.Common.Utilities;

    public abstract class ScriptableObjectsHolder<T> : ScriptableObject where T : ScriptableObject
    {
        [SerializeField]
        private List<T> targets = new List<T>();


        protected virtual void Reset()
        {
            CollectAll();
        }


        public virtual void CollectAll()
        {
            CollectAll(null);
        }

        public virtual void CollectAll(Func<T, bool> predicate)
        {
            targets = AssetUtility.CollectAllAssets<T>(predicate);
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }


        public List<T> Targets
        {
            get => targets;
        }
    }
}