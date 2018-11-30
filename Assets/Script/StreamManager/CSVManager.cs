using System;
using System.Collections;
using System.Collections.Generic;
using Tools;

public partial class StreamManager
{
    public class CSVManager
    {
        //public static event Action<string, int> evtAddProv;

        public static void Load(string path)
        {
            CSVReader _csv1 = new CSVReader(path +"/prov.csv");
            listProvince.AddRange(_csv1.rows);

            CSVReader _csv2 = new CSVReader(path + "/office.csv");
            listOffice.AddRange(_csv2.rows);

            CSVReader _csv3 = new CSVReader(path + "/faction.csv");
            listFaction.AddRange(_csv3.rows);
        }

        public static dynamic[] Province
        {
            get
            {
                return listProvince.ToArray();
            }
        }

        public static dynamic[] Office
        {
            get
            {
                return listOffice.ToArray();
            }
        }

        public static dynamic[] Faction
        {
            get
            {
                return listFaction.ToArray();
            }
        }

        private static List<dynamic> listProvince = new List<dynamic>();
        private static List<dynamic> listOffice = new List<dynamic>();
        private static List<dynamic> listFaction = new List<dynamic>();
    }
}