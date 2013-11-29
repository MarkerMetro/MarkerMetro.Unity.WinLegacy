using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LegacySystem.Reflection
{
    [Flags]
    public enum BindingFlags
    {
        Default,
        Public,
        Instance,
        InvokeMethod,
        NonPublic,
        Static,
        FlattenHierarchy,
        DeclaredOnly
    }

    public static class ReflectionExtensions
    {

        public static bool IsClass(this Type type)
        {
            return false;
        }

        public static bool IsEnum(this Type type)
        {
            return false;
        }

        public static Type GetInterface(this Type type, string name)
        {
            return null;
        }

        public static PropertyInfo[] GetProperties(this Type type)
        {
            return null;
        }

        public static MethodInfo[] GetMethods(this Type t, BindingFlags flags)
        {
            return null;
        }

        public static MemberInfo[] GetMembers(this Type t, BindingFlags flags)
        {
            return null;
        }

        public static Object InvokeMember(this Type t, string name, BindingFlags flags, object binder, object target, object[] args)
        {
            return null;
        }

        public static FieldInfo[] GetFields(this Type type)
        {
            return null;
        }

        public static FieldInfo[] GetFields(this Type t, BindingFlags flags)
        {
            return null;
        }

        public static MethodInfo GetMethod(this Type type, string name)
        {
            return null;
        }

        public static MethodInfo GetMethod(this Type type, string name, Type[] types)
        {
            return null;
        }

        public static MethodInfo GetMethod(this Type t, string name, BindingFlags flags)
        {
            return null;
        }

        public static MethodInfo GetMethod(Type t, string name, BindingFlags flags, Type[] parameters)
        {
            return null;

        }

        public static Type[] GetGenericArguments(this Type t)
        {
            return null;
        }

        public static bool IsAssignableFrom(this Type current, Type toCompare)
        {
            return false;
        }

        public static bool IsPrimitive(this Type current)
        {
            return false;
        }

        public static bool IsSubclassOf(this System.Type type, System.Type parent)
        {
            return false;
        }

    }
}