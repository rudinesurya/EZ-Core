using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

namespace EZ_Core
{
    [CustomEditor(typeof(PoolManager))]
    public class PoolManager_E : Editor
    {
        public class EZ_EditorAssets
        {
            public static string assetName = "EZ_Pooling_2";

            public static TextureData poolManagerItemLogo = new TextureData(null, "EZ Pooling Logo.psd");
            public static TextureData missingPrefabIcon = new TextureData(null, "missingPrefabIcon.psd");
            public static TextureData poolItemTop = new TextureData(null, "Pool Item Top Logo.psd");
            public static TextureData poolItemBottom = new TextureData(null, "Pool Item Bottom Logo.psd");
            public static TextureData errorItemTop = new TextureData(null, "Error Item Top Logo.psd");
            public static TextureData errorItemBottom = new TextureData(null, "Error Item Bottom Logo.psd");

            public static Color addBtnColor = new Color(0, 1f, 0);
            public static Color delBtnColor = new Color(1f, 0, 0);
            public static Color shiftPosColor = new Color(0.5f, 0.5f, 0.5f);
        }

        private PoolManager script;

        private Vector2 scrollPos = Vector2.zero;

        public override void OnInspectorGUI()
        {
            EZ_EditorUtility.AssetName = EZ_EditorAssets.assetName;

            EditorGUI.indentLevel = 0;
            GUI.contentColor = Color.white;
            bool isDirty = false;
            script = (PoolManager)target;

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EZ_EditorUtility.DrawTexture(EZ_EditorAssets.poolManagerItemLogo);
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            Undo.RecordObject(script, "PoolManager script");
            script.autoAddMissingPrefabPool = EditorGUILayout.Toggle("Auto Add Missing Items", script.autoAddMissingPrefabPool);
            script.showDebugLog = EditorGUILayout.Toggle("Show Debug Log", script.showDebugLog);

            //Default pool setting
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Default Settings for dynamically pooled objects", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Won't affect if there is a 'PrefabPoolDefaultOverride' script present", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Won't affect objects currently in the Pool Manager.", EditorStyles.boldLabel);
            EditorGUILayout.Separator();

            var settings = script.defaultSetting;
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

            EditorGUILayout.Separator();
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);

            EditorGUI.indentLevel = 1;

            script.isRootExpanded = EditorGUILayout.Foldout(script.isRootExpanded, string.Format("Pools ({0})", script.prefabPoolSettings_List.Count));

            // Add expand / collapse buttons if there are items in the list
            if (script.prefabPoolSettings_List.Count > 0)
            {
                EZ_EditorUtility.BeginColor(EZ_EditorAssets.shiftPosColor);
                var masterCollapse = GUILayout.Button("Collapse All", EditorStyles.toolbarButton, GUILayout.Width(80));

                var masterExpand = GUILayout.Button("Expand All", EditorStyles.toolbarButton, GUILayout.Width(80));
                EZ_EditorUtility.EndColor();

                if (masterExpand)
                {
                    foreach (var item in script.prefabPoolSettings_List)
                    {
                        item.isPoolExpanded = true;
                    }
                }

                if (masterCollapse)
                {
                    foreach (var item in script.prefabPoolSettings_List)
                    {
                        item.isPoolExpanded = false;
                    }
                }
            }
            else
            {
                GUILayout.FlexibleSpace();
            }

            //only enable adding of pools when the application is NOT in play state
            if (!Application.isPlaying) //During Editor
            {
                EZ_EditorUtility.BeginColor(EZ_EditorAssets.addBtnColor);
                if (GUILayout.Button("Add", EditorStyles.toolbarButton, GUILayout.Width(32)))
                {
                    script.prefabPoolSettings_List.Insert(0, new PrefabPoolSettings()); //add to the top of the list
                    isDirty = true;
                }
                EZ_EditorUtility.EndColor();
            }

            EditorGUILayout.EndHorizontal();

            if (script.isRootExpanded)
            {
                int i_ToRemove = -1;
                int i_ToInsertAt = -1;
                int i_ToShiftUp = -1;
                int i_ToShiftDown = -1;

                EditorGUILayout.BeginVertical();

                scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(0), GUILayout.Height(0));

