using UnityEngine;

namespace EZ_Core
{
    //this is a base class that will work with EZ Pooling 2
    //extends from this class to implement additional variables / methods 
    public class ActorPooled : MonoBehaviour
    {
        [HideInInspector]
        public GameObject cachedGameObject;
        [HideInInspector]
        public Transform cachedTransform;

        //Initialize will be called when the object is created by the pool manager. 
        //It will not be called during OnSpawned or in Unity's Awake / Start methods.
        public virtual void Initialize()
        {
            cachedGameObject = this.gameObject;
            cachedTransform = this.transform;
        }

        /// <summary>
        /// This function will be called from the pool manager upon spawn
        /// </summary>
        public virtual void OnSpawned()
        {
        }

        /// <summary>
        /// This function will be called from the pool manager upon despawn
        /// </summary>
        public virtual void OnDespawned()
        {
        }
    }
}