using UnityEngine;
using System.Collections;

namespace EZ_Core
{
    public class BindCollisionExit : MonoBehaviour
    {
        [SerializeField]
        private CollisionEvent m_OnCollisionExit = new CollisionEvent();

        void OnCollisionExit(Collision other)
        {
            m_OnCollisionExit.Invoke(other);
        }
    }
}