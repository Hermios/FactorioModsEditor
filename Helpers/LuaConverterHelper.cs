using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModEnvironnment.Helpers
{
    public static class LuaConverterHelper
    {
        public static string ConvertDictionaryAsLua(Dictionary<string, object> dataToConvert, int tabs = 0)
        {
            StringBuilder result = new StringBuilder();
            string startTabs = "";
            for (int i = 0; i < tabs; i++)
                startTabs += "   ";

            foreach (var entry in dataToConvert)
            {
                result.Append(startTabs + entry.Key + " = ");
                switch (entry.Value.GetType().ToString())
                {
                    case "System.string":
                        result.Append("\"" + entry.Value + "\"");
                        break;
                    case "System.Int32":
                        result.Append(entry.Value);
                        break;
                    case "System.Boolean":
                        result.Append(entry.Value.ToString());
                        break;
                    case "System.Collections.Generic.Dictionary<TKey,TValue>":
                        result.Append(Environment.NewLine + startTabs + "{" + Environment.NewLine);
                        result.Append(ConvertDictionaryAsLua(entry.Value as Dictionary<string, object>, tabs + 1));
                        result.Append(Environment.NewLine + "}");
                        break;
                    case "System.Collections.Generic.List<T>":
                        result.Append(ConvertListAsLua(entry.Value as List<object>));
                        break;
                }
                result.Append("," + Environment.NewLine);
            }
            return result.ToString();
        }

        public static string ConvertListAsLua(List<object> dataToConvert)
        {
            StringBuilder result = new StringBuilder("{");
            foreach (var data in dataToConvert)
            {
                switch (data.GetType().ToString())
                {
                    case "System.string":
                        result.Append("\"" + data + "\"");
                        break;
                    case "System.Int32":
                        result.Append(data);
                        break;
                    case "System.Boolean":
                        result.Append(data);
                        break;
                }
                result.Append(",");
            }
            result.Append("}");
            return result.ToString();
        }

    }
}
