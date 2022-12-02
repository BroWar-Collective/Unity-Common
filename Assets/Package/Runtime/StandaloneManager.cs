using UnityEngine;

namespace BroWar.Common
{
    public abstract class StandaloneManager : MonoBehaviour
    {
        protected virtual void OnValidate()
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.Euler(Vector3.zero);
            transform.localScale = Vector3.one;
            transform.hideFlags |= HideFlags.HideInInspector;
        }
    }
}