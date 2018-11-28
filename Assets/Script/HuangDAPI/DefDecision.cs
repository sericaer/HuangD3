using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace HuangDAPI
{
    public class DefDecision : ReflectBase
    {
        public static DefDecision Find(string name)
        {
            return _dict[name];
        }

        public static event Action<string> evtEnablePublish;
        public static event Action<string> evtEnableCancel;

        public readonly Affect affect;
        public readonly Func<string> _funcTitle;
        public readonly Func<string> _funcDescribe;
        public readonly Func<bool> _funcEnablePublish;
        public readonly Func<bool> _funcEnableCancel;

        public DefDecision()
        {
            
            Debug.Log(this.GetType().Name);
            affect = new Affect(this);

            _funcTitle = GetDelegate<Func<string>>("Title",
                                    () =>
                                    {
                                        FieldInfo field = _subFields.Where(x => x.Name == "title").First();
                                        return (string)field.GetValue(this);
                                    });

            _funcDescribe = GetDelegate<Func<string>>("Describe",
                                                 () => {
                                                     return string.Format("FLAG_{0}_DESC", this.GetType().Name);
                                                 });

            _funcEnablePublish = GetDelegate<Func<bool>>("EnablePublish");
            _funcEnableCancel  = GetDelegate<Func<bool>>("EnableCancel");

            _dict.Add(this.GetType().Name, this);
        }

        private static Dictionary<string, DefDecision> _dict = new Dictionary<string, DefDecision>();

        internal static void OnTimer()
        {
            foreach(var elem in _dict)
            {
                if (elem.Value._funcEnablePublish())
                {
                    Debug.Log(elem.Key + "enabled");
                    evtEnablePublish(elem.Key);
                }
                else if(elem.Value._funcEnableCancel())
                {
                    Debug.Log(elem.Key + "disabled");
                    evtEnableCancel(elem.Key);
                }
            }
        }
    }
}
