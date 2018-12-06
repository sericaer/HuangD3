using HuangDAPI;
using System.Linq;
using UnityEngine;

namespace native
{
    class EVENT_FORCE_SSYD : EventDef
    {
        bool Precondition()
        {
            if (Decisions.SSYD.isPublished)
            {
                return false;
            }

            if (Offices.SG1.person.faction != Factions.SHI)
            {
                return false;
            }

            if(Factions.SHI.power < 0.3)
            {
                return false;
            }

            return true;
        }

        class OPTION1 : Option
        {
            void OnSelect()
            {
                Decisions.SSYD.OnPublish();
                Stability.current++;

                Debug.Log("OPTION1");
            }
        }
    }

    class EVENT_INC_SSYD_LEVEL : EventDef
    {
        bool Precondition()
        {
            if (!Decisions.SSYD.isPublished)
            {
                Debug.Log("EVENT_INC_SSYD_LEVEL1");
                return false;
            }

            if (Offices.SG1.person.faction != Factions.SHI)
            {
                Debug.Log("EVENT_INC_SSYD_LEVEL2");
                return false;
            }

            if (Factions.SHI.power > 0.4 && Decisions.SSYD.Level < 2)
            {
                return true;
            }

            if (Factions.SHI.power > 0.6 && Decisions.SSYD.Level < 3)
            {
                return true;
            }

            return false;
        }

        class OPTION1 : Option
        {
            void OnSelect()
            {
                Decisions.SSYD.Level++;

                Debug.Log("OPTION1");
            }
        }

        class OPTION2 : Option
        {
            void OnSelect()
            {
                Decisions.SSYD.Level += 2;
                Stability.current++;

                Debug.Log("OPTION2");
            }
        }
    }

    //class EVENT_YHSX_END : EVENT_HD
    //{
    //    bool Precondition()
    //    {
    //        if (CountryFlags.YHSX.IsEnabled())
    //        {
    //            if (Probability.IsProbOccur(0.05))
    //            {
    //                return true;
    //            }
    //        }
    //        return false;
    //    }

    //    class OPTION1 : Option
    //    {
    //        void Selected(ref string nxtEvent, ref object param)
    //        {
    //            CountryFlags.YHSX.Disable();
    //        }
    //    }
    //}
}