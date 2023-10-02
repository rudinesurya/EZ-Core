using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

//Code generator to make new custom class that derive from ActorPooled easy

namespace EZ_Core
{
    class EZ_Pooling_CreateTemplateScript : EditorWindow
    {
        string newClassName = "NewActorPooled";

        [MenuItem("Assets/Create/NewActorPooled C#")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(EZ_Pooling_CreateTemplateScript));
        }

        void OnGUI()
        {
            //GUILayout.Label("Base Settings", EditorStyles.boldLabel);
            newClassName = EditorGUILayout.TextField("Class name", newClassName);

            if (GUILayout.Button("Ok"))
            {
                string path = AssetDatabase.GetAssetPath(Selection.activeObject);
                if (path == "")
                {
                    path = "Assets";
                }
                else if (Path.GetExtension(path) != "")
                {
                    path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
                }

                string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/" + newClassName + ".cs");


                //filling the class content
                List<string> contents = new List<string>();

                contents.Add("using UnityEngine;");
                contents.Add("using System.Collections;");
                contents.Add("using EZ_Core;");

                contents.Add("");

                contents.Add("public class " + newClassName + " : ActorPooled");
                contents.Add("{");

                contents.Add("    //Place to add your custom variable.");
                contents.Add("");
                contents.Add("");

                //Initialize
                contents.Add("    //Initialize will be called when the object is created by the pool manager.");
                contents.Add("    //It will not be called during OnSpawned or in Unity's Awake / Start methods.");
                contents.Add("    public override void Initialize()");
                contents.Add("    {");
                contents.Add("        base.Initialize(); //Initializes base class variables. Do not forget to call this !");
                contents.Add("");
                contents.Add("        //Initialize your custom variable here");
                contents.Add("    }");
                contents.Add("");

                //OnSpawned
                contents.Add("    public override void OnSpawned()");
                contents.Add("    {");
                contents.Add("        base.OnSpawned();");
                contents.Add("    }");
                contents.Add("");

                //OnDespawned
                contents.Add("    public override void OnDespawned()");
                contents.Add("    {");
                contents.Add("        base.OnDespawned();");
                contents.Add("    }");


                contents.Add("}");

                try
                {
                    File.WriteAllLines(assetPathAndName, contents.ToArray());
                }
                catch (System.Exception ex)
                {
                    Debug.LogError("Error writing class at location : " + assetPathAndName + ". Error : " + ex.Message.ToString());
                }

                AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
            }
        }
    }
}