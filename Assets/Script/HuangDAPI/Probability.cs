using System;
namespace HuangDAPI
{
    public class Probability
    {
        public static bool IsProbOccur(double prob)
        {
            return Tools.Probability.IsProbOccur(prob);
        }

        public static bool OccurPerDays(int daycount)
        {
            return Tools.Probability.IsProbOccur(1/(double)daycount);
        }
    }
}
