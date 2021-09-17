﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;


namespace Core.EF.Application.Commom
{
    public static class ReflectionHelper
    {
        public static List<Type> GetTypeListBaseOn<T>()
        {
            List<Type> result = new List<Type>();

            Type baseOnType = typeof(T);
            Assembly assembly = Assembly.GetAssembly(baseOnType);
            if (baseOnType.IsInterface)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.IsAbstract || type.IsInterface)
                        continue;

                    if (type.GetInterface(baseOnType.Name) != null)
                    {
                            result.Add(type);
                    }
                }
            }
            else
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.IsAbstract || type.IsInterface)
                        continue;

                    if (type.IsSubclassOf(baseOnType))
                        result.Add(type);
                }
            }

            return result;
        }

        public static List<AttributeT> GetTypeAttributeList<AttributeT>(Type type)
        {
            List<AttributeT> list = new List<AttributeT>();
            object[] attributes = type.GetCustomAttributes(typeof(AttributeT), true);
            if (attributes.Length > 0)
            {
                foreach (var item in attributes)
                {
                    if (item is AttributeT)
                    {
                        AttributeT attribute = (AttributeT)item;
                        if (attribute != null)
                            list.Add(attribute);
                    }
                }
            }

            return list;
        }

        public static List<FieldInfo> GetAllFieldInfo(Type targetType)
        {
            List<FieldInfo> allFieldInfo = new List<FieldInfo>();
            if (targetType == null)
                return allFieldInfo;

            allFieldInfo.AddRange(targetType.GetFields().ToList());

            if (targetType.BaseType != null)
                allFieldInfo.AddRange(GetAllFieldInfo(targetType.BaseType));

            return allFieldInfo;
        }

        public static object Clone(object targetObject)
        {
            if (targetObject == null)
                return null;

            Type targetType = targetObject.GetType();

            if (targetType.IsValueType)
                return targetObject;

            if (targetType == String.Empty.GetType())
                return targetObject;

            object cloneObject = Activator.CreateInstance(targetType);

            Type targetlistType = targetType.GetInterface("IList");
            if (targetlistType != null)
            {
                IList list = (IList)targetObject;
                IList cloneList = (IList)cloneObject;
                foreach (var item in list)
                {
                    cloneList.Add(Clone(item));
                }
            }
            else
            {

                List<PropertyInfo> propertyList = targetType.GetProperties().ToList();

                foreach (PropertyInfo property in propertyList)
                {
                    if (property.CanWrite == false)
                        continue;

                    List<ParameterInfo> parameterInfoList = property.GetIndexParameters().ToList();
                    if (parameterInfoList.Count == 0)
                    {
                        object propertyValue = property.GetValue(targetObject, null);
                        object clonePropertyValue = Clone(propertyValue);
                        property.SetValue(cloneObject, clonePropertyValue, null);
                    }
                    //else
                    //{
                    //    foreach (ParameterInfo item in parameterInfoList)
                    //    {

                    //    }
                    //}
                }
            }

            return cloneObject;
        }

        /// <summary>
        /// 把 sourceObject 对象里的 property 值拷贝到 targetObject 中
        /// 不要求 targetObject 和 sourceObject 是同一类型
        /// 也不要求 property 完全一样，targetObject 中的 property 可以是 sourceObject 的子集
        /// </summary>
        /// <param name="targetObject"></param>
        /// <param name="sourceObject"></param>
        public static void Inject(object targetObject, object sourceObject)
        {
            Inject(targetObject, sourceObject, null);
        }

        /// <summary>
        /// 把 sourceObject 对象里的 property 值拷贝到 targetObject 中
        /// 不要求 targetObject 和 sourceObject 是同一类型
        /// 也不要求 property 完全一样，targetObject 中的 property 可以是 sourceObject 的子集
        /// excludeProperties 指示忽略不注入的 targetObject 中的属性
        /// </summary>
        /// <param name="targetObject"></param>
        /// <param name="sourceObject"></param>
        public static void Inject(object targetObject, object sourceObject, List<string> excludeProperties)
        {
            if (targetObject == null || sourceObject == null)
                return;

            Type targetType = targetObject.GetType();
            Type sourceType = sourceObject.GetType();

            foreach (PropertyInfo targetObjProperty in targetType.GetProperties())
            {
                PropertyInfo sourceObjProperty = sourceType.GetProperty(targetObjProperty.Name);
                if (sourceObjProperty == null)
                    continue;

                if (excludeProperties != null && excludeProperties.Contains(sourceObjProperty.Name))
                    continue;

                if (targetObjProperty.CanWrite == false)
                    continue;

                targetObjProperty.FastSetValue(targetObject, sourceObjProperty.FastGetValue(sourceObject));
            }
        }

        public static object GetPropertyValue_old(object obj, string propertyName)
        {
            if (obj == null)
                throw new ArgumentNullException("GetPropertyValue 没有指定 obj");

            if (String.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException("GetPropertyValue 没有指定 propertyName");

            Type objType = obj.GetType();
            PropertyInfo propertyInfo = objType.GetProperty(propertyName);

            if (propertyInfo == null)
                throw new ArgumentNullException("GetPropertyValue 指定的 propertyName 不存在");

            return propertyInfo.FastGetValue(obj);

        }

        public static object GetPropertyValue(object obj, string propertyName)
        {
            if (obj == null)
                throw new ArgumentNullException("GetPropertyValue 没有指定 obj");

            if (String.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException("GetPropertyValue 没有指定 propertyName");

            string [] propertyNameList = propertyName.Split('.');

            foreach (var propertyItem in propertyNameList)
            {
                Type objType = obj.GetType();
                PropertyInfo propertyInfo = objType.GetProperty(propertyItem);

                if (propertyInfo == null)
                    throw new ArgumentNullException("GetPropertyValue 指定的 propertyItem 不存在：" + propertyItem);

                obj = propertyInfo.FastGetValue(obj);

                if (obj == null)
                    return null;

            }

            return obj;
        }

        public static void SetPropertyValue(object obj, string propertyName, object value)
        {
            if (obj == null)
                throw new ArgumentNullException("GetPropertyValue 没有指定 obj");

            if (String.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException("GetPropertyValue 没有指定 propertyName");

            Type objType = obj.GetType();
            PropertyInfo propertyInfo = objType.GetProperty(propertyName);

            //if (value != null && value.ToString() != String.Empty)
            //    propertyInfo.FastSetValue(obj, value);

            if (value != null && value.ToString() != String.Empty)
            {
                string strValue = value.ToString();
                if (propertyInfo.PropertyType == typeof(Int32) || propertyInfo.PropertyType == typeof(Int32?))
                {
                    propertyInfo.FastSetValue(obj, Int32.Parse(strValue));
                }
                else if (propertyInfo.PropertyType == typeof(float) || propertyInfo.PropertyType == typeof(float?))
                {
                    propertyInfo.FastSetValue(obj, float.Parse(strValue));
                }
                else if (propertyInfo.PropertyType == typeof(decimal) || propertyInfo.PropertyType == typeof(decimal?))
                {
                    propertyInfo.FastSetValue(obj, decimal.Parse(strValue));
                }
                else if (propertyInfo.PropertyType == typeof(DateTime) || propertyInfo.PropertyType == typeof(DateTime?))
                {
                    propertyInfo.FastSetValue(obj, DateTime.Parse(strValue));
                }
                else if (propertyInfo.PropertyType == typeof(bool) || propertyInfo.PropertyType == typeof(bool?))
                {
                    propertyInfo.FastSetValue(obj, Boolean.Parse(strValue));
                }
                else
                {
                    propertyInfo.FastSetValue(obj, value);
                }
            }

        }
    }
}
