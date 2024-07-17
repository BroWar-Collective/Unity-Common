using UnityEditor;
using UnityEngine;

namespace BroWar.Common.Editor
{
    public static class SerializedPropertyUtility
    {
        public static int ComparePropertyTo(SerializedProperty a, SerializedProperty b)
        {
            if (a == null || b == null)
            {
                return 0;
            }

            if (a.propertyType != b.propertyType)
            {
                LogHandler.Log($"[Common][Editor] Couldn't compare 2 {nameof(SerializedProperty)}/ies of different types.", LogType.Error);
                return 0;
            }

            switch (a.propertyType)
            {
                case SerializedPropertyType.Boolean:
                    return a.boolValue.CompareTo(b.boolValue);
                case SerializedPropertyType.Float:
                    return a.floatValue.CompareTo(b.floatValue);
                case SerializedPropertyType.Integer:
                    return a.intValue.CompareTo(b.intValue);
                case SerializedPropertyType.String:
                    return a.stringValue.CompareTo(b.stringValue);
                case SerializedPropertyType.Enum:
                    return a.enumValueIndex.CompareTo(b.enumValueIndex);
                case SerializedPropertyType.ObjectReference:
                    {
                        var aReference = a.objectReferenceValue;
                        var bReference = b.objectReferenceValue;
                        if (aReference == null && bReference == null)
                        {
                            return 0;
                        }

                        if (aReference == null)
                        {
                            return 1;
                        }

                        if (bReference == null)
                        {
                            return -1;
                        }

                        return aReference.name.CompareTo(bReference.name);
                    }
            }

            return 0;
        }
    }
}