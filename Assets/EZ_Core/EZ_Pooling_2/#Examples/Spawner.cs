using UnityEngine;
using System.Collections;
using EZ_Core;

//Simple Spawner

namespace EZ_Core.EZ_Pooling_2_Example
{
    public class Spawner : MonoBehaviour
    {
        public ActorPooled prefabA;
        public ActorPooled prefabB;

        void Update()
        {
            PoolManager.Spawn(prefabA, Random.insideUnitSphere, Quaternion.identity);

            PoolManager.Spawn(prefabB, Random.insideUnitSphere, Quaternion.identity);
        }
    }
}