                bool missingPoolNameFound = false;
                bool duplicateFound = false;
                for (var i = 0; i < script.prefabPoolSettings_List.Count; ++i)
                {
                    bool missingPoolNameFoundPerElement = false;
                    bool duplicateFoundPerElement = false;

                    var prefabPoolOption = script.prefabPoolSettings_List[i];

                    EditorGUI.indentLevel = 2;
                    EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);

                    //Check for duplicates
                    for (var j = 0; j < script.prefabPoolSettings_List.Count; ++j)
                    {
                        if (i == j)
                            continue;

                        //if havent fill, skip
                        if (script.prefabPoolSettings_List[i].sourcePrefab == null || script.prefabPoolSettings_List[j].sourcePrefab == null)
                            continue;

                        if (script.prefabPoolSettings_List[i].sourcePrefab.name == script.prefabPoolSettings_List[j].sourcePrefab.name)
                        {
                            duplicateFoundPerElement = true;
                            duplicateFound = true;
                        }
                    }

                    var poolName = prefabPoolOption.sourcePrefab == null ? "" : prefabPoolOption.sourcePrefab.name;
                    string label;

                    if (prefabPoolOption.sourcePrefab == null || string.IsNullOrEmpty(poolName))
                    {
                        missingPoolNameFound = true;
                        missingPoolNameFoundPerElement = true;
                        label = "ERROR ! Check your prefab";
                    }
                    else
                    {
                        label = poolName.ToString();
                    }

                    prefabPoolOption.isPoolExpanded = EditorGUILayout.Foldout(prefabPoolOption.isPoolExpanded, label, EditorStyles.foldout);

                    if (duplicateFoundPerElement || missingPoolNameFoundPerElement)
                        prefabPoolOption.isPoolExpanded = true;

                    EZ_EditorUtility.BeginColor(EZ_EditorAssets.shiftPosColor);

                    if (i > 0)
                    {
                        // the up arrow.
                        if (GUILayout.Button("▲", EditorStyles.toolbarButton, GUILayout.Width(24)))
                        {
                            i_ToShiftUp = i;
                        }
                    }
                    else
                    {
                        GUILayout.Space(24);
                    }

                    if (i < script.prefabPoolSettings_List.Count - 1)
                    {
                        // The down arrow will move things towards the end of the List
                        if (GUILayout.Button("▼", EditorStyles.toolbarButton, GUILayout.Width(24)))
                        {
                            i_ToShiftDown = i;
                        }
                    }
                    else
                    {
                        GUILayout.Space(24);
                    }

                    EZ_EditorUtility.EndColor();

                    //only enable adding or deleting of pools when the application is NOT in play state
                    if (!Application.isPlaying) //During Editor
                    {
                        EZ_EditorUtility.BeginColor(EZ_EditorAssets.addBtnColor);
                        if (GUILayout.Button("Add", EditorStyles.toolbarButton, GUILayout.Width(32)))
                        {
                            i_ToInsertAt = i + 1;
                        }
                        EZ_EditorUtility.EndColor();

                        EZ_EditorUtility.BeginColor(EZ_EditorAssets.delBtnColor);
                        if (GUILayout.Button("Del", EditorStyles.toolbarButton, GUILayout.Width(32)))
                        {
                            i_ToRemove = i;
                        }
                        EZ_EditorUtility.EndColor();
                    }

                    EditorGUILayout.EndHorizontal();

