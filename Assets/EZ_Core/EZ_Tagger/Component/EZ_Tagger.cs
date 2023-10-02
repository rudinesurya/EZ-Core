using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EZ_Core
{
    public class EZ_Tagger : MonoBehaviour
    {
        public List<Tag> tags = new List<Tag>();

        //Editor usage
        [HideInInspector]
        public bool isRootExpanded = true;
    }
}