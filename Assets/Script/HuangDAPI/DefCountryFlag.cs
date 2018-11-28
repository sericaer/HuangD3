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

        }

        public void Disable()
        {
            if(evtDisable != null)
            {
                evtDisable(this.GetType().Name);
            }

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
        public Func<int, int> EmperorHeath = null;
        public Func<double, double> ProvinceTax = null;
        public Func<double, double> CountryReb = null;

        public Affect(object outter) : base(outter)
        {
            EmperorHeath = GetDelegate<Func<int, int>>("affectEmperorHeath");
            ProvinceTax = GetDelegate <Func<double, double>>("affectProvinceTax");
            CountryReb = GetDelegate <Func<double, double>>("affectCountryReb");
        }
    }
}
