using UnityEngine;
using System.Collections;

namespace EZ_Core
{
    public class BindTriggerEnter : MonoBehaviour
    {
        [SerializeField]
        private TriggerEvent m_OnTriggerEnter = new TriggerEvent();

        void OnTriggerEnter(Collider other)
        {
            m_OnTriggerEnter.Invoke(other);
        }
    }
}