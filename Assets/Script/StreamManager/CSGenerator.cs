using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.CodeDom;

public class CSGenerator
{
    public static CSharpCompiler.ScriptBundleLoader csharpLoader;
    public static string path;

    public static string MakeFlags()
    {
        
        string[] fileNames = Directory.GetFiles(path+"/flag", "*.cs", SearchOption.AllDirectories);
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

        Debug.Log(source);

        Debug.Log("Load country flag count:" + FlagTypes.Count());
        return source;
    }

    public static string MakeDecisions()
    {
        string[] fileNames = Directory.GetFiles(path+"/decision", "*.cs", SearchOption.AllDirectories);
        CSharpCompiler.ScriptBundleLoader.IScriptBundle bd = csharpLoader.LoadAndWatchScriptsBundle(fileNames);

        Type[] types = bd.assembly.GetTypes();

        Type[] FlagTypes = types.Where(x => x.BaseType.Name == "DefDecision").ToArray();

        var fields = new List<Tuple<string, Type, Type, List<object>, CodeAttributeDeclaration>>();
        foreach (var type in FlagTypes)
        {
            fields.Add(new Tuple<string, Type, Type, List<object>, CodeAttributeDeclaration>(type.Name, type, type, null, null));
        }

        CodeDomGen sourceCodeCreater = new CodeDomGen("Decisions", fields);
        string source = sourceCodeCreater.Create();
        Debug.Log(source);

        Debug.Log("Load decision count:" + FlagTypes.Count());
        return source;
    }
}