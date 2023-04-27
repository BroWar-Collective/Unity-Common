using UnityEngine;

namespace BroWar.Common.Patterns
{
    /// <summary>
    /// Standard Unity-based implementation of the Singleton pattern. 
    /// Suggested not to use it in the production code, can be useful for testing.
    /// </summary>
    [DisallowMultipleComponent]
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static readonly object objectLock = new object();

        private static T instance;

        protected virtual void Awake()
        {
            if (!(instance is null))
            {
                if (instance != this)
                {
                    Destroy(gameObject);
                }

                return;
            }

            instance = (T)this;
            if (transform.parent)
            {
                return;
            }

            DontDestroyOnLoad(instance.gameObject);
        }

        protected virtual void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }

        public static T Instance
        {
            get
            {
                lock (objectLock)
                {
                    if (instance is null)
                    {
                        instance = FindObjectOfType<T>();
                        if (instance is null)
                        {
                            var singletonObject = new GameObject();
                            instance = singletonObject.AddComponent<T>();
                            singletonObject.name = $"{typeof(T)} (Singleton)";
                            DontDestroyOnLoad(singletonObject);
                        }
                    }

                    return instance;
                }
            }
        }

        public static bool HasInstance
        {
            get => instance;
        }
    }
}