using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace GMDATA
{
    public class GMControll
    {
        public static void Init()
        {
            DialogLogic.evntCreate += Timer.Pause;
            DialogLogic.evntDestory += Timer.unPause;

            DecisionLogic.evtPublish += (string name) =>{
                GMData.Inist.decisions.OnPublish(name);
            };
            DecisionLogic.evtCancel += (string name) =>{
                GMData.Inist.decisions.OnCancel(name);
            };

            Decisions.evtDel += (string name)=>{
                DecisionGroup.Inst.DeleteElem(name);
            };

            Decisions.evtAdd += (Decision decision) =>{
                DecisionGroup.Task (()=>{
                    DecisionGroup.Inst.NewDecisionUI(decision);
                });
            };

            Decision.evtStateChange += (string name, Decision.State currState)=>{
                DecisionGroup.Task(() => {
                    DecisionGroup.Inst.StateChange(name, currState);
                });
            };


            CountryFlag.evtAddFlag += (string flagname) => {
                CountryStatusLogic.Task(() =>
                {
                    CountryStatusLogic.Inst.AddFlag(flagname);
                });
            };

            CountryFlag.evtDelFlag += (string flagname) => {
                CountryStatusLogic.Inst.DelFlag(flagname);
            };


            Relationship.evtOffice2PersonChange += (string office, string person) =>{
                OfficeCenterGroup1.Task(() => {
                    OfficeCenterGroup1.Inst.PersonOfficeChange(office, GMData.Inist.persons.Find(person));
                });
            };

            Relationship.evtPerson2FactionChange += (string person, string faction) =>{
                    PersonInOfficeLogic inst = PersonInOfficeLogic.Find(person);
                    inst.PersonFactionChange(GMData.Inist.persons.Find(person), faction);
            };

            Timer.evtOnTimer += ()=>{
                GMData.Inist.date.Increase();
            };
            Timer.evtOnTimer += StreamManager.EventManager.OnTimer;
            Timer.evtOnTimer += HuangDAPI.DefDecision.OnTimer;

            Timer.Register("DATE:*/1/2", () => {
                string desc = "";
                double value = 0;
                foreach (var elem in GMData.Inist.economy.funcIncomeDetail())
                {
                    desc += elem.Item1 + ": " + elem.Item2.ToString() + "\n";
                    value += elem.Item2;
                }

                desc += "TOTAL: " + value.ToString();

                var opts = new List<Tuple<string, Action>>();
                opts.Add(new Tuple<string, Action>("CONFIRM", () => { GMData.Inist.economy.current += (int)value; }));

                StreamManager.EventManager.AddEvent("TITLE_YEAR_INCOME_REPORT", desc, opts);
            });

            Timer.Register("DATE:*/*/1", () => {
                GMData.Inist.economy.current -= GMData.Inist.military.current;
            });

            Timer.Register("DATE:*/*/*", () => {
                if (GMData.Inist.emperor.heath <= 0)
                {
                    var opts = new List<Tuple<string, Action>>();
                    opts.Add(new Tuple<string, Action>("CONFIRM", () => {
                        Timer.Clear();

                        SceneManager.LoadSceneAsync("EndScene");
                    }));

                    StreamManager.EventManager.AddEvent("TITLE_EMPEROR_DIE", "CONTENT_TITLE_EMPEROR_DIE", opts);

                }
            });

            StreamManager.EventManager.evtNewGMEvent += MainScene.OnNewGMEvent;

            HuangDAPI.DefCountryFlag.evtEnable += (string name) =>{
                GMData.Inist.countryFlag.Add(name);
            };
            HuangDAPI.DefCountryFlag.evtDisable += (string name) =>{
                GMData.Inist.countryFlag.Del(name);
            };
            HuangDAPI.DefCountryFlag.IsEnabled += (string name) =>{
                return GMData.Inist.countryFlag.names.Contains(name);
            };
            HuangDAPI.DefDecision.evtEnablePublish += (string name) =>{
                GMData.Inist.decisions.EnablePublish(name);
            };
            HuangDAPI.DefDecision.evtEnableCancel +=  (string name) =>{
                GMData.Inist.decisions.EnableCancel(name);
            };

            HuangDAPI.Stability.evtChange += (int value) => {

                var opts = new List<Tuple<string, Action>>();
                opts.Add(new Tuple<string, Action>("CONFIRM", () => { GMData.Inist.stability.current += value; }));

                if (value > 0)
                {
                    StreamManager.EventManager.AddEvent("TITLE_STABILITY_INCREASE", "CONTENT_STABILITY_INCREASE" + value.ToString(), opts);
                }
                if (value < 0)
                {
                    StreamManager.EventManager.AddEvent("TITLE_STABILITY_DECREASE", "CONTENT_STABILITY_DECREASE" + value.ToString(), opts);
                }
            };

            HuangDAPI.Stability.stability = () =>{
                return GMData.Inist.stability;
            };
        }
    }
}
