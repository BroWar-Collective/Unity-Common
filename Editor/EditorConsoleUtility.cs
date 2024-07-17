using System.Reflection;

namespace BroWar.Common.Editor
{
    public static class EditorConsoleUtility
    {
        //NOTE: more information about the ConsoleWindow implementation:
        //https://github.com/Unity-Technologies/UnityCsReference/blob/master/Editor/Mono/ConsoleWindow.cs

        private const int clearConsoleOnBuildFlag = 2048;

        private static readonly MethodInfo hasFlagMethodInfo =
            EditorReflectionUtility.GetEditorMethod("UnityEditor.ConsoleWindow", "HasFlag",
                BindingFlags.NonPublic | BindingFlags.Static);
        private static readonly MethodInfo setFlagMethodInfo =
            EditorReflectionUtility.GetEditorMethod("UnityEditor.ConsoleWindow", "SetFlag",
                BindingFlags.NonPublic | BindingFlags.Static);

        public static bool HasFlag(int flags)
        {
            return hasFlagMethodInfo != null && (bool)hasFlagMethodInfo.Invoke(null, new object[] { flags });
        }

        public static bool HasClearConsoleOnBuildFlag()
        {
            return HasFlag(clearConsoleOnBuildFlag);
        }

        public static void SetFlag(int flags, bool value)
        {
            setFlagMethodInfo?.Invoke(null, new object[] { flags, value });
        }

        public static void SetClearConsoleOnBuildFlag(bool value)
        {
            SetFlag(clearConsoleOnBuildFlag, value);
        }
    }
}