using System;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Tools;
using System.CodeDom;

public partial class StreamManager
{
    public static void Load()
    {
        if(_inst == null)
        {
            _inst = new StreamManager();
        } 
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

        CSVManager.Load(path);

        CSGenerator.csharpLoader = csharpLoader;
        CSGenerator.path = path;

        List<string> sourceCodes = new List<string>();
        sourceCodes.Add(CSGenerator.MakeOffices());
        sourceCodes.Add(CSGenerator.MakeFactions());

        sourceCodes.Add(CSGenerator.MakeFlags());
        sourceCodes.Add(CSGenerator.MakeDecisions(sourceCodes.ToArray()));


        foreach (string filename in Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories))
        {
            string script = File.ReadAllText(filename);
            sourceCodes.Add(script);
        }

        CSharpCompiler.ScriptBundleLoader.IScriptBundle bd = csharpLoader.LoadAndWatchSourceBundle(sourceCodes.ToArray());

        Type[] Types = bd.assembly.GetTypes();

        DynastyName.Load(Types);
        YearName.Load(Types);
        PersonName.Load(Types);
        EventManager.Load(Types);


        var qurey = (from x in Types
                     where x.Name == "CountryFlags"
                     select x).Single();
        System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(qurey.TypeHandle);

        qurey = (from x in Types
                     where x.Name == "Decisions"
                     select x).Single();
        System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(qurey.TypeHandle);

        qurey = (from x in Types
                 where x.Name == "Offices"
                 select x).Single();
        System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(qurey.TypeHandle);

        qurey = (from x in Types
                 where x.Name == "Factions"
                 select x).Single();
        System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(qurey.TypeHandle);

        Debug.Log(string.Format("******************End Load mod {0}********************", path));
    }

    private static StreamManager _inst;
    private CSharpCompiler.ScriptBundleLoader csharpLoader;
}

