using UnityEngine;
using System.Collections;

namespace EZ_Core
{
    public class BindTriggerExit : MonoBehaviour
    {
        [SerializeField]
        private TriggerEvent m_OnTriggerExit = new TriggerEvent();

        void OnTriggerExit(Collider other)
        {
            m_OnTriggerExit.Invoke(other);
        }
    }
}