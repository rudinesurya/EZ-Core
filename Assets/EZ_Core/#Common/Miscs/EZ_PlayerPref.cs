using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

//EZ_PlayerPrefEx offers what standard Unity PlayerPref doesnt provide.
//1. Bool
//2. Vector2
//3. Vector3

namespace EZ_Core
{
    public static class EZ_PlayerPrefEx
    {
#if UNITY_EDITOR

        [MenuItem("Window/EZ Core/EZ PlayerPrefEx/Delete All")]
        public static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }

#endif
        /// <summary>
        /// Return true if key exists
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool HasKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }

        /// <summary>
        /// Set the value of the preference identified by the key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetBool(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }

        /// <summary>
        /// Get the value of the preference corresponding to the key if it exists. Else it will return false.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetBool(string key)
        {
            return PlayerPrefs.GetInt(key) == 1;
        }

        /// <summary>
        /// Set the value of the preference identified by the key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }

        /// <summary>
        /// Get the value of the preference corresponding to the key if it exists. Else it will return its default value.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetInt(string key)
        {
            return PlayerPrefs.GetInt(key);
        }

        /// <summary>
        /// Set the value of the preference identified by the key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }

        /// <summary>
        /// Get the value of the preference corresponding to the key if it exists. Else it will return its default value.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static float GetFloat(string key)
        {
            return PlayerPrefs.GetFloat(key);
        }

        /// <summary>
        /// Set the value of the preference identified by the key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }

        /// <summary>
        /// Get the value of the preference corresponding to the key if it exists. Else it will return its default value.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetString(string key)
        {
            return PlayerPrefs.GetString(key);
        }

        /// <summary>
        /// Set the value of the preference identified by the key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetVector2(string key, Vector2 value)
        {
            PlayerPrefs.SetFloat(key + "$%x", value.x);
            PlayerPrefs.SetFloat(key + "$%y", value.y);
        }

        /// <summary>
        /// Get the value of the preference corresponding to the key if it exists. Else it will return its default value.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Vector2 GetVector2(string key)
        {
            float x = PlayerPrefs.GetFloat(key + "$%x");
            float y = PlayerPrefs.GetFloat(key + "$%y");

            return new Vector2(x, y);
        }

        /// <summary>
        /// Set the value of the preference identified by the key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetVector3(string key, Vector3 value)
        {
            PlayerPrefs.SetFloat(key + "$%x", value.x);
            PlayerPrefs.SetFloat(key + "$%y", value.y);
            PlayerPrefs.SetFloat(key + "$%z", value.z);
        }

        /// <summary>
        /// Get the value of the preference corresponding to the key if it exists. Else it will return its default value.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Vector3 GetVector3(string key)
        {
            float x = PlayerPrefs.GetFloat(key + "$%x");
            float y = PlayerPrefs.GetFloat(key + "$%y");
            float z = PlayerPrefs.GetFloat(key + "$%z");

            return new Vector3(x, y, z);
        }
    }
}