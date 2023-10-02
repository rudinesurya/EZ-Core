using UnityEngine;
using System.Collections;

namespace EZ_Core
{
    public class BindCollisionExit2D : MonoBehaviour
    {
        [SerializeField]
        private CollisionEvent2D m_OnCollisionExit = new CollisionEvent2D();

        void OnCollisionExit2D(Collision2D other)
        {
            m_OnCollisionExit.Invoke(other);
        }
    }
}