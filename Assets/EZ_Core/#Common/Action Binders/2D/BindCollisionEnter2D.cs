using UnityEngine;
using System.Collections;

namespace EZ_Core
{
    public class BindCollisionEnter2D : MonoBehaviour
    {
        [SerializeField]
        private CollisionEvent2D m_OnCollisionEnter = new CollisionEvent2D();

        void OnCollisionEnter2D(Collision2D other)
        {
            m_OnCollisionEnter.Invoke(other);
        }
    }
}