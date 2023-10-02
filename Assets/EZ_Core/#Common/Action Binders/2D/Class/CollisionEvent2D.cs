using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace EZ_Core
{
    [System.Serializable]
    public class CollisionEvent2D : UnityEvent<Collision2D>
    {
    }
}