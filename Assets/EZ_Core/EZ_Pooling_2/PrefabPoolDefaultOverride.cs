using UnityEngine;

//A component which will override the Pool Manager default settings
//It only works when the spawned object is not registered in the pool, which means the object is only registered during play mode using 'Auto Add Missing Items'

namespace EZ_Core
{
    public class PrefabPoolDefaultOverride : MonoBehaviour
    {
        public PrefabPoolSettings settings;
    }
}