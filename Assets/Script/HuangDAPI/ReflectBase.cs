using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace HuangDAPI
{
    public class ReflectBase
    {
        public ReflectBase()
        {
            Type type = this.GetType();
            obj = this;

            _subFields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static).ToList();

            _subMethods = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static).ToList();
        }

        public ReflectBase(object objparam)
        {
            Type type = objparam.GetType();
            obj = objparam;

            _subFields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static).ToList();

            _subMethods = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static).ToList();
        }

        protected T GetDelegate<T>(string delegateName, T defaultValue = default(T))
        {
            IEnumerable<MethodInfo> methodIEnum = _subMethods.Where(x => x.Name == delegateName);
            if (methodIEnum.Count() == 0)
            {
                return defaultValue;

            }
            return (T)(object)Delegate.CreateDelegate(typeof(T), obj, methodIEnum.First());
        }

        protected List<FieldInfo> _subFields;
        protected List<MethodInfo> _subMethods;
        protected object obj;
    }
}