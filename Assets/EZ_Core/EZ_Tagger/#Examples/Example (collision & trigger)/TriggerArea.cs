using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EZ_Core;

namespace EZ_Core.EZ_Tagger_Example2
{
    public class TriggerArea : MonoBehaviour
    {
        public List<Tag> allowedTag;
        public Matcher matcher;
        public GameObject allowedSign;
        public GameObject notAllowedSign;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(allowedTag, matcher))
            {
                DebugClass.Log(other.name + " allowed to enter " + this.name);
                var go = Instantiate(allowedSign, other.transform.position, Quaternion.identity);
                Destroy(go, 1f);
            }
            else
            {
                var go = Instantiate(notAllowedSign, other.transform.position, Quaternion.identity);
                Destroy(go, 1f);
            }
        }
    }
}