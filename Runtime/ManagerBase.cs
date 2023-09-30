using System;
using UnityEngine;

namespace BroWar.Common
{
    /// <summary>
    /// <see cref="Behaviour"/>-based, manager-like class.
    /// Mainly used to unify component Managers in the project.
    /// </summary>
    public abstract class ManagerBase : MonoBehaviour, IInitializable, IDeinitializable
    {
        public event Action OnInitialized;
        public event Action OnDeinitialized;

        private void OnDestroy()
        {
            Deinitialize();
        }

        protected virtual void OnValidate()
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.Euler(Vector3.zero);
            transform.localScale = Vector3.one;
            transform.hideFlags |= HideFlags.HideInInspector;
        }

        protected virtual void OnInitialize()
        { }

        protected virtual void OnDeinitialize()
        { }

        public virtual void Initialize()
        {
            if (IsInitialized)
            {
                var type = GetType();
                LogHandler.Log($"[Common] Cannot initialize {type.Name} - manager is already initialized.", LogType.Warning);
                return;
            }

            OnInitialize();
            IsInitialized = true;
            OnInitialized?.Invoke();
        }

        public virtual void Deinitialize()
        {
            if (!IsInitialized)
            {
                return;
            }

            OnDeinitialize();
            IsInitialized = false;
            OnDeinitialized?.Invoke();
        }

        public bool IsInitialized { get; protected set; }
    }
}