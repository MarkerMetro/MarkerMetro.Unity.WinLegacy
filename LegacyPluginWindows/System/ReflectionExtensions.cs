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
            return type.GetTypeInfo().IsClass;
        }

        public static bool IsEnum(this Type type)
        {
            return type.GetTypeInfo().IsEnum;
        }

        public static Type GetInterface(this Type type, string name)
        {
            return type.GetTypeInfo().ImplementedInterfaces.FirstOrDefault(t => t.Name == name);
        }

        public static PropertyInfo[] GetProperties(this Type type)
        {
            return type.GetTypeInfo().DeclaredProperties != null ? type.GetTypeInfo().DeclaredProperties.ToArray() : null;
        }

        public static MethodInfo[] GetMethods(this Type t, BindingFlags flags)
        {
            if (!flags.HasFlag(BindingFlags.Instance) && !flags.HasFlag(BindingFlags.Static)) return null;

            var ti = t.GetTypeInfo();
            var allMethods = ti.DeclaredMethods;
            var resultList = new List<MethodInfo>();
            foreach (var method in allMethods)
            {
                var isValid = (flags.HasFlag(BindingFlags.Public) && method.IsPublic)
                    || (flags.HasFlag(BindingFlags.NonPublic) && !method.IsPublic);
                isValid &= (flags.HasFlag(BindingFlags.Static) && method.IsStatic) || (flags.HasFlag(BindingFlags.Instance) && !method.IsStatic);
                if (flags.HasFlag(BindingFlags.DeclaredOnly))
                    isValid &= method.DeclaringType == t;

                if (isValid)
                    resultList.Add(method);
            }
            return resultList.ToArray();
        }

        public static MemberInfo[] GetMembers(this Type t, BindingFlags flags)
        {
            if (!flags.HasFlag(BindingFlags.Instance) && !flags.HasFlag(BindingFlags.Static)) return null;

            var ti = t.GetTypeInfo();
            var result = new List<MemberInfo>();
            result.AddRange(ti.DeclaredMembers);
            return result.ToArray();
        }

        public static Object InvokeMember(this Type t, string name, BindingFlags flags, object binder, object target, object[] args)
        {
            if (binder != null || target != null)
                throw new ArgumentException("doesn't support binder or target when invoking");
            // We only support invoking a normal method, not a field/property/other member
            var ti = t.GetTypeInfo();
            foreach (var m in ti.DeclaredMethods)
            {
                if (m.Name.Equals(name))
                {
                    return m.Invoke(t, args);
                }
            }
            return null;
        }

        public static FieldInfo[] GetFields(this Type type)
        {
            return GetFields(type, BindingFlags.Default);
        }

        public static FieldInfo[] GetFields(this Type t, BindingFlags flags)
        {
            if (!flags.HasFlag(BindingFlags.Instance) && !flags.HasFlag(BindingFlags.Static)) return null;

            var ti = t.GetTypeInfo();
            var origFields = ti.DeclaredFields;
            var results = new List<FieldInfo>();
            foreach (var field in origFields)
            {
                var isValid = (flags.HasFlag(BindingFlags.Public) && field.IsPublic)
                    || (flags.HasFlag(BindingFlags.NonPublic) && !field.IsPublic);
                isValid &= (flags.HasFlag(BindingFlags.Static) && field.IsStatic) || (flags.HasFlag(BindingFlags.Instance) && !field.IsStatic);
                if (flags.HasFlag(BindingFlags.DeclaredOnly))
                    isValid &= field.DeclaringType == t;

                results.Add(field);
            }
            return results.ToArray();
        }

        public static MethodInfo GetMethod(this Type type, string name)
        {
            return GetMethod(type, name, BindingFlags.Default, null);
        }

        public static MethodInfo GetMethod(this Type type, string name, Type[] types)
        {
            return GetMethod(type, name, BindingFlags.Default, types);
        }

        public static MethodInfo GetMethod(this Type t, string name, BindingFlags flags)
        {
            return GetMethod(t, name, flags, null);
        }

        public static MethodInfo GetMethod(Type t, string name, BindingFlags flags, Type[] parameters)
        {
            var ti = t.GetTypeInfo();
            var methods = ti.GetDeclaredMethods(name);
            foreach (var m in methods)
            {
                var plist = m.GetParameters();
                bool match = true;
                foreach (var param in plist)
                {
                    bool valid = true;
                    if (parameters != null)
                    {
                        foreach (var ptype in parameters)
                            valid &= ptype == param.ParameterType;
                    }
                    match &= valid;
                }
                if (match)
                    return m;
            }
            return null;

        }

        public static Type[] GetGenericArguments(this Type t)
        {
            var ti = t.GetTypeInfo();
            return ti.GenericTypeArguments;
        }

        public static bool IsAssignableFrom(this Type current, Type toCompare)
        {
            return current.GetTypeInfo().IsAssignableFrom(toCompare.GetTypeInfo());
        }

        public static bool IsPrimitive(this Type current)
        {
            if (current == typeof(Boolean)) return true;
            if (current == typeof(Byte)) return true;
            if (current == typeof(SByte)) return true;
            if (current == typeof(Int16)) return true;
            if (current == typeof(UInt16)) return true;
            if (current == typeof(Int32)) return true;
            if (current == typeof(UInt32)) return true;
            if (current == typeof(Int64)) return true;
            if (current == typeof(UInt64)) return true;
            if (current == typeof(IntPtr)) return true;
            if (current == typeof(UIntPtr)) return true;
            if (current == typeof(Char)) return true;
            if (current == typeof(Double)) return true;
            if (current == typeof(Single)) return true;
            return false;
        }

        /**
         * Missing IsSubclassOf, this works well
         */
        public static bool IsSubclassOf(this System.Type type, System.Type parent)
        {
            return parent.GetTypeInfo().IsAssignableFrom(type.GetTypeInfo());
        }

    }
}