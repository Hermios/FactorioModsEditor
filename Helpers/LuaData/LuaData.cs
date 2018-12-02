using System.Linq;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using NLua;
using HtmlAgilityPack;
using StandardTools.ServiceLocator;
using FactorioModEnvironnment.Properties;

namespace FactorioModEnvironnment.Helpers.LuaData
{
    public class LuaData:ILuaData
    {
        private const string REQUIREDREGEX = "^require\\s*\\(?\"(\\w*)";
        private Lua _luaObject;

        public LuaData()
        {
            Settings settings = new Settings();
            var packagePaths = new StringBuilder();
            foreach (var path in settings.luaLibsPaths)
                packagePaths.Append(settings.FactorioProgramFiles +  path + "/?.lua;");
            _luaObject = new Lua();
            _luaObject.DoString("package.path=\""+packagePaths.ToString()+"\"");
            AddDefines(settings.FactorioProgramFiles+settings.definesHtml);
        }

        private void AddDefines(string htmlSource)
        {
            HtmlDocument definesDocument = new HtmlDocument();
            definesDocument.LoadHtml(File.ReadAllText(htmlSource));

            _luaObject.DoString("defines={}");
            foreach (var childNode in definesDocument.DocumentNode.Descendants().Where(node => node.HasClass("header")).Select(node => node.FirstChild))
            {
                var definesNameArray = childNode.InnerText.Replace("defines.","").Split('.');
                string parent = "defines";
                foreach (var element in definesNameArray)
                {
                    var currentName = parent + "." + element;
                    _luaObject.DoString(currentName+"="+
                        (
                            currentName==childNode.InnerText?
                            (_luaObject.GetTable(parent).Keys.Count+1).ToString():
                            currentName+" or {}"
                         ));
                    parent= currentName;
                }                
            }

        }

        private void LoadLuaFile(string filePath)
        {           
            try
            {                
                _luaObject.DoFile(filePath);
            }
            catch (Exception e) { Console.WriteLine(filePath+ "->" + e.Message); }
        }

        public List<string> GetRawTypes()
        {
            var listKeys = (_luaObject["data.raw"] as LuaTable);
            var array = new String[listKeys.Keys.Count];
            listKeys.Keys.CopyTo(array, 0);
            return array.ToList<string>().Where(type => listKeys[type].GetType() == typeof(LuaTable)).ToList<string>();
        }

        public List<string> GetRawNames(string type)
        {
            var listKeys = _luaObject["data.raw." + type] as LuaTable;
            var array = new String[listKeys.Keys.Count];
            listKeys.Keys.CopyTo(array, 0);
            return array.ToList<string>().Where(name => listKeys[name].GetType() == typeof(LuaTable)).ToList<string>();
        }

        public string GetRawValue(string name, string type, params string[] keys)
        {
            var path = "." + string.Join(".", keys);
            return _luaObject["data.raw." + type + "." + name + path] as string;
        }
        
        public void initService(IServiceLocator serviceLocator, params object[] args)
        {
            Settings settings = new Settings();
            foreach(var fileToLoad in settings.luaInitFiles)
                this.LoadLuaFile(settings.FactorioProgramFiles+fileToLoad);
        }
    }
}
