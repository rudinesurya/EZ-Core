#define DEBUG_LEVEL_LOG
#define DEBUG_LEVEL_WARN
#define DEBUG_LEVEL_ERROR
#define SYSTEM_CHECK

using UnityEngine;
using System.Collections;

namespace EZ_Core
{
    public class SystemCheck
    {
        [System.Diagnostics.Conditional("SYSTEM_CHECK")]
        public static void Log(object message)
        {
            Debug.Log(message);
        }
    }

    public class DebugClass
    {
        [System.Diagnostics.Conditional("DEBUG_LEVEL_LOG")]
        [System.Diagnostics.Conditional("DEBUG_LEVEL_WARN")]
        [System.Diagnostics.Conditional("DEBUG_LEVEL_ERROR")]
        public static void Log(object message)
        {
            Debug.Log(message);
        }

        [System.Diagnostics.Conditional("DEBUG_LEVEL_LOG")]
        [System.Diagnostics.Conditional("DEBUG_LEVEL_WARN")]
        [System.Diagnostics.Conditional("DEBUG_LEVEL_ERROR")]
        public static void Log(object message, Object context)
        {
            Debug.Log(message, context);
        }

        [System.Diagnostics.Conditional("DEBUG_LEVEL_WARN")]
        [System.Diagnostics.Conditional("DEBUG_LEVEL_ERROR")]
        public static void LogWarning(object message)
        {
            Debug.LogWarning(message);
        }

        [System.Diagnostics.Conditional("DEBUG_LEVEL_WARN")]
        [System.Diagnostics.Conditional("DEBUG_LEVEL_ERROR")]
        public static void LogWarning(object message, Object context)
        {
            Debug.LogWarning(message, context);
        }

        [System.Diagnostics.Conditional("DEBUG_LEVEL_ERROR")]
        public static void LogError(object message)
        {
            Debug.LogError(message);
        }

        [System.Diagnostics.Conditional("DEBUG_LEVEL_ERROR")]
        public static void LogError(object message, Object context)
        {
            Debug.LogError(message, context);
        }

        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        [System.Diagnostics.Conditional("DEBUG_LEVEL_ERROR")]
        public static void Assert(bool condition, string assertString, bool pauseOnFail)
        {
            if (!condition)
            {
                Debug.LogError("Assert failed! " + assertString);

                if (pauseOnFail)
                    Debug.Break();
            }
        }
    }
}