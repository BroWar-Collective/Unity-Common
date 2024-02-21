using System;
using System.Collections.Generic;

namespace BroWar.Common.Patterns
{
    /// <summary>
    /// Pool implementation using Queue.
    /// </summary>
    public class ObjectPool<T>
    {
        protected Func<T> createObjectFunction;
        protected Action<T> onTakenFromPoolAction;
        protected Action<T> onReturnedToPoolAction;
        protected Action<T> onDestroyedInPoolAction;

        private readonly Queue<T> pooledObjects;
        private readonly int maxSize;
        private readonly bool collectionCheck;

        /// <summary>
        /// ObjectPool constructor with needed Functions and Actions callbacks.
        /// </summary>
        /// <param name="createObjectFunction">Create function used for creating new instances of pooled objects.</param>
        /// <param name="onTakenFromPoolAction">Called when the instance is taken from the pool.</param>
        /// <param name="onReturnedToPoolAction">Called when the instance is returned to the pool. This can be used to clean up or disable the instance.</param>
        /// <param name="onDestroyedInPoolAction">Called when the element could not be returned to the pool due to the pool reaching the maximum size.</param>
        /// <param name="collectionCheck">Collection checks are performed when an instance is returned back to the pool. An exception will be thrown if the instance is already in the pool. Collection checks are only performed in the Editor.</param>
        /// <param name="maxSize">The maximum size of the pool. When the pool reaches the max size then any further instances returned to the pool will be ignored and can be garbage collected. This can be used to prevent the pool growing to a very large size. If max size is set to -1 then max size is ignored.</param>
        public ObjectPool(Func<T> createObjectFunction, Action<T> onTakenFromPoolAction = null, Action<T> onReturnedToPoolAction = null, Action<T> onDestroyedInPoolAction = null, bool collectionCheck = false, int maxSize = -1)
        {
            pooledObjects = new Queue<T>();

            this.maxSize = maxSize;
            this.collectionCheck = collectionCheck;

            this.createObjectFunction = createObjectFunction;
            this.onTakenFromPoolAction = onTakenFromPoolAction;
            this.onReturnedToPoolAction = onReturnedToPoolAction;
            this.onDestroyedInPoolAction = onDestroyedInPoolAction;

            CountActive = 0;
        }

        protected virtual T CreateElement()
        {
            return createObjectFunction.Invoke();
        }

        protected virtual void OnTakenFromPool(T element)
        {
            onTakenFromPoolAction?.Invoke(element);
        }

        protected virtual void OnReturnedToPool(T element)
        {
            onReturnedToPoolAction?.Invoke(element);
        }

        protected virtual void OnDestroyedInPool(T element)
        {
            onDestroyedInPoolAction?.Invoke(element);
        }

        public void FillPool(int initialPoolSize = 0)
        {
            for (var i = 0; i < initialPoolSize; i++)
            {
                var pooledObject = CreateElement();
                pooledObjects.Enqueue(pooledObject);
            }
        }

        /// <summary>
        /// Get an instance from the pool. If the pool is empty then a new instance will be created.
        /// </summary>
        /// <returns>A pooled object or a new instance if the pool is empty.</returns>
        public T Get()
        {
            var element = pooledObjects.Count == 0 ? CreateElement() : pooledObjects.Dequeue();

            OnTakenFromPool(element);
            CountActive++;
            return element;
        }

        /// <summary>
        /// Returns the instance back to the pool.
        /// If the pool has collection checks enabled and the instance is already held by the pool then an exception will be thrown (Editor only).
        /// </summary>
        /// <param name="element">The instance to return to the pool.</param>
        public void Release(T element)
        {
#if UNITY_EDITOR
            if (collectionCheck && pooledObjects.Contains(element))
            {
                throw new Exception($"[Common] Item {element} cannot be returned to the pool as it already exists in the pool!");
            }
#endif

            if (maxSize == -1 || pooledObjects.Count < maxSize)
            {
                pooledObjects.Enqueue(element);
                OnReturnedToPool(element);
            }
            else
            {
                OnDestroyedInPool(element);
            }

            CountActive--;
        }

        /// <summary>
        /// Removes all pooled items. If the pool contains a destroy callback then it will be called for each item that is in the pool.
        /// </summary>
        public void Clear()
        {
            var count = pooledObjects.Count;
            for (var i = 0; i < count; i++)
            {
                var element = pooledObjects.Dequeue();
                onDestroyedInPoolAction?.Invoke(element);
            }

            CountActive = 0;
        }

        /// <summary>
        /// Number of objects that have been created by the pool but are currently in use and have not yet been returned.
        /// </summary>
        public int CountActive
        {
            get; private set;
        }

        /// <summary>
        /// Number of objects that are currently available in the pool.
        /// </summary>
        public int CountPooled => pooledObjects.Count;

        /// <summary>
        /// The total number of active and inactive (pooled) objects.
        /// </summary>
        public int CountAll => CountActive + CountPooled;
    }
}