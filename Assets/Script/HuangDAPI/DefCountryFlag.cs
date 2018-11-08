using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace HuangDAPI
{
    public class DefCountryFlag
    {
        public static DefCountryFlag Find(string name)
        {
            return _dict[name];
        }

        public static event Action<string> evtEnable;
        public static event Action<string> evtDisable;
        public readonly Affect affect;

        public DefCountryFlag()
        {
            Debug.Log(this.GetType().Name);
            affect = new Affect(this);

            _dict.Add(this.GetType().Name, this);
        }

        public void Enable()
        {
            evtEnable(this.GetType().Name);
        }

        public void Disable()
        {
            evtDisable(this.GetType().Name);
        }

        private static Dictionary<string, DefCountryFlag> _dict = new Dictionary<string, DefCountryFlag>();
    }

    public class Affect
    {
        public Func<int, int> EmperorHeath = null;
        public Func<double, double> CountryTax = null;
        public Func<double, double> CountryReb = null;

        public Affect(object outter)
        {
            _outter = outter;
            EmperorHeath = AssocAffect<int, int>("affectEmperorHeath");
            CountryTax = AssocAffect<double, double>("affectCountryTax");
            CountryReb = AssocAffect<double, double>("affectCountryReb");
        }

        private Func<T, TResult> AssocAffect<T, TResult>(string methodName)
        {
            MethodInfo method = _outter.GetType().GetMethod(methodName);
            if (method != null)
            {
                return (Func<T, TResult>)(object)Delegate.CreateDelegate(typeof(Func<T, TResult>), _outter, method);
            }

            return null;
        }

        object _outter;
    }
}
