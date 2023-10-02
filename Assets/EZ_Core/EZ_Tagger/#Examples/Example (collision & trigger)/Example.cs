using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EZ_Core;

namespace EZ_Core.EZ_Tagger_Example2
{
    public class Example : MonoBehaviour
    {
        public LayerMask pickableLayer;

        bool picking;
        Transform picked;

        void Update()
        {
            var worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldMousePosition.z = 0;

            if (Input.GetMouseButtonDown(0) && !picking)
            {
                var go = Physics2D.OverlapPoint(worldMousePosition, pickableLayer);

                if (go)
                {
                    picking = true;
                    picked = go.transform;
                }
            }

            if (picking)
            {
                //update its position
                picked.transform.position = worldMousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                picking = false;
            }
        }

        void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 150, 100), "Reset scene"))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }
}