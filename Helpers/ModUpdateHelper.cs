using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Compression;
using System.Threading.Tasks;
using FactorioModEnvironnment.Properties;
using System.IO;
using Newtonsoft.Json.Linq;

namespace FactorioModEnvironnment.Helpers
{
    public class ModUpdateHelper
    {
        private string _modPath;
        private string _modName;
        private string _oldVersion;
        private string _newVersion;
        private Settings _settings;
        public ModUpdateHelper(string modName,updateLevelType updateLevel)
        {
            _settings = new Settings();
            _oldVersion = modName.Split('_').Last();
            _modName = modName.Replace("_" + _oldVersion, "");
            var oldVersionArray=_oldVersion.Split('.');
            oldVersionArray[(int)updateLevel] = (int.Parse(oldVersionArray[(int)updateLevel]) + 1).ToString();
            _newVersion = string.Join(".",oldVersionArray);
        }

        public void sendDataToGit(string modName)
        {
            
        }

        public void updateMod(bool generateZip)
        {
            //Update version
            Directory.Move(_modPath + "_" + _oldVersion, _modPath + "_" + _newVersion);        
            var jsonInfo = JObject.Parse(File.ReadAllText(_modPath + "_" + _newVersion + "/info.json"));
            jsonInfo.Property("version").Value = _newVersion;
            File.WriteAllText(_modPath + "_" + _newVersion + "/info.json", jsonInfo.ToString());

            if (generateZip)
                ZipFile.CreateFromDirectory(_modPath+"_"+_newVersion,_settings.DestinationZip+_modName,CompressionLevel.Optimal,true);
        }
    }
    public enum updateLevelType:int
    {
        minor=2,
        major=1,
        full=0
    }
}
