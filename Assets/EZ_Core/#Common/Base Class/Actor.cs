using UnityEngine;

namespace EZ_Core
{
    public class Actor : MonoBehaviour
    {
        public GameObject cachedGameObject;
        public Transform cachedTransform;

        protected virtual void Initialize()
        {
            cachedGameObject = this.gameObject;
            cachedTransform = this.transform;
        }

        void Awake()
        {
            SystemCheck.Log("Actor Awake()");
            Initialize();
        }
    }
}