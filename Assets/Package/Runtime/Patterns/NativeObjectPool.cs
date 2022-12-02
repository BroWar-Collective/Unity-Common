using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BroWar.Common.Patterns
{
    /// <summary>
    /// ObjectPool implementation for Prefabs.
    /// </summary>
    public class NativeObjectPool<T> : ObjectPool<T> where T : Component
    {
        private readonly bool useCustomObjectPoolHandling;

        /// <summary>
        /// MonoObjectPool constructor with default pooled objected handling.
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="parent"></param>
        /// <param name="collectionCheck">Collection checks are performed when an instance is returned back to the pool. An exception will be thrown if the instance is already in the pool. Collection checks are only performed in the Editor.</param>
        /// <param name="maxSize">The maximum size of the pool. When the pool reaches the max size then any further instances returned to the pool will be ignored and can be garbage collected. This can be used to prevent the pool growing to a very large size. If max size is set to -1 then max size is ignored.</param>
        public NativeObjectPool(T prefab, Transform parent, bool collectionCheck = false, int maxSize = -1) : this(prefab, parent, null, null, null, null, collectionCheck, maxSize)
        { }

        /// <summary>
        /// Extended MonoObjectPool constructor with custom pooled object handling with Functions and Actions callbacks.
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="parent"></param>
        /// <param name="createFunc">Create function used for creating new instances of pooled objects.</param>
        /// <param name="actionOnGet">Called when the instance is taken from the pool.</param>
        /// <param name="actionOnRelease">Called when the instance is returned to the pool. This can be used to clean up or disable the instance.</param>
        /// <param name="actionOnDestroy">Called when the element could not be returned to the pool due to the pool reaching the maximum size.</param>
        /// <param name="collectionCheck">Collection checks are performed when an instance is returned back to the pool. An exception will be thrown if the instance is already in the pool. Collection checks are only performed in the Editor.</param>
        /// <param name="maxSize">The maximum size of the pool. When the pool reaches the max size then any further instances returned to the pool will be ignored and can be garbage collected. This can be used to prevent the pool growing to a very large size. If max size is set to -1 then max size is ignored.</param>
        public NativeObjectPool(T prefab, Transform parent, Func<T> createFunc, Action<T> actionOnGet = null, Action<T> actionOnRelease = null, Action<T> actionOnDestroy = null, bool collectionCheck = false, int defaultCapacity = 10, int maxSize = -1) : base(createFunc, actionOnGet, actionOnRelease, actionOnDestroy, collectionCheck, maxSize)
        {
            Prefab = prefab;
            Parent = parent;
            useCustomObjectPoolHandling = createObjectFunction != null;
        }

        protected override T CreateElement()
        {
            if (useCustomObjectPoolHandling)
            {
                return createObjectFunction.Invoke();
            }

            var element = Object.Instantiate(Prefab, Parent);
            element.gameObject.SetActive(false);
            return element;
        }

        protected override void OnDestroyedInPool(T element)
        {
            base.OnDestroyedInPool(element);
            Object.Destroy(element.gameObject);
        }

        protected override void OnReturnedToPool(T element)
        {
            element.gameObject.SetActive(false);
            base.OnReturnedToPool(element);
        }

        protected override void OnTakenFromPool(T element)
        {
            element.gameObject.SetActive(true);
            base.OnTakenFromPool(element);
        }

        /// Template prefab for creating instances of pooled object.
        /// </summary>
        public T Prefab
        {
            get; private set;
        }

        /// <summary>
        /// Transform parent that all pooled objects are created under.
        /// </summary>
        public Transform Parent
        {
            get; private set;
        }
    }
}