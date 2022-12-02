using System;
using System.Collections.Generic;
using UnityEngine;

namespace BroWar.Common.Factories
{
    using BroWar.Common.Patterns;

    /// <summary>
    /// Dedicated <see cref="IFactory{T}"/> for Unity native objects (mostly <see cref="MonoBehaviour"/>s).
    /// In background takes advantage of objects pooling and can be easily extended for custom types.
    /// </summary>
    public class NativeFactory<T> : IFactory<T> where T : Component
    {
        private readonly Dictionary<Type, NativeObjectPool<T>> poolsByTypes = new Dictionary<Type, NativeObjectPool<T>>();

        public NativeFactory(IReadOnlyList<T> prefabs, Transform parent = null, int initialPool = 0)
        {
            foreach (var prefab in prefabs)
            {
                CachePrefab(prefab, parent, initialPool);
            }
        }

        protected void CachePrefab(T prefab, Transform parent, int initialPool)
        {
            if (prefab == null)
            {
                Debug.LogError($"[Factories] Given prefab is invalid.");
                return;
            }

            var type = prefab.GetType();
            if (poolsByTypes.TryGetValue(type, out _))
            {
                Debug.LogError($"[Factories] Prefab with type {type} is already cached.");
                return;
            }

            var pool = new NativeObjectPool<T>(prefab, parent);
            poolsByTypes.Add(type, pool);
            pool.FillPool(initialPool);
        }

        /// <inheritdoc />
        public virtual T Create<TReference>() where TReference : T
        {
            return Create(typeof(TReference));
        }

        /// <inheritdoc />
        public virtual T Create(Type type)
        {
            if (poolsByTypes.TryGetValue(type, out var pool))
            {
                return pool.Get();
            }

            Debug.LogError($"[Factories] Cannot create instance for type - {type}.");
            return null;
        }

        /// <inheritdoc />
        public virtual void Dispose(T target)
        {
            if (!target)
            {
                return;
            }

            var type = target.GetType();
            if (poolsByTypes.TryGetValue(type, out var pool))
            {
                pool.Release(target);
            }
        }
    }
}