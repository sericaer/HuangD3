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

        List<string> sourceCodes = new List<string>();
        sourceCodes.Add(GenerateFlags(path + "/flag"));

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
        CSVManager.Load(path);

        var qurey = (from x in Types
                     where x.Name == "CountryFlags"
                     select x).Single();
        System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(qurey.TypeHandle);

        Debug.Log(string.Format("******************End Load mod {0}********************", path));
    }

    private string GenerateFlags(string path)
    {
        List<string> defineSourceCodes = new List<string>();

        string[] fileNames = Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories);
        CSharpCompiler.ScriptBundleLoader.IScriptBundle bd = csharpLoader.LoadAndWatchScriptsBundle(fileNames);

        Type[] types = bd.assembly.GetTypes();

        Type[] FlagTypes = types.Where(x => x.BaseType.Name == "DefCountryFlag").ToArray();

        var fields = new List<Tuple<string, Type, Type, List<object>, CodeAttributeDeclaration>>();
        foreach (var type in FlagTypes)
        {
            fields.Add(new Tuple<string, Type, Type, List<object>, CodeAttributeDeclaration>(type.Name, type, type, null, null));
        }

        CodeDomGen sourceCodeCreater = new CodeDomGen("CountryFlags", fields);
        string source = sourceCodeCreater.Create();

//        Regex r = new Regex(@"(.*)( = new )(.*())(.*;)");
//        source = r.Replace(source, new MatchEvaluator((Match match) =>
//        {
//            string result = string.Format(@"
//                {{
//                    var elem = (from x in COUNTRY_FLAG.All
//                    where x.GetType().Name == typeof({0}).Name
//                    select x).SingleOrDefault();
//
//                    if (elem != null)
//                    {{
//                        {1} = elem as {2};
//                    }}
//                    else
//                    {{
//                        {3} = new {4}();
//                    }}
//                }}",
//                                          match.Groups[3].Value.TrimEnd("()".ToCharArray()), match.Groups[1].Value, match.Groups[3].Value.TrimEnd("()".ToCharArray()), match.Groups[1].Value, match.Groups[3].Value.TrimEnd("()".ToCharArray()));

//            return result;
//        }));

        Debug.Log(source);

        Debug.Log("Load country flag count:" + FlagTypes.Count());
        return source;
    }

    private static StreamManager _inst;
    private CSharpCompiler.ScriptBundleLoader csharpLoader;
}

