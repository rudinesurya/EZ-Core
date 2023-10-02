using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EZ_Core;

namespace EZ_Core.EZ_Tagger_Example
{
    public class Example : MonoBehaviour
    {
        public Tag circleTag;
        public Tag boxTag;
        public Tag redTag;
        public Tag blueTag;

        void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 150, 100), "show red circle"))
            {
                HideAll();

                var tags = new List<Tag>();
                tags.Add(redTag);
                tags.Add(circleTag);

                var gos = EZ_Tagger_Methods.FindGameObjectsWithTags(tags);
                for (int i = 0; i < gos.Count; ++i)
                    gos[i].GetComponent<Renderer>().enabled = true;
            }

            if (GUI.Button(new Rect(10, 110, 150, 100), "show red box"))
            {
                HideAll();

                var tags = new List<Tag>();
                tags.Add(redTag);
                tags.Add(boxTag);

                var gos = EZ_Tagger_Methods.FindGameObjectsWithTags(tags);
                for (int i = 0; i < gos.Count; ++i)
                    gos[i].GetComponent<Renderer>().enabled = true;
            }

            if (GUI.Button(new Rect(10, 210, 150, 100), "show red geometry"))
            {
                HideAll();
                var gos = EZ_Tagger_Methods.FindGameObjectsWithTags(redTag);
                for (int i = 0; i < gos.Count; ++i)
                    gos[i].GetComponent<Renderer>().enabled = true;
            }

            if (GUI.Button(new Rect(10, 310, 150, 100), "show blue geometry"))
            {
                HideAll();
                var gos = EZ_Tagger_Methods.FindGameObjectsWithTags(blueTag);
                for (int i = 0; i < gos.Count; ++i)
                    gos[i].GetComponent<Renderer>().enabled = true;
            }

            if (GUI.Button(new Rect(10, 410, 150, 100), "show all geometry"))
            {
                HideAll();

                var tags = new List<Tag>();
                tags.Add(circleTag);
                tags.Add(boxTag);

                var gos = EZ_Tagger_Methods.FindGameObjectsWithTags(tags, Matcher.Or);
                for (int i = 0; i < gos.Count; ++i)
                    gos[i].GetComponent<Renderer>().enabled = true;
            }
        }

        void HideAll()
        {
            var gos = FindObjectsOfType<EZ_Tagger>();
            for (int i = 0; i < gos.Length; ++i)
                gos[i].GetComponent<Renderer>().enabled = false;
        }
    }
}