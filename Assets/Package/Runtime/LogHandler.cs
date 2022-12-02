using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace BroWar.Common
{
    public static class LogHandler
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Log(object message)
        {
            Log(message, LogType.Log);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Log(object message, LogType logType)
        {
            Log(message, logType, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Log(object message, LogType logType, Object context)
        {
            Debug.unityLogger.Log(logType, message, context);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void DebugLog(object message)
        {
            Log(message);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void DebugLog(object message, LogType logType)
        {
            Log(message, logType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void DebugLog(object message, LogType logType, Object context)
        {
            Log(message, logType, context);
        }
    }
}