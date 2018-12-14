using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace HuangDAPI
{
    public class DefCountryFlag : ReflectBase
    {
        public static Func<string, bool> IsEnabled;

        public static DefCountryFlag Find(string name)
        {
            return _dict[name];
        }

        public static event Action<string> evtEnable;
        public static event Action<string> evtDisable;
        public readonly Affect affect;
        public readonly Func<string> describe;

        public DefCountryFlag()
        {
            Debug.Log(this.GetType().Name);
            affect = new Affect(this);
            describe = GetDelegate<Func<string>>("Describe",
                                                 () => {
                                                     return string.Format("FLAG_{0}_DESC", this.GetType().Name);
                                                 });

            _dict.Add(this.GetType().Name, this);
        }


        public void Enable()
        {
            if(evtEnable != null)
            {
                evtEnable(this.GetType().Name);
            }

            Affect.Start(this.GetType().Name);
        }

        public void Disable()
        {
            if(evtDisable != null)
            {
                evtDisable(this.GetType().Name);
            }

            Affect.End(this.GetType().Name);
        }

        public bool isEnabled
        {
            get
            {
                return IsEnabled(this.GetType().Name);
            }

        }

        private static Dictionary<string, DefCountryFlag> _dict = new Dictionary<string, DefCountryFlag>();
    }

    public class Affect : ReflectBase
    {
        public static event Action evtTaxChanged;

        public Func<int, int> EmperorHeath = null;
        public Func<double, double> ProvinceTax = null;
        public Func<double, double> SHIZTaxPercent = null;
        public Func<double, double> CountryReb = null;

        public Affect(object outter) : base(outter)
        {
            EmperorHeath = GetDelegate<Func<int, int>>("affectEmperorHeath");
            ProvinceTax = GetDelegate <Func<double, double>>("affectProvinceTax");
            CountryReb = GetDelegate <Func<double, double>>("affectCountryReb");
            SHIZTaxPercent = GetDelegate<Func<double, double>>("affectMaxTaxPercent");

            dict.Add(outter.GetType().Name, this);
        }

        internal static void Start(string name)
        {
            if(Started.ContainsKey(name))
            {
                return;
            }

            Started.Add(name, dict[name]);
            if(dict[name].ProvinceTax != null)
            {
                evtTaxChanged();
            }
        }

        internal static void End(string name)
        {
            Started.Remove(name);
        }

        public static Dictionary<string, Affect> Started = new Dictionary<string, Affect>();
        private static Dictionary<string, Affect> dict = new Dictionary<string, Affect>();


    }
}
