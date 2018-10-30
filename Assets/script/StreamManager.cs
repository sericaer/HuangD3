using System;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class StreamManager
{
    public class DynastyName
    {
        public static string GetRandom()
        {
            int i = Probability.GetRandomNum(0, names.Count - 1);
            return names[i];
        }

        public static void Load(Type[] types)
        {
            Type type = types.Where(x => x.Name == "DynastyName").First();
            object obj = Activator.CreateInstance(type);

            FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            FieldInfo nameField = fields.Where(x => x.Name == "name").First();
            names.AddRange((string[])nameField.GetValue(obj));
        }

        internal static List<string> names = new List<string>();
    }


    public class YearName
    {
        public static string GetRandom()
        {
            int i = Probability.GetRandomNum(0, names.Count - 1);

            int j = i;
            while (j == i)
            {
                j = Probability.GetRandomNum(0, names.Count - 1);
            }

            return names[i] + names[j];
        }

        public static void Load(Type[] types)
        {
            Type type = types.Where(x => x.Name == "YearName").First();
            object obj = Activator.CreateInstance(type);

            FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            FieldInfo nameField = fields.Where(x => x.Name == "name").First();
            yearName.names.AddRange((string[])nameField.GetValue(obj));
        }

        internal static List<string> names = new List<string>();
    }

    public class PersonName
    {
        public static string GetRandomMale()
        {
            int i = Probability.GetRandomNum(0, family.Count - 1);
            int j = Probability.GetRandomNum(0, given.Count - 1);

            return family[i] + given[j];
        }

        public static string GetRandomFemale()
        {
            int i = Probability.GetRandomNum(0, family.Count - 1);
            int j = Probability.GetRandomNum(0, givenfemale.Count - 1);

            return family[i] + givenfemale[j];
        }


        public static void Load(Type[] types)
        {
            IEnumerable<Type> EmumType = types.Where(x => x.Name == "PersonName");
            if (EmumType.Count() == 0)
            {
                return;
            }

            Type PersonNameType = EmumType.First();
            object obj = Activator.CreateInstance(PersonNameType);

            FieldInfo[] fields = PersonNameType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            IEnumerable<FieldInfo> EmumField = fields.Where(x => x.Name == "family");
            if (EmumField.Count() != 0)
            {
                FieldInfo familyField = EmumField.First();
                family.AddRange((string[])familyField.GetValue(obj));
            }

            EmumField = fields.Where(x => x.Name == "given");
            if (EmumField.Count() != 0)
            {
                FieldInfo givenField = EmumField.First();
                given.AddRange((string[])givenField.GetValue(obj));
            }

            EmumField = fields.Where(x => x.Name == "givenfemale");
            if (EmumField.Count() != 0)
            {
                FieldInfo givenfemaleField = EmumField.First();
                givenfemale.AddRange((string[])givenfemaleField.GetValue(obj));
            }
        }

        internal static List<string> family = new List<string>();
        internal static List<string> given = new List<string>();
        internal static List<string> givenfemale = new List<string>();
    }

    private StreamManager()
    {
        csharpLoader = new CSharpCompiler.ScriptBundleLoader(null);
        csharpLoader.actLog = CSharpCompiler.UnityLogTextWriter.Log;

        string[] subDir = Directory.GetDirectories(Application.streamingAssetsPath);
        foreach (string dirname in subDir)
        {
            string infoPath = dirname + "/info.txt";
            if (!File.Exists(infoPath))
            {
                continue;
            }

            LoadMod(dirname);
        }
    }

    private void LoadMod(string path)
    {
        Debug.Log(string.Format("*****************Start Load mod {0}********************", path));

        foreach (string filename in Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories))
        {
            string script = File.ReadAllText(filename);
            sourceCodes.Add(script);
        }

        CSharpCompiler.ScriptBundleLoader.IScriptBundle bd = csharpLoader.LoadAndWatchSourceBundle(sourceCodes.ToArray());

        Types = bd.assembly.GetTypes();

        DynastyName.Load(Types);
        YearName.Load(Types);
        PersonName.Load(Types);
    
        Debug.Log(string.Format("******************End Load mod {0}********************", path));
    }

    
}

