﻿using HuangDAPI;
using System.Linq;
using UnityEngine;

namespace native
{
    //class EVENT_YHSX_START : EventDef
    //{
    //    bool Precondition()
    //    {
    //        return Probability.IsProbOccur(0.001);
    //    }

    //    class OPTION1 : Option
    //    {
    //        void OnSelect()
    //        {
    //            CountryFlags.YHSX.Enable();
    //            Stability.current--;

    //            Debug.Log("OPTION1");
    //        }
    //    }
    //}

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