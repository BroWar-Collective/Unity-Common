using UnityEngine;

namespace BroWar.Common
{
    /// <summary>
    /// Base class for all <see cref="Component"/>s that should be treated like Scene-based actor.
    /// </summary>
    [DisallowMultipleComponent]
    public abstract class Avatar : MonoBehaviour
    {
        protected virtual void OnShow()
        { }

        protected virtual void OnHide()
        { }

        public virtual void SetActive(bool value)
        {
            if (value == IsActive)
            {
                return;
            }

            gameObject.SetActive(value);
            if (value)
            {
                OnShow();
            }
            else
            {
                OnHide();
            }
        }

        public void Show()
        {
            SetActive(true);
        }

        public void Hide()
        {
            SetActive(false);
        }

        public bool IsActive
        {
            get => gameObject.activeSelf;
        }
    }
}