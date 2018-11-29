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
        public static event Action<string, Decision.State> evtStateChange;

        public enum State
        {
            PUBLISH_ENABLE,
            PUBLISH_ING,
            PUBLISH_ED,
            CANCEL_ENABLE,
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
            if(_currState == State.PUBLISH_ED)
            {
                _currState = State.CANCEL_ENABLE;

                evtStateChange(name, _currState);
            }
        }

        public void OnPublish()
        {
            if(_currState != State.PUBLISH_ENABLE)
            {
                throw new ArgumentException();
            }

            if(_isEnableCancel)
            {
                _currState = State.PUBLISH_ED;
            }
            else
            {
                _currState = State.CANCEL_ENABLE;
            }

            HuangDAPI.Affect.Start(_name);

            evtStateChange(name, _currState);
        }

        public void OnCancel()
        {
            if (_currState != State.CANCEL_ENABLE)
            {
                throw new ArgumentException();
            }

            HuangDAPI.Affect.End(_name);

            GMData.Inist.decisions.Del(this);
        }

        [JsonProperty]
        private string _name;

        [JsonProperty]
        public State _currState = State.PUBLISH_ENABLE;

        [JsonProperty]
        public bool _isEnableCancel = false;

        internal void OnDeserialized()
        {
            evtStateChange(name, _currState);
        }
    }


    [JsonObject(MemberSerialization.OptIn)]
    public class Decisions
    {
        public static event Action<Decision> evtAdd;
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
            if (evtAdd != null)
            {
                evtAdd(decision);
            }

        }

        public void Del(Decision decision)
        {
            _list.Remove(decision);
            if (evtDel != null)
            {
                evtDel(decision.name);
            }
        }

        public Decision[] All
        {
            get
            {
                return _list.ToArray();
            }
        }

        public void OnPublish(string name)
        {
            Debug.Log("OnPublish" + name);
            var decision = _list.Find((Decision obj) => obj.name == name);
            if (decision == null)
            {
                throw new ArgumentException();
            }

            decision.OnPublish();
        }

        public void OnCancel(string name)
        {
            var decision = _list.Find((Decision obj) => obj.name == name);
            if (decision == null)
            {
                throw new ArgumentException();
            }

            decision.OnCancel();
        }

        [JsonProperty]
        private List<Decision> _list = new List<Decision>();

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            if (evtAdd == null)
            {
                return;
            }

            foreach (var decision in _list)
            {
                evtAdd(decision);
                decision.OnDeserialized();
            }
        }


    }
}
