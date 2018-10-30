using System;

public class GMData
{
    public GMData()
    {
    }

    public class DynastyName
    {
        public string GetRandom()
        {
            int i = Probability.GetRandomNum(0, names.Count - 1);
            return names[i];
        }

        internal List<string> names = new List<string>();
    }


    public class YearName
    {
        public string GetRandom()
        {
            int i = Probability.GetRandomNum(0, names.Count - 1);

            int j = i;
            while (j == i)
            {
                j = Probability.GetRandomNum(0, names.Count - 1);
            }

            return names[i] + names[j];
        }

        internal List<string> names = new List<string>();
    }


    public class PersonName
    {
        public string GetRandomMale()
        {
            int i = Probability.GetRandomNum(0, family.Count - 1);
            int j = Probability.GetRandomNum(0, given.Count - 1);

            return family[i] + given[j];
        }

        public string GetRandomFemale()
        {
            int i = Probability.GetRandomNum(0, family.Count - 1);
            int j = Probability.GetRandomNum(0, givenfemale.Count - 1);

            return family[i] + givenfemale[j];
        }

        internal List<string> family = new List<string>();
        internal List<string> given = new List<string>();
        internal List<string> givenfemale = new List<string>();
    }
}

