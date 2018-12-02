using StandardTools.ServiceLocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModEnvironnment.Helpers.LuaData
{
    public interface ILuaData:IService
    {
        List<string> GetRawTypes();
        List<string> GetRawNames(string type);
        string GetRawValue(string name, string type, params string[] keys);
    }
}
