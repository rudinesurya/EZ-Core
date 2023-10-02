using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EZ_Core
{
    public static class EZ_Tagger_Methods
    {
        /// <summary>
        /// Return true if tags1 contain tag2.
        /// </summary>
        /// <param name="tags1"></param>
        /// <param name="tag2"></param>
        /// <returns></returns>
        public static bool CompareTag(List<Tag> tags1, Tag tag2)
        {
            if (tags1.Contains(tag2))
            {
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Return true if tags1 contain tags2 depending on the matcher type.
        /// <para>Matcher.Exact : all the tags must match</para>
        /// <para>Matcher.Or : at least one must match</para>
        /// </summary>
        /// <param name="tags1"></param>
        /// <param name="tags2"></param>
        /// <param name="matcher"></param>
        /// <returns></returns>
        public static bool CompareTag(List<Tag> tags1, List<Tag> tags2, Matcher matcher = Matcher.Exact)
        {
            if (matcher == Matcher.Exact)
            {
                bool can = true;
                for (int i = 0; i < tags2.Count; ++i)
                {
                    if (!tags1.Contains(tags2[i]))
                    {
                        can = false;
                        break;
                    }
                }

                if (can)
                    return true;
            }
            else
            {
                bool can = false;
                for (int i = 0; i < tags2.Count; ++i)
                {
                    if (tags1.Contains(tags2[i]))
                    {
                        can = true;
                        break;
                    }
                }

                if (can)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Return a list filled with all the gameobjects that has the tag.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static List<GameObject> FindGameObjectsWithTags(Tag tag)
        {
            EZ_Tagger[] tagged = GameObject.FindObjectsOfType<EZ_Tagger>();

            List<GameObject> matches = new List<GameObject>();

            for (int i = 0; i < tagged.Length; ++i)
            {
                EZ_Tagger item = tagged[i];

                if (item.tags.Contains(tag))
                    matches.Add(item.gameObject);
            }

            return matches;
        }

        /// <summary>
        /// Return a list filled with all the gameobjects that has the tag.
        /// <para>Matcher.Exact : all the tags must match</para>
        /// <para>Matcher.Or : at least one must match</para>
        /// </summary>
        /// <param name="tags"></param>
        /// <param name="matcher"></param>
        /// <returns></returns>
        public static List<GameObject> FindGameObjectsWithTags(List<Tag> tags, Matcher matcher = Matcher.Exact)
        {
            EZ_Tagger[] tagged = GameObject.FindObjectsOfType<EZ_Tagger>();

            List<GameObject> matches = new List<GameObject>();

            if (matcher == Matcher.Exact)
            {
                for (int i = 0; i < tagged.Length; ++i)
                {
                    EZ_Tagger item = tagged[i];

                    bool add = true;
                    for (int j = 0; j < tags.Count; ++j)
                    {
                        var tag = tags[j];
                        if (!item.tags.Contains(tag))
                        {
                            add = false;
                            break;
                        }
                    }

                    if (add)
                        matches.Add(item.gameObject);
                }
            }
            else
            {
                for (int i = 0; i < tagged.Length; ++i)
                {
                    EZ_Tagger item = tagged[i];

                    bool add = false;
                    for (int j = 0; j < tags.Count; ++j)
                    {
                        var tag = tags[j];
                        if (item.tags.Contains(tag))
                        {
                            add = true;
                            break;
                        }
                    }

                    if (add)
                        matches.Add(item.gameObject);
                }
            }

            return matches;
        }
    }
}