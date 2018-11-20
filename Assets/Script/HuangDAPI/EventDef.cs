using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace HuangDAPI
{

    public class EventDef : ReflectBase
    {
        public Func<bool> _funcPrecondition;
        public Func<string> _funcTitle;
        public Func<string> _funcContent;
        public List<Tuple<string, Action>> listOptions;

        public string[] options = { "opt1" };

        public EventDef()
        {
            title = StreamManager.UIDesc.Get(this.GetType().Name + "_TITLE");
            content = StreamManager.UIDesc.Get(this.GetType().Name + "_CONTENT");

            _funcPrecondition = GetDelegate<Func<bool>>("Precondition",
                                                          () =>
                                                          {
                                                              return false;
                                                          });
            _funcTitle = GetDelegate<Func<string>>("Title",
                                                () =>
                                                {
                                                    FieldInfo field = _subFields.Where(x => x.Name == "title").First();
                                                    return (string)field.GetValue(this);
                                                });

            _funcContent = GetDelegate<Func<string>>("Content",
                                                            () =>
                                                            {
                                                                FieldInfo field = _subFields.Where(x => x.Name == "content").First();
                                                                return (string)field.GetValue(this);
                                                            });

            listOptions = new List<Tuple<string, Action>>();

            Type[] nestedTypes = this.GetType().GetNestedTypes(BindingFlags.NonPublic | BindingFlags.Public);

            for (int i = nestedTypes.Count() - 1; i >= 0; i--)
            {
                if (nestedTypes[i].BaseType != typeof(EventDef.Option))
                {
                    continue;
                }

                EventDef.Option option = (EventDef.Option)Activator.CreateInstance(nestedTypes[i]);
                option.Initialize(this);
                listOptions.Add(new Tuple<string, Action>(option._funcDesc(), option._funcOnSelect));
            }
        }

        public abstract class Option : ReflectBase
        {
            public Option()
            {
            }

            internal void Initialize(EventDef outter)
            {
                FieldInfo[] fields = _subFields.Where(x => x.Name == "OUTTER").ToArray();
                if (fields.Length != 0)
                {
                    fields.First().SetValue(this, outter);
                }

                desc = StreamManager.UIDesc.Get(outter.GetType().Name + "_" + this.GetType().Name + "_DESC");

                //_funcIsVisable = GetDelegateInSubEvent<DelegateIsVisable>("IsVisable",
                //                                                   () =>
                //                                                   {
                //                                                       return true;
                //                                                   });
                _funcDesc = GetDelegate<Func<string>>("Desc",
                                                                 () =>
                                                                 {
                                                                     FieldInfo field = _subFields.Where(x => x.Name == "desc").First();
                                                                     string Desc = (string)field.GetValue(this);
                                                                     return Desc;
                                                                 });
                _funcOnSelect = GetDelegate<Action>("OnSelect",
                                                                        () =>
                                                                        {
                                                                        });

                this.OUTTER = outter;

            }

            //public Decision AssocDecision
            //{
            //    get
            //    {
            //        return OUTTER.AssocDecision;
            //    }
            //}

            //public dynamic Decision
            //{
            //    get
            //    {
            //        return OUTTER.Decision;
            //    }
            //}

            public Func<bool> _funcIsVisable;
            public Func<string> _funcDesc;
            public Action _funcOnSelect;

            public dynamic OUTTER;
            protected string desc;

        }

        protected string title;
        protected string content;
    }
}
