using StandardTools.ServiceLocator;
using System.IO;

namespace FactorioModEnvironnment.Helpers
{
    public interface IApplicationDataHandler:IService
    {
        string GetGitPath(FileInfo file);

        void LoadData();

        void SetGitPath(FileInfo file, string gitPath);
    }
}
