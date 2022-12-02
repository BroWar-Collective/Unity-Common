using System;
using System.Reflection;
using System.Text;

namespace BroWar.Common.Utilities
{
    public static class ReflectionUtility
    {
        public static string GetMethodDefinitionString(MethodInfo method)
        {
            var sb = new StringBuilder();
            sb.Append(method.Name);
            var parameters = method.GetParameters();
            foreach (var param in parameters)
            {
                var paramType = param.ParameterType;
                sb.Append($" <color=\"white\">{TypeToSimpleTypeString(paramType)}</color>");
            }

            return sb.ToString();
        }

        private static string TypeToSimpleTypeString(Type type)
        {
            switch (type.Name)
            {
                case "Int32":
                    return "int";
                case "Single":
                    return "float";
                case "Double":
                    return "double";
                case "Boolean":
                    return "bool";
                case "String":
                    return "string";
                default:
                    return type.Name;
            }
        }
    }
}