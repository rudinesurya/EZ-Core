using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using System.IO;
#endif

namespace EZ_Core
{
    public class TagList : ScriptableObject
    {
        public string[] tagList;

#if UNITY_EDITOR

        [MenuItem("Window/EZ Core/EZ Tagger/Edit Tag List")]
        public static void Edit()
        {
            UnityEditor.Selection.activeObject = Resources.Load("TagList");
        }

        [ContextMenu("Save")]
        void Save()
        {
            //Create the enum class
            List<string> contents = new List<string>();

            contents.Add("//This is a auto-generated code. Do not edit directly in the .cs file !!");
            contents.Add("//Modify the ScriptableObjects accessed via the Windows drop down menu item to add / delete new stuffs.");
            contents.Add("");
            contents.Add("namespace EZ_Core");
            contents.Add("{");
            contents.Add("    public enum Tag");
            contents.Add("    {");

            contents.Add("        " + "None" + ",");

            for (int i = 0; i < tagList.Length; ++i)
            {
                contents.Add("        " + tagList[i] + ",");
            }

            contents.Add("    }");
            contents.Add("}");

            try
            {
                File.WriteAllLines("Assets/EZ_Core/EZ_Tagger/Generated/Tags.cs", contents.ToArray());
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Error writing Tags enum" + ex.Message.ToString());
            }

            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        }
#endif
    }
}