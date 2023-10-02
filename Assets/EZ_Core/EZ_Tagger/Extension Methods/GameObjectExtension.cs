using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EZ_Core
{
    public static class GameObjectExtension
    {
        /// <summary>
        /// Get all the tags of this gameobject
        /// </summary>
        /// <param name="go"></param>
        /// <returns></returns>
        public static List<Tag> GetTags(this GameObject go)
        {
            var tagger = go.GetComponent<EZ_Tagger>();
			if (tagger == null)
			{
				Debug.LogWarning("EZ_Tagger Component missing !");
				return null;
			}
            else
            {
                List<Tag> enumTags = new List<Tag>();
                int length = tagger.tags.Count;
                for (int i = 0; i < length; ++i)
                {
                    var tagEnum = tagger.tags[i];
                    enumTags.Add(tagEnum);
                }
                return enumTags;
            }
        }

        /// <summary>
        /// Add a new tag to the gameobject
        /// </summary>
        /// <param name="go"></param>
        /// <param name="tag"></param>
        /// <param name="checkDuplicate"></param>
        public static void AddTag(this GameObject go, Tag tag, bool checkDuplicate = false)
        {
            var tagger = go.GetComponent<EZ_Tagger>();
            if (tagger == null)
            {
                tagger = go.AddComponent<EZ_Tagger>();
            }

            if (checkDuplicate)
            {
                if (tagger.tags.Contains(tag))
                {
                    DebugClass.LogWarning("Duplicate tag assigned !");
                    return;
                }
            }

            tagger.tags.Add(tag);
        }
        
        /// <summary>
        /// Is this object tagged with tag ?
        /// </summary>
        /// <param name="col"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static bool CompareTag(this GameObject go, Tag tag)
        {
            var tagger = go.GetComponent<EZ_Tagger>();
			if (tagger == null)
			{
				Debug.LogWarning("EZ_Tagger Component missing !");
				return false;
			}

            return EZ_Tagger_Methods.CompareTag(tagger.tags, tag);
        }

        /// <summary>
        /// Is this object tagged with tags ?
        /// <para>Matcher.Exact : all the tags must match</para>
        /// <para>Matcher.Or : at least one must match</para>
        /// </summary>
        /// <param name="col"></param>
        /// <param name="tags"></param>
        /// <param name="matcher"></param>
        /// <returns></returns>
        public static bool CompareTag(this GameObject go, List<Tag> tags, Matcher matcher = Matcher.Exact)
        {
            var tagger = go.GetComponent<EZ_Tagger>();
			if (tagger == null)
			{
				Debug.LogWarning("EZ_Tagger Component missing !");
				return false;
			}

            return EZ_Tagger_Methods.CompareTag(tagger.tags, tags, matcher);
        }
    }
}