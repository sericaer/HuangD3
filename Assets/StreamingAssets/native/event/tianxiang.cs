using HuangDAPI;
using System.Linq;
using UnityEngine;

namespace native
{
    class EVENT_YHSX_START : EventDef
    {
        bool Precondition()
        {
            if (CountryFlags.YHSX.isEnabled)
            {
                return false;
            }

            return Probability.OccurPerDays(2000);
        }

        class OPTION1 : Option
        {
            void OnSelect()
            {
                CountryFlags.YHSX.Enable();
                Stability.current--;

                Debug.Log("EVENT_YHSX_START OPTION1");
            }
        }
    }

    class EVENT_YHSX_AFFECT_EMPHEATH : EventDef
    {
        bool Precondition()
        {
            if (!CountryFlags.YHSX.isEnabled)
            {
                return false;
            }

            return Probability.OccurPerDays(30);
        }

        class OPTION1 : Option
        {
            void OnSelect()
            {
                CountryFlags.YHSX.emperorHeathAffect--;
            }
        }
    }

    class EVENT_YHSX_END : EventDef
    {
        bool Precondition()
        {
            if (!CountryFlags.YHSX.isEnabled)
            {
                return false;
            }

            return Probability.OccurPerDays(50);
        }

        class OPTION1 : Option
        {
            void OnSelect()
            {
                CountryFlags.YHSX.Disable();

                Debug.Log("EVENT_YHSX_END OPTION1");
            }
        }
    }
}