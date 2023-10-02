using UnityEngine;
using UnityEditor;
using System.Collections;

namespace EZ_Core
{
    [CustomEditor(typeof(PrefabPoolDefaultOverride))]
    public class PrefabPoolDefaultOverride_E : Editor
    {
        private PrefabPoolDefaultOverride script;

        public override void OnInspectorGUI()
        {
            script = (PrefabPoolDefaultOverride)target;
            var settings = script.settings;

            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Will override Pool Manager's Default Settings for dynamically pooled objects", EditorStyles.boldLabel);
            EditorGUILayout.Separator();


            if (!Application.isPlaying) //During Editor
            {
                settings.parentTransform = (Transform)EditorGUILayout.ObjectField("Parent", settings.parentTransform, typeof(Transform), true);
                settings.preloadAmount = EditorGUILayout.IntSlider("Preload Qty", settings.preloadAmount, 1, 10000);

                settings.showDebugLog = EditorGUILayout.Toggle("Show Debug Log", settings.showDebugLog);
            }
            else
            {
                if (settings.parentTransform)
                    EditorGUILayout.LabelField("Parent : " + settings.parentTransform.name, EditorStyles.miniLabel);
                else
                    EditorGUILayout.LabelField("No Parent", EditorStyles.miniLabel);

                EditorGUILayout.LabelField("Preload Qty : " + settings.preloadAmount, EditorStyles.miniLabel);
            }
        }
    }
}