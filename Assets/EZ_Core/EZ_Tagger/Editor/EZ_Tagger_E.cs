using UnityEngine;
using UnityEditor;
using System.Collections;

namespace EZ_Core
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(EZ_Tagger))]
    public class EZ_Tagger_E : Editor
    {
        public class EZ_EditorAssets
        {
            public static string assetName = "EZ_Tagger";

            public static TextureData assetLogo = new TextureData(null, "EZ Tagger Logo.psd");

            public static Color addBtnColor = new Color(0, 1f, 0);
            public static Color delBtnColor = new Color(1f, 0, 0);
            public static Color shiftPosColor = new Color(0.5f, 0.5f, 0.5f);
        }

        private EZ_Tagger script;

        private Vector2 scrollPos = Vector2.zero;



        public override void OnInspectorGUI()
        {
            EZ_EditorUtility.AssetName = EZ_EditorAssets.assetName;

            EditorGUI.indentLevel = 0;
            GUI.contentColor = Color.white;
            bool isDirty = false;
            script = (EZ_Tagger)target;

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EZ_EditorUtility.DrawTexture(EZ_EditorAssets.assetLogo);
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);

            EditorGUI.indentLevel = 1;

            Undo.RecordObject(script, "EZ_Tagger script");

            var new_isRootExpanded = EditorGUILayout.Foldout(script.isRootExpanded, string.Format("Tags ({0})", script.tags.Count));
            if (new_isRootExpanded != script.isRootExpanded)
            {
                foreach (var item_ in targets)
                {
                    var item__ = item_ as EZ_Tagger;

                    item__.isRootExpanded = new_isRootExpanded;
                }
            }

            GUILayout.FlexibleSpace();

            var TagEnumLength = System.Enum.GetValues(typeof(Tag)).Length;
            
            //only enable adding of pools when the application is NOT in play state
            if (!Application.isPlaying) //During Editor
            {
                EZ_EditorUtility.BeginColor(EZ_EditorAssets.addBtnColor);
                if (GUILayout.Button("Add", EditorStyles.toolbarButton, GUILayout.Width(32)))
                {
                    foreach (var item_ in targets)
                    {
                        var item__ = item_ as EZ_Tagger;

                        item__.tags.Insert(0, new Tag()); //add to the top of the list
                    }

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

                bool missingTagFound = false;
                bool duplicateFound = false;
                for (var i = 0; i < script.tags.Count; ++i)
                {
                    bool missingTagFoundPerElement = false;
                    bool duplicateFoundPerElement = false;

                    if (script.tags[i] == Tag.None || (int)script.tags[i] >= TagEnumLength)
                    {
                        missingTagFound = true;
                        missingTagFoundPerElement = true;
                    }

                    EditorGUI.indentLevel = 2;
                    EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);

                    for (var j = 0; j < script.tags.Count; ++j)
                    {
                        if (i == j)
                            continue;

                        if (script.tags[i] == script.tags[j])
                        {
                            duplicateFoundPerElement = true;
                            duplicateFound = true;
                        }
                    }

                    if (duplicateFoundPerElement || missingTagFoundPerElement)
                        EZ_EditorUtility.BeginColor(Color.red);
                    


                    var new_tag = (Tag)EditorGUILayout.EnumPopup("tag : ", script.tags[i]);
                    if (new_tag != script.tags[i])
                    {
                        foreach (var item_ in targets)
                        {
                            var item__ = item_ as EZ_Tagger;

                            item__.tags[i] = new_tag;
                        }
                    }

                    if (duplicateFoundPerElement || missingTagFoundPerElement)
                        EZ_EditorUtility.EndColor();

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

                    if (i < script.tags.Count - 1)
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
                }

                EditorGUILayout.EndScrollView();

                if (missingTagFound)
                {
                    EZ_EditorUtility.BeginColor(Color.red);
                    EditorGUILayout.LabelField("Missing Tag Found !", EditorStyles.whiteLabel);
                    EditorGUILayout.Separator();
                }

                if (duplicateFound)
                {
                    EZ_EditorUtility.BeginColor(Color.red);
                    EditorGUILayout.LabelField("Duplicate Found !", EditorStyles.whiteLabel);
                    EditorGUILayout.LabelField("Remove to increase performance.", EditorStyles.whiteLabel);
                    EditorGUILayout.Separator();
                }

                EditorGUILayout.EndVertical();

                if (i_ToRemove != -1)
                {
                    foreach (var item_ in targets)
                    {
                        var item__ = item_ as EZ_Tagger;

                        item__.tags.RemoveAt(i_ToRemove);
                    }

                    isDirty = true;
                }
                if (i_ToInsertAt != -1)
                {
                    foreach (var item_ in targets)
                    {
                        var item__ = item_ as EZ_Tagger;

                        item__.tags.Insert(i_ToInsertAt, new Tag());
                    }
                    
                    isDirty = true;
                }
                if (i_ToShiftUp != -1)
                {
                    foreach (var item_ in targets)
                    {
                        var item__ = item_ as EZ_Tagger;

                        var itemToShiftUp = item__.tags[i_ToShiftUp];
                        item__.tags.Insert(i_ToShiftUp - 1, itemToShiftUp);
                        item__.tags.RemoveAt(i_ToShiftUp + 1);
                    }
                    
                    isDirty = true;
                }

                if (i_ToShiftDown != -1)
                {
                    foreach (var item_ in targets)
                    {
                        var item__ = item_ as EZ_Tagger;

                        var index = i_ToShiftDown + 1;
                        var itemToShiftDown = item__.tags[index];
                        item__.tags.Insert(index - 1, itemToShiftDown);
                        item__.tags.RemoveAt(index + 1);
                    }
                    
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