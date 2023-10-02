using UnityEngine;
using System.Collections;

namespace EZ_Core
{
    public class BindCollisionEnter : MonoBehaviour
    {
        [SerializeField]
        private CollisionEvent m_OnCollisionEnter = new CollisionEvent();

        void OnCollisionEnter(Collision other)
        {
            m_OnCollisionEnter.Invoke(other);
        }
    }
}