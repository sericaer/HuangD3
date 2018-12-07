using HuangDAPI;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace native
{
    public class SSYD : DefDecision
    {
        public bool EnablePublish()
        {
            if (Offices.SG1.person.faction == Factions.SHI
              && Factions.SHI.power > 0.2)
            {
                return true;
            }

            return false;
        }

        public bool EnableCancel()
        {
            if (Offices.SG1.person.faction != Factions.SHI
                && Factions.SHI.power < 0.2)
            {
                return true;
            }
            return false;
        }

        public double affectProvinceTax(double baseValue)
        {
            Debug.Log("affectProvinceTax");

            return baseValue * Level * -0.1;
        }

        public string Title()
        {
            return string.Format("DEC_{0}_{1}_TITLE", this.GetType().Name, Level);
        }

        public void InitData()
        {
            data.level = 1;
        }

        public int Level
        {
            get
            {
                return data.level;
            }
            set
            {
                data.level = value;
            }
        }

        //public double provinceTaxAffect = -0.1;
    }

    //public class SSYD : COUNTRY_FLAG
    //{
    //    public SSYD()
    //    {
    //        _param.level = 0;
    //    }

    //    public override string Title()
    //    {
    //        return string.Format("{0}_{1}_{2}", "COUNTRY_FLAG", GetType().Name + Level.ToString(), "TITLE");
    //    }

    //    public override string Desc()
    //    {
    //        return string.Format("{0}_{1}_{2}", "COUNTRY_FLAG", GetType().Name + Level.ToString(), "DESC");
    //    }

    //    public double affectCountryTax(double baseValue)
    //    {
    //        switch (Level)
    //        {
    //            case 1:
    //                return -baseValue * 0.1;
    //            case 2:
    //                return -baseValue * 0.25;
    //            case 3:
    //                return -baseValue * 0.5;
    //            default:
    //                throw new Exception();
    //        }
    //    }

    //    public int Level
    //    {
    //        get
    //        {
    //            //Debug.Log(_param.level);
    //            return (int)_param.level;
    //        }
    //        set
    //        {
    //            _param.level = value;
    //            if (_param.level == 0)
    //            {
    //                Disable();
    //            }
    //            else
    //            {
    //                Enable();
    //            }

    //        }
    //    }
    //}

    //public class KJZS : COUNTRY_FLAG
    //{
    //    public KJZS()
    //    {
    //        _param.level = 0;
    //    }

    //    public override string Title()
    //    {
    //        return string.Format("{0}_{1}_{2}", "COUNTRY_FLAG", GetType().Name + Level.ToString(), "TITLE");
    //    }

    //    public override string Desc()
    //    {
    //        return string.Format("{0}_{1}_{2}", "COUNTRY_FLAG", GetType().Name + Level.ToString(), "DESC");
    //    }

    //    public double affectCountryTax(double baseValue)
    //    {
    //        switch (Level)
    //        {
    //            case 1:
    //                return baseValue * 0.1;
    //            case 2:
    //                return baseValue * 0.3;
    //            case 3:
    //                return baseValue * 0.5;
    //            default:
    //                throw new Exception();
    //        }
    //    }

    //    public double affectCountryReb(double baseValue)
    //    {
    //        switch (Level)
    //        {
    //            case 1:
    //                return 0.5;
    //            case 2:
    //                return 1.0;
    //            case 3:
    //                return 3.0;
    //            default:
    //                throw new Exception();
    //        }
    //    }

    //    public int Level
    //    {
    //        get
    //        {
    //            return (int)_param.level;
    //        }
    //        set
    //        {
    //            _param.level = value;
    //            if (_param.level == 0)
    //            {
    //                Disable();
    //            }
    //            else
    //            {
    //                Enable();
    //            }

    //        }
    //    }

    //    public int MAX_LEVEL = 3;
    //    public int MIN_LEVEL = 0;
    //}
}