                    if (prefabPoolOption.isPoolExpanded)
                    {
                        EditorGUI.indentLevel = 1;

                        if (duplicateFoundPerElement || missingPoolNameFoundPerElement)
                            EZ_EditorUtility.DrawTexture(EZ_EditorAssets.errorItemTop);
                        else
                            EZ_EditorUtility.DrawTexture(EZ_EditorAssets.poolItemTop);

                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.BeginVertical();

                        if (!Application.isPlaying) //During Editor
                        {
                            prefabPoolOption.parentTransform = (Transform)EditorGUILayout.ObjectField("Parent", prefabPoolOption.parentTransform, typeof(Transform), true);

                            if (duplicateFoundPerElement || missingPoolNameFoundPerElement)
                                EZ_EditorUtility.BeginColor(Color.red);

                            prefabPoolOption.sourcePrefab = (ActorPooled)EditorGUILayout.ObjectField("Prefab", prefabPoolOption.sourcePrefab, typeof(ActorPooled), false);

                            if (duplicateFoundPerElement || missingPoolNameFoundPerElement)
                                EZ_EditorUtility.EndColor();

                            prefabPoolOption.preloadAmount = EditorGUILayout.IntSlider("Preload Qty", prefabPoolOption.preloadAmount, 1, 10000);
                            prefabPoolOption.showDebugLog = EditorGUILayout.Toggle("Show Debug Log", prefabPoolOption.showDebugLog);
                        }
                        else //During Play mode
                        {
                            if (prefabPoolOption.sourcePrefab != null)
                            {
                                var itemInfo = PoolManager.GetPool(poolName);
                                if (itemInfo != null)
                                {
                                    var spawnedCount = itemInfo.spawnedList.Count;
                                    var totalCount = itemInfo.spawnedList.Count + itemInfo.despawnedList.Count;
                                    EditorGUILayout.LabelField(string.Format("{0} / {1} Spawned", spawnedCount, totalCount), EditorStyles.boldLabel);
                                    EditorGUILayout.Separator();
                                }
                            }

                            if (prefabPoolOption.parentTransform)
                                EditorGUILayout.LabelField("Parent : " + prefabPoolOption.parentTransform.name, EditorStyles.miniLabel);
                            else
                                EditorGUILayout.LabelField("No Parent", EditorStyles.miniLabel);

                            EditorGUILayout.LabelField("Preload Qty : " + prefabPoolOption.preloadAmount, EditorStyles.miniLabel);
                        }

                        EditorGUILayout.EndVertical();
                        EditorGUILayout.EndHorizontal();

                        if (duplicateFoundPerElement || missingPoolNameFoundPerElement)
                            EZ_EditorUtility.DrawTexture(EZ_EditorAssets.errorItemBottom);
                        else
                            EZ_EditorUtility.DrawTexture(EZ_EditorAssets.poolItemBottom);
                    }
                }

                EditorGUILayout.EndScrollView();
                EditorGUILayout.EndVertical();

                if (missingPoolNameFound)
                {
                    EZ_EditorUtility.BeginColor(Color.red);
                    EditorGUILayout.LabelField("Missing Pool Name Found !", EditorStyles.whiteLabel);
                    EditorGUILayout.Separator();
                }

                if (duplicateFound)
                {
                    EZ_EditorUtility.BeginColor(Color.red);
                    EditorGUILayout.LabelField("Duplicate Found !", EditorStyles.whiteLabel);
                    EditorGUILayout.LabelField("Remove to increase performance.", EditorStyles.whiteLabel);
                    EditorGUILayout.Separator();
                }

                if (i_ToRemove != -1)
                {
                    script.prefabPoolSettings_List.RemoveAt(i_ToRemove);
                    isDirty = true;
                }
                if (i_ToInsertAt != -1)
                {
                    script.prefabPoolSettings_List.Insert(i_ToInsertAt, new PrefabPoolSettings());
                    isDirty = true;
                }
                if (i_ToShiftUp != -1)
                {
                    var item = script.prefabPoolSettings_List[i_ToShiftUp];
                    script.prefabPoolSettings_List.Insert(i_ToShiftUp - 1, item);
                    script.prefabPoolSettings_List.RemoveAt(i_ToShiftUp + 1);
                    isDirty = true;
                }

                if (i_ToShiftDown != -1)
                {
                    var index = i_ToShiftDown + 1;
                    var item = script.prefabPoolSettings_List[index];
                    script.prefabPoolSettings_List.Insert(index - 1, item);
                    script.prefabPoolSettings_List.RemoveAt(index + 1);
                    isDirty = true;
                }
            }


            if (GUI.changed || isDirty)
            {
                EditorUtility.SetDirty(target);
            }

            this.Repaint();
        }
    }
}