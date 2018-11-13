using System;
using System.Collections;
using System.Collections.Generic;
using Tools;

public partial class StreamManager
{
    public class ProvinceCSV
    {
        //public static event Action<string, int> evtAddProv;

        public static void Load(string path)
        {
            CSVReader _csv = new CSVReader(path);
            listProvDef.AddRange(_csv.rows);
        }

        public static dynamic[] Defs
        {
            get
            {
                return listProvDef.ToArray();
            }
        }

        public static List<dynamic> listProvDef = new List<dynamic>();

    }
}