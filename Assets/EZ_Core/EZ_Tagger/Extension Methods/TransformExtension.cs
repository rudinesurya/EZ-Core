using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EZ_Core
{
    public static class TransformExtension
    {
        /// <summary>
        /// Is this object tagged with tag ?
        /// </summary>
        /// <param name="col"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static bool CompareTag(this Transform t, Tag tag)
        {
            var tagger = t.GetComponent<EZ_Tagger>();
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
        public static bool CompareTag(this Transform t, List<Tag> tags, Matcher matcher = Matcher.Exact)
        {
            var tagger = t.GetComponent<EZ_Tagger>();
			if (tagger == null)
			{
				Debug.LogWarning("EZ_Tagger Component missing !");
				return false;
			}

            return EZ_Tagger_Methods.CompareTag(tagger.tags, tags, matcher);
        }
    }
}