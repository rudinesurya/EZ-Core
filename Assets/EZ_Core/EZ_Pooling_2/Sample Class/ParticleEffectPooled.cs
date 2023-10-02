using UnityEngine;
using System.Collections;

namespace EZ_Core
{
    public class ParticleEffectPooled : ActorPooled
    {
        public float timeout;

        //Initialize will be called when the object is created by the pool manager.
        //It will not be called during OnSpawned or in Unity's Awake / Start methods.
        public override void Initialize()
        {
            base.Initialize();
        }

        public override void OnSpawned()
        {
            base.OnSpawned();

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