using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Dynamic;

namespace GMDATA
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Decision
    {
        public enum State
        {
            PUBLISH_ENABLE,
            PUBLISH_ING,
            PUBLISH_ED,
        }

        public Decision()
        {

        }

        public Decision(string name)
        {
            this._name = name;
        }

        public dynamic Info()
        {
            dynamic rslt = new ExpandoObject();

            var dict = (IDictionary<string, object>)rslt;
            dict.Add("name", name);
            dict.Add("state", _currState);
            dict.Add("enalecancel", _isEnableCancel);

            return rslt;
        }

        public string name
        {
            get
            {
                return _name;
            }
        }

        public void EnableCancel(bool value)
        {
            _isEnableCancel = value;
        }

        public void OnPublish()
        {
            if(_currState != State.PUBLISH_ENABLE)
            {
                throw new ArgumentException();
            }

            _currState = State.PUBLISH_ED;
        }

        public void OnCancel()
        {
            if (_currState != State.PUBLISH_ED)
            {
                throw new ArgumentException();
            }

            GMData.Inist.decisions.Del(this);
        }

        [JsonProperty]
        private string _name;

        [JsonProperty]
        public State _currState = State.PUBLISH_ENABLE;

        [JsonProperty]
        public bool _isEnableCancel = false;
    }


    [JsonObject(MemberSerialization.OptIn)]
    public class Decisions
    {
        public static event Action<Func<dynamic>> evtAdd;
        public static event Action<string> evtDel;

        public void EnablePublish(string name)
        {
            var decision = _list.Find((Decision obj) => obj.name == name);
            if(decision == null)
            {
                Add(new Decision(name));
            }
        }

        public void EnableCancel(string name)
        {
            var decision = _list.Find((Decision obj) => obj.name == name);
            if (decision == null)
            {
                return;
            }

            decision.EnableCancel(true);
        }

        public void UnableCancel(string name)
        {
            var decision = _list.Find((Decision obj) => obj.name == name);
            if (decision == null)
            {
                return;
            }

            decision.EnableCancel(false);
        }

        public void Add(Decision decision)
        {
            _list.Add(decision);
            if (evtAddProv != null)
            {
                evtAdd(decision.Info);
            }

        }

        public void Del(Decision decision)
        {
            _list.Remove(decision);
            evtDel(decision.name);
        }

        public Decision[] All
        {
            get
            {
                return _list.ToArray();
            }
        }


        [JsonProperty]
        private List<Decision> _list = new List<Decision>();
        private object evtAddProv;

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            if (evtAddProv == null)
            {
                return;
            }

            foreach (var prov in _list)
            {
                evtAdd(prov.Info);
            }
        }
    }
}
