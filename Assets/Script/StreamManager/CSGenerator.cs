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

    public static string MakeDecisions(string[] dependCS)
    {
        List<string> SourceCodes = new List<string>(dependCS);
        string[] fileNames = Directory.GetFiles(path + "/decision", "*.cs", SearchOption.AllDirectories);
        foreach (string filename in Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories))
        {
            string script = File.ReadAllText(filename);
            SourceCodes.Add(script);
        }

        CSharpCompiler.ScriptBundleLoader.IScriptBundle bd = csharpLoader.LoadAndWatchSourceBundle(SourceCodes);

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

    public static string MakeOffices()
    {
        var fields = new List<Tuple<string, Type, Type, List<object>, CodeAttributeDeclaration>>();
        foreach (var eoffice in StreamManager.CSVManager.Office)
        {
            fields.Add(new Tuple<string, Type, Type, List<object>, CodeAttributeDeclaration>(eoffice.name, typeof(HuangDAPI.Office), typeof(HuangDAPI.Office), new List<object> { eoffice.name, int.Parse(eoffice.power), eoffice.group }, null));
        }

        CodeDomGen sourceCodeCreater = new CodeDomGen("Offices", fields);

        //foreach (var egroup in Enum.GetValues(typeof(OfficeGroup)))
        //{
        //    CodeMemberProperty prty = new CodeMemberProperty();
        //    prty.Name = "group" + egroup.ToString();
        //    prty.Type = new CodeTypeReference(typeof(HuangDAPI.Office[]));
        //    prty.Attributes = MemberAttributes.Public | MemberAttributes.Static;
        //    prty.HasGet = true;

        //    string seq = string.Format(@"
        //    Type type = typeof(Offices);
        //    List<FieldInfo> _subFields = type.GetFields(BindingFlags.Static).ToList();
            
        //    List<HuangDAPI.Office> rslt = new List<HuangDAPI.Office>();
        //    foreach(var field in _subFields)
        //    {{
        //        OfficeAttr attribute = Attribute.GetCustomAttribute(field, typeof(OfficeAttr)) as OfficeAttr;
        //        if(attribute != null && attribute.group == OfficeGroup.{0})
        //        {{
        //            rslt.Add((HuangDAPI.Office)field.GetValue(null));
        //        }}
        //    }}
        //    return rslt.ToArray();", egroup);

        //    CodeSnippetStatement snippet = new CodeSnippetStatement(seq);
        //    prty.GetStatements.Add(snippet);

        //    sourceCodeCreater.AddMemeber(prty);
        //}

        string source = sourceCodeCreater.Create();
        Debug.Log(source);

        Debug.Log("Load office count:" + fields.Count());
        return source;
    }

    public static string MakeFactions()
    {
        var fields = new List<Tuple<string, Type, Type, List<object>, CodeAttributeDeclaration>>();
        foreach (var eoffice in StreamManager.CSVManager.Faction)
        {
            fields.Add(new Tuple<string, Type, Type, List<object>, CodeAttributeDeclaration>(eoffice.name, typeof(HuangDAPI.Faction), typeof(HuangDAPI.Faction), new List<object> { eoffice.name }, null));
        }

        CodeDomGen sourceCodeCreater = new CodeDomGen("Factions", fields);

        //foreach (var egroup in Enum.GetValues(typeof(OfficeGroup)))
        //{
        //    CodeMemberProperty prty = new CodeMemberProperty();
        //    prty.Name = "group" + egroup.ToString();
        //    prty.Type = new CodeTypeReference(typeof(HuangDAPI.Office[]));
        //    prty.Attributes = MemberAttributes.Public | MemberAttributes.Static;
        //    prty.HasGet = true;

        //    string seq = string.Format(@"
        //    Type type = typeof(Offices);
        //    List<FieldInfo> _subFields = type.GetFields(BindingFlags.Static).ToList();

        //    List<HuangDAPI.Office> rslt = new List<HuangDAPI.Office>();
        //    foreach(var field in _subFields)
        //    {{
        //        OfficeAttr attribute = Attribute.GetCustomAttribute(field, typeof(OfficeAttr)) as OfficeAttr;
        //        if(attribute != null && attribute.group == OfficeGroup.{0})
        //        {{
        //            rslt.Add((HuangDAPI.Office)field.GetValue(null));
        //        }}
        //    }}
        //    return rslt.ToArray();", egroup);

        //    CodeSnippetStatement snippet = new CodeSnippetStatement(seq);
        //    prty.GetStatements.Add(snippet);

        //    sourceCodeCreater.AddMemeber(prty);
        //}

        string source = sourceCodeCreater.Create();
        Debug.Log(source);

        Debug.Log("Load faction count:" + fields.Count());
        return source;
    }
}