using System.Reflection;

namespace BroWar.Common.Editor
{
    public static class EditorReflectionUtility
    {
        private static readonly Assembly editorAssembly = typeof(UnityEditor.Editor).Assembly;

        public const BindingFlags allBindings = BindingFlags.Instance |
            BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public;

        public static MethodInfo GetEditorMethod(string classType, string methodName, BindingFlags falgs)
        {
            return editorAssembly.GetType(classType).GetMethod(methodName, falgs);
        }
    }
}