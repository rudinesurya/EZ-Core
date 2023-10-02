using UnityEngine;
using System.Collections;

namespace EZ_Core
{
    public class BindTriggerEnter2D : MonoBehaviour
    {
        [SerializeField]
        private TriggerEvent2D m_OnTriggerEnter = new TriggerEvent2D();

        void OnTriggerEnter2D(Collider2D other)
        {
            m_OnTriggerEnter.Invoke(other);
        }
    }
}