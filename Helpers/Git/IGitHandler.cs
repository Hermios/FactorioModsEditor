namespace FactorioModEnvironnment.Helpers
{
    public interface IGitHandler: StandardTools.ServiceLocator.IService
    {
        void PushData();

        void CommitData();

        void UpdateData();
    }
}
