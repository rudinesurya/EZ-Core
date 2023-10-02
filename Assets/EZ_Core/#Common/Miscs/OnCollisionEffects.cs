using UnityEngine;
using System.Collections;

namespace EZ_Core
{
    public class OnCollisionEffects : MonoBehaviour
    {
        public ActorPooled particleEffect;
        bool isVisible;

        void OnCollisionEnter2D(Collision2D other)
        {
            if (!isVisible)
                return;

            for (int i = 0; i < other.contacts.Length; ++i)
            {
                PoolManager.Spawn(particleEffect, other.contacts[i].point, Quaternion.Euler(other.contacts[i].normal));
            }
        }

        void OnBecameVisible()
        {
            isVisible = true;
        }

        void OnBecameInvisible()
        {
            isVisible = false;
        }
    }
}