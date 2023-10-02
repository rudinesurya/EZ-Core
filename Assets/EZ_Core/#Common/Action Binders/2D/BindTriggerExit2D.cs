using UnityEngine;
using System.Collections;

namespace EZ_Core
{
    public class BindTriggerExit2D : MonoBehaviour
    {
        [SerializeField]
        private TriggerEvent2D m_OnTriggerExit = new TriggerEvent2D();

        void OnTriggerExit2D(Collider2D other)
        {
            m_OnTriggerExit.Invoke(other);
        }
    }
}