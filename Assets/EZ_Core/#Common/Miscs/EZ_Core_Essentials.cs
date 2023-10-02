using UnityEngine;
using System.Collections;

//Class used to store Unity Transform var in serializable format instead of Unity Object.
//This class might come handy when you need to store a player's starting transform to be used for respawning later.

namespace EZ_Core
{
    [System.Serializable]
    public class UnityTransformData
    {
        public Vector3 position;
        public Vector3 scale;
        public Quaternion rotation;

        public UnityTransformData(Transform trans)
        {
            position = trans.position;
            scale = trans.localScale;
            rotation = trans.rotation;
        }
    }
}