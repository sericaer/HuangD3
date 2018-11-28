using System;
namespace GMDATA
{
    public class GMControll
    {
        public static void Init()
        {
            DecisionLogic.evtPublish += (string name) =>{
                GMData.Inist.decisions.OnPublish(name);
            };

            DecisionLogic.evtCancel += (string name) =>{
                GMData.Inist.decisions.OnCancel(name);
            };

            Decisions.evtDel += DecisionLogic.DestroyInstance;
            Decisions.evtAdd += (Decision decision) =>{
                ChaoTScene.Task (()=>{
                    DecisionLogic.newInstance(decision, ChaoTScene.Inst.panelDecision.transform);
                });
            };

            Decision.evtStateChange += DecisionLogic.OnStateChange;
        }
    }
}
