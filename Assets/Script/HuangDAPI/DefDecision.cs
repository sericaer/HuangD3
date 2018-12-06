using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
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
        public readonly Action _funcInitData;

        public DefDecision()
        {
            
            Debug.Log(this.GetType().Name);
            affect = new Affect(this);

            _funcTitle = GetDelegate<Func<string>>("Title",
                                    () =>
                                    {
                                        return string.Format("DEC_{0}_TITLE", this.GetType().Name);
                                    });

            _funcDescribe = GetDelegate<Func<string>>("Describe",
                                                 () => {
                                                     return string.Format("DEC_{0}_DESC", this.GetType().Name);
                                                 });

            _funcEnablePublish = GetDelegate<Func<bool>>("EnablePublish");
            _funcEnableCancel  = GetDelegate<Func<bool>>("EnableCancel");
            _funcInitData = GetDelegate<Action>("InitData");
            if(_funcInitData != null)
            {
                _funcInitData();
            }


            _dict.Add(this.GetType().Name, this);
        }

        public void OnPublish()
        {
            GMDATA.GMData.Inist.decisions.OnPublish(this.GetType().Name);
        }

        public bool isPublished
        {
            get
            {
                var decision = GMDATA.GMData.Inist.decisions.All.Where(x => x.name == this.GetType().Name).SingleOrDefault();
                if(decision == null)
                {
                    return false;
                }
                return (decision._currState == GMDATA.Decision.State.PUBLISH_ED 
                        || decision._currState == GMDATA.Decision.State.CANCEL_ENABLE);
            }

        }

        public dynamic data
        {
            get
            {
                if(GMDATA.GMData.Inist == null)
                {
                    return dataloacl;
                }

                var rslt = GMDATA.GMData.Inist.decisions.All.Where(x => x.name == this.GetType().Name).SingleOrDefault();
                if(rslt == null)
                {
                    return dataloacl;
                }
                return rslt.data;
            }
            set
            {
                dataloacl = value;
                GMDATA.GMData.Inist.decisions.All.Where(x => x.name == this.GetType().Name).Single().data = value;
            }
        }

        private static Dictionary<string, DefDecision> _dict = new Dictionary<string, DefDecision>();

        protected dynamic dataloacl = new ExpandoObject();

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
