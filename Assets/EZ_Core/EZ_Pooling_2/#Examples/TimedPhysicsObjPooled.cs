using UnityEngine;
using System.Collections;
using EZ_Core;

namespace EZ_Core.EZ_Pooling_2_Example
{
    public class TimedPhysicsObjPooled : ActorPooled
    {
        public float timeout;

        [HideInInspector]
        public Rigidbody cachedRigidbody;

        //Initialize will be called when the object is created by the pool manager.
        //It will not be called during OnSpawned or in Unity's Awake / Start methods.
        public override void Initialize()
        {
            base.Initialize();

            cachedRigidbody = this.GetComponent<Rigidbody>();
        }

        public override void OnSpawned()
        {
            base.OnSpawned();

            cachedRigidbody.velocity = Vector3.zero;

            if (timeout > 0)
                StartCoroutine(Decay());
        }

        IEnumerator Decay()
        {
            yield return new WaitForSeconds(timeout);

            PoolManager.Despawn(this);
        }
    }
}