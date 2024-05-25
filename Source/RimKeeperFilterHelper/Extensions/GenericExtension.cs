using System;
using System.Reflection;
using Verse;

namespace Keepercraft.RimKeeperFilterHelper.Extensions
{
    public static class GenericExtension
    {
        public static void SetPrivateField(this object obj, string fieldName, object value)
        {
            Type type = obj.GetType();
            FieldInfo fieldInfo = type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);

            if (fieldInfo != null)
            {
                fieldInfo.SetValue(obj, value);
            }
            else
            {
                Log.Error("[RimKeeperFilterHelper] SetPrivateField:" + fieldName);
            }
        }

        public static void SetPrivateProperty(this object obj, string fieldName, object value)
        {
            Type type = obj.GetType();
            PropertyInfo fieldInfo = type.GetProperty(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);

            if (fieldInfo != null)
            {
                fieldInfo.SetValue(obj, value);
            }
            else
            {
                Log.Error("[RimKeeperFilterHelper] SetPrivateProperty:" + fieldName);
            }
        }

        public static void SetPrivateStaticField(this object obj, string fieldName, object value)
        {
            Type type = obj.GetType();
            FieldInfo fieldInfo = type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static);

            if (fieldInfo != null)
            {
                fieldInfo.SetValue(obj, value);
            }
            else
            {
                Log.Error("[RimKeeperFilterHelper] SetPrivateStaticField:" + fieldName);
            }
        }

        public static T GetPrivateField<T>(this object obj, string fieldName)
        {
            Type type = obj.GetType();
            FieldInfo fieldInfo = type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);

            if (fieldInfo != null)
            {
                return (T)fieldInfo.GetValue(obj);
            }
            else
            {
                Log.Error("[RimKeeperFilterHelper] GetPrivateField:" + fieldName);
                return default;
            }
        }

        public static T GetPrivateStaticField<T>(this object obj, string fieldName)
        {
            Type type = obj.GetType();
            FieldInfo fieldInfo = type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static);

            if (fieldInfo != null)
            {
                return (T)fieldInfo.GetValue(obj);
            }
            else
            {
                Log.Error("[RimKeeperFilterHelper] GetPrivateStaticField:" + fieldName);
                return default;
            }
        }

        public static T GetPrivateProperty<T>(this object obj, string fieldName)
        {
            Type type = obj.GetType();
            PropertyInfo fieldInfo = type.GetProperty(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);

            if (fieldInfo != null)
            {
                return (T)fieldInfo.GetValue(obj);
            }
            else
            {
                Log.Error("[RimKeeperFilterHelper] GetPrivateProperty:" + fieldName);
                return default;
            }
        }

        public static T GetPrivateStaticProperty<T>(this object obj, string fieldName)
        {
            Type type = obj.GetType();
            PropertyInfo fieldInfo = type.GetProperty(fieldName, BindingFlags.NonPublic | BindingFlags.Static);

            if (fieldInfo != null)
            {
                return (T)fieldInfo.GetValue(obj);
            }
            else
            {
                Log.Error("[RimKeeperFilterHelper] GetPrivateProperty:" + fieldName);
                return default;
            }
        }

        public static T GetPrivateMethod<T>(this object obj, string methodName, params object[] methodParams)
        {
            Type type = obj.GetType();

            MethodInfo methodInfo = type.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (methodInfo != null)
            {
                return (T)methodInfo.Invoke(obj, methodParams);
            }
            else
            {
                Log.Error("[RimKeeperFilterHelper] GetPrivateMethod:" + methodName);
                return default;
            }
        }
    }
}
