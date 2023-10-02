using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

namespace EZ_Core
{
    public class TextureData
    {
        public Texture tex;
        public string path;

        public TextureData(Texture t, string s)
        {
            tex = t;
            path = s;
        }
    }

    /// <summary>
    /// class containing some common editor methods
    /// </summary>
    public static class EZ_EditorUtility
    {
        private static string assetName;

        public static string AssetName
        {
            get
            {
                return assetName;
            }
            set
            {
                assetName = value;
            }
        }

        /// <summary>
        /// method to draw a texture in the editor inspector
        /// </summary>
        public static void DrawTexture(Texture tex)
        {
            if (tex == null)
            {
                Debug.LogWarning("GUI texture is missing !");
                return;
            }

            Rect rect = GUILayoutUtility.GetRect(0f, 0f);
            rect.width = tex.width;
            rect.height = tex.height;
            GUILayout.Space(rect.height);
            GUI.DrawTexture(rect, tex);
        }

        public static void DrawTexture(TextureData texData)
        {
            DrawTexture(LoadTexture(texData));
        }

        public static Texture LoadTexture(TextureData texData)
        {
            if (texData.tex == null)
            {
                if (!Directory.Exists("Assets/Editor Default Resources/EZ_Core/" + assetName))
                {
                    Directory.CreateDirectory("Assets/Editor Default Resources/EZ_Core/" + assetName);
                }

                AssetDatabase.Refresh();

                FileInfo fInfo = new FileInfo("Assets/EZ_Core/" + assetName + "/Editor/Texture Resources/" + texData.path);
                fInfo.CopyTo("Assets/Editor Default Resources/EZ_Core/" + assetName + "/" + texData.path, true);

                AssetDatabase.Refresh();

                texData.tex = EditorGUIUtility.LoadRequired("EZ_Core/" + assetName + "/" + texData.path) as Texture;
            }

            return texData.tex;
        }

        public static void DrawTexture(Texture tex, float optionalWidth, float optionalHeight)
        {
            if (tex == null)
            {
                Debug.LogWarning("GUI texture is missing !");
                return;
            }

            Rect rect = GUILayoutUtility.GetRect(0f, 0f);
            rect.width = optionalWidth;
            rect.height = optionalHeight;
            GUILayout.Space(rect.height);
            GUI.DrawTexture(rect, tex);
        }

        private static Color oldColor;

        public static void BeginColor(Color col)
        {
            oldColor = GUI.color;
            GUI.color = col;
        }

        public static void EndColor()
        {
            GUI.color = oldColor;
        }
    }